using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskPractice.Data.dto;
using TaskPractice.Data.Model;

namespace TaskPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDbContext;

        public CategoryController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        [HttpGet]
        public ActionResult getAllCategory() {
           var category= applicationDbContext.Categories.ToList();
            if(category==null)
                return NotFound("Category not found");

            return Ok(category);
        }
        [HttpPost]
        public ActionResult addCategory(dto_Category addCategory)
        {
            var category = new Category
            {
                CategoryName = addCategory.CategoryName
            };
            applicationDbContext.Categories.Add(category);
            applicationDbContext.SaveChanges();
            return Ok("Category Add Successfully");
        }
        [HttpPut]
        public ActionResult updateCategory(dto_UpdateCategory category)
        {
           var existingCategory= applicationDbContext.Categories.Find(category.CategoryId);
            if (existingCategory == null)
                return NotFound("Category Not Exist so I can Not Update");
            else
            {
                existingCategory.CategoryName = category.CategoryName;
                applicationDbContext.Categories.Update(existingCategory);
                 applicationDbContext.SaveChanges();
                return Ok("Update Category Successfully");

            }
        }
        [HttpDelete]
        public ActionResult deleteCategory(int id)
        {
            var existingCategory= applicationDbContext.Categories.Find(id);
            if(existingCategory == null)
            {
                return NotFound("Id Does Not Exist So I can Not Delete It");
            }
            else
            {
                applicationDbContext.Categories.Remove(existingCategory);
                applicationDbContext.SaveChanges();
                return Ok("Category Deleted Successfully");
            }
        }
    }
}
