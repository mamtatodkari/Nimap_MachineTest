using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskPractice.Data.dto;
using TaskPractice.Data.Model;
using BCrypt.Net;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Data;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace TaskPractice.Services
{
    public class Login : ILogin
    {
        private readonly ApplicationDbContext applicationDbContext;

        public Login(ApplicationDbContext applicationDbContext, IConfiguration configuration)
        {
            this.applicationDbContext = applicationDbContext;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public async Task<ActionResult> LoginAsync(string email, string password)
        {
            try
            {
                var user = await AuthenticateUserAsync(email, password);

                if (user != null)
                {
                    var tokenString = GenerateToken(user);
                    return new OkObjectResult(new { token = tokenString });
                }

                return new UnauthorizedObjectResult(new { message = "Unauthorized" });
            }
            catch (Exception ex)
            {
                // Log the exception here (actual logging not shown)
                return new ObjectResult(new { message = "An error occurred while logging in", error = ex.Message })
                {
                    StatusCode = 500
                };
            }
        }
        //{
        //   var user=  await AuthenticateUserAsync(email, password);
        //    if(user != null)
        //    {
        //        var tokenString = GenerateToken(user);
        //        return new OkObjectResult(new { token = tokenString });
        //    }
        //}

        private object GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim> {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        new Claim(JwtRegisteredClaimNames.Email, user.UserEmail),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Unique identifier for the token
    };

            // Add roles as claims
            var userRoles = applicationDbContext.UserRoles.Where(u => u.UserId == user.id).ToList();
            var roleIds = userRoles.Select(u => u.RoleId).ToList();
            var roles = applicationDbContext.Roles.Where(r => roleIds.Contains(r.id)).ToList();
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name)); // Use ClaimTypes.Role for role claims
            }

            var token = new JwtSecurityToken(
                issuer: Configuration["Jwt:Issuer"],
                audience: Configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    

    [NonAction]
        private async Task<User> AuthenticateUserAsync(string email, string password)
        {

            string hashedPassword = await HashPassword(password);
            var user = applicationDbContext.Users.FirstOrDefault(u => u.UserEmail == email && u.Password == hashedPassword);

            if (user == null)
            {
                throw new AuthenticationException("Invalid username or password");
            }
            return user;
        }

        public async Task<String> RegisterAsync(dto_Register users)
        {
            var existingUser =  await applicationDbContext.Users.FirstOrDefaultAsync(x => x.UserEmail == users.UserEmail);
            if (existingUser != null)
            {
                return "User Is Registerd";
            }
            string hashedPassword = await HashPassword(users.Password);
            var user = new User
            {
             UserEmail=users.UserEmail,
              UserName=users.UserName,
               Password= hashedPassword
            };
            await applicationDbContext.AddAsync(user);
            await applicationDbContext.SaveChangesAsync();
            return "User Registered Successfull";
        }

        private async Task<string> HashPassword(string password)
        {
            // Generate a random salt
            byte[] salt = Encoding.UTF8.GetBytes("fixedSaltValue123");

            // Create a new instance of the Rfc2898DeriveBytes class
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);

            // Generate the hash value
            byte[] hash = pbkdf2.GetBytes(20);

            // Combine the salt and hash
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            // Convert the combined salt+hash to a base64-encoded string
            string hashedPassword = Convert.ToBase64String(hashBytes);

            return hashedPassword;
        }

        public Task<ActionResult> ResetPasswordAsync(string userEmail, string newPassword)
        {
            throw new NotImplementedException();
        }
        public Role AddRole(dto_Role dto_role)
        {
            var role = new Role
            {
                Name = dto_role.Name,
                RoleDescription = dto_role.RoleDescription

            };
             applicationDbContext.Roles.Add(role);
            applicationDbContext.SaveChanges();
            return role;
        }
        public bool AssignRoleToUser(dto_addUserRole obj)
        {
            try
            {
                var addRoles = new List<UserRole>();
                var user = applicationDbContext.Users.SingleOrDefault(s => s.id == obj.UserId);
                if (user == null)
                {
                    throw new Exception("User Not Valid");
                }
                foreach (int role in obj.RoleIds)
                {
                    var userRoles = new UserRole();
                    userRoles.RoleId = role;
                    userRoles.UserId = user.id;
                    addRoles.Add(userRoles);

                }
                applicationDbContext.UserRoles.AddRange(addRoles);
                applicationDbContext.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

    }
}
