using System.ComponentModel.DataAnnotations;

namespace CrudOperation.Models.Model
{
    public class Category
    {
        [PrimaryKey] 
        public int CategoryId { get; set; }
        public string ? CategoryName { get; set; }
    }
}
