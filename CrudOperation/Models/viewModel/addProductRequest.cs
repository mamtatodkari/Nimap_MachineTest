using Microsoft.AspNetCore.Mvc.Rendering;

namespace CrudOperation.Models.viewModel
{
    public class addProductRequest
    {
        public string? ProductName { get; set; }
        public IEnumerable<SelectListItem > Products { get; set; }
        public string ?SelectedProduct { get; set; }
    }
}
