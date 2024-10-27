using System.ComponentModel.DataAnnotations;

namespace TaskPractice.Data.Model
{
    public class Role
    {
        [Key]
        public int id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string RoleDescription { get; set; } = string.Empty;
    }
}
