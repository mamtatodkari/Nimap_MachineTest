using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskPractice.Data.dto;
using TaskPractice.Data.Model;
using TaskPractice.Services;

namespace TaskPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Category1Controller : ControllerBase
    {
        private readonly ICategory category;

        public Category1Controller(ICategory category)
        {
            this.category = category;
        }
        [HttpPost]
        [Route("AddCategory")]
        public ActionResult AddCategory(dto_Category categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            category.Add(categoryDto);
            return Ok("Category Added Successfully");
        }
        [HttpGet]
        [Route("getAllCategory")]
        public ActionResult getAllCategory()
        {
            IEnumerable<Category> Existining = category.GetAll();
            return Ok(Existining);
        }

        [HttpGet]
        [Route("getCategory")]
        public ActionResult getCategory(int id)
        {
          var existingCategory=  category.GetById(id);
            if (existingCategory == null)
            {
                return NotFound("This Id is NOt Exist");
            }
            return Ok(existingCategory);

        }
        [HttpPut]
        [Route("UpdateCategory")]
        public ActionResult updateCategory(dto_UpdateCategory dto_UpdateCategory)
        {
            var updatedCategory=category.Update(dto_UpdateCategory);
            if(updatedCategory==null)
            {
                return NotFound("This Category Id is Not Exist");
            }
            return Ok("Update Success");

        }
        [HttpDelete]

        [Route("DeleteCategory")]
        public ActionResult deleteCategory(int id)
        {
            var deletedCategory=category.Delete(id);
            if (deletedCategory == null)
            {
                return NotFound("This Id Is Not Exist So We can Delete");
            }

            return Ok("Category Has Successfull Deleted");
        }
    }
}
