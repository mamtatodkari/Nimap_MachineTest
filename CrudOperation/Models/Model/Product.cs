using System.ComponentModel.DataAnnotations.Schema;

namespace CrudOperation.Models.Model
{
    public class Product
    {
        [PrimaryKey]
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        [ForeignKey("Categorys")]
        public int CategoryId { get; set; }
        public Category? Categorys { get; set; }
    }
}
