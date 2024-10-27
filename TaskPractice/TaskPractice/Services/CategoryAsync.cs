using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TaskPractice.Data.dto;
using TaskPractice.Data.Model;

namespace TaskPractice.Services
{
    public class CategoryAsync:ICategoryAsync
    {
        private readonly ApplicationDbContext applicationDbContext;

        public CategoryAsync(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await applicationDbContext.Categories.ToListAsync();
        }
        public async Task<Category> GetByIdAsync(int id)
        {
            var existingCategory = await applicationDbContext.Categories.FindAsync(id);
            if (existingCategory != null)
            {
                return existingCategory;
            }
            return null;
        }
        public async Task AddAsync(dto_Category categoryDto)
        {
            Category category = new Category
            {
                CategoryName = categoryDto.CategoryName
            };
            await applicationDbContext.AddAsync(category);
            await applicationDbContext.SaveChangesAsync();


        }
        public async Task<Category> UpdateAsync(dto_UpdateCategory categoryDto)
        {
            var existingCategory = await applicationDbContext.Categories.FindAsync(categoryDto.CategoryId);

            if (existingCategory != null)
            {
                existingCategory.CategoryName = categoryDto.CategoryName;
                return existingCategory;
            }
            return null;
        }
        public async Task<Category> DeleteAsync(int id)
        {
            var deletedCategory = await applicationDbContext.Categories.FindAsync(id);
            if (deletedCategory != null)
            {
                applicationDbContext.Categories.Remove(deletedCategory);
                await applicationDbContext.SaveChangesAsync();
                return deletedCategory;
            }
            return null;
        }
       

    }
}
