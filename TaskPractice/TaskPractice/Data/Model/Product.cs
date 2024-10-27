using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskPractice.Data.Model
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string  ProductName { get; set; }
        [ForeignKey("Categorys")]
        public int ?CategoryId { get; set; }
        public Category  Categorys { get; set; }
    }
}
