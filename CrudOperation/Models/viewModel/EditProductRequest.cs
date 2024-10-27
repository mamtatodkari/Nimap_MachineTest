using Microsoft.AspNetCore.Mvc.Rendering;

namespace CrudOperation.Models.viewModel
{
    public class EditProductRequest
    {
        public int ProductId { get; set; }    
        public string ProductName { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public string? SelectedCategory { get; set; }
    }
}
