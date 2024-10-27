using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using TaskPractice.Data.dto;
using TaskPractice.Data.Model;
using TaskPractice.Services;

namespace TaskPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryWithAsyncController : ControllerBase
    {
        private readonly ICategoryAsync categoryAsync;

        public CategoryWithAsyncController(ICategoryAsync categoryAsync)
        {
            this.categoryAsync = categoryAsync;
        }
        [HttpGet]
        [Route("GetAll")]
       public async Task<ActionResult<IEnumerable>> GetAll()
        {
           var existingCategory= await categoryAsync.GetAllAsync();
            return Ok(existingCategory);

        }
        [HttpGet]
        [Route("GetSingle")]
        public async Task<ActionResult<Category>> GetSingle(int id)
        {
           var existingCategory= await categoryAsync.GetByIdAsync(id);
            if(existingCategory == null)
            {
                return NotFound("Id Does Not Exist");
            }
            return Ok(existingCategory);
        }

        [HttpPost]
        [Route("AddCategory")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddCategory(dto_Category dto_Category)
        {
            await categoryAsync.AddAsync(dto_Category);
            return Ok("Added into database");
        }
        [HttpPut]
        [Route("UpdateCategory")]
        public async Task<ActionResult<Category>> UpdateCategory(dto_UpdateCategory dto_UpdateCategory)
        {
           var UpdateCategory= await categoryAsync.UpdateAsync(dto_UpdateCategory);
            if(UpdateCategory == null)
            {
                return NotFound("This Category is NOt Exist so I can Not Update It");
            }
            return Ok(UpdateCategory);
        }
        [HttpDelete]
        [Route("DeleteCategory")]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
           var deletedCategory= await categoryAsync.DeleteAsync(id);
            if(deletedCategory == null)
            {
                return NotFound("This Id is Not Present So I can not Delete this");
            }
            return Ok(deletedCategory);
        }
    }
}
