using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskPractice.Data.dto;
using TaskPractice.Data.Model;
using TaskPractice.Services;

namespace TaskPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogin login;

        public LoginController(ILogin login)
        {
            this.login = login;
        }
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<string>> register(dto_Register dto_Register)
        {
            return await login.RegisterAsync(dto_Register);

        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            return await login.LoginAsync(email, password);
        }
        [HttpPost("AddRole")]
        public Role AddRole([FromBody] dto_Role dto_Role)
        {
            var addRole = login.AddRole(dto_Role);
            return addRole;
        }
        [HttpPost("AssignRole")]
        public bool AssignRoleToUser([FromBody] dto_addUserRole userRole)
        {
            var addUserRole = login.AssignRoleToUser(userRole);
            return addUserRole;
        }


    }
}
    