using TaskPractice.Data.dto;
using TaskPractice.Data.Model;

namespace TaskPractice.Services
{
    public class Category1:ICategory
    {
        private readonly ApplicationDbContext _dbContext;

        public Category1(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Category> GetAll()
        {
            return _dbContext.Categories.ToList();
        }

        public Category GetById(int id)
        {
            return _dbContext.Categories.Find(id);
        }

        public void Add(dto_Category categoryDto)
        {
            var category = new Category
            {
                CategoryName = categoryDto.CategoryName
            };
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
        }

        public Category Update(dto_UpdateCategory categoryDto)
        {
            var category = _dbContext.Categories.Find(categoryDto.CategoryId);
            if (category != null)
            {
                category.CategoryName = categoryDto.CategoryName;
                _dbContext.SaveChanges();
                return category;
            }
            return null;
        }

        public Category Delete(int id)
        {
            var category = _dbContext.Categories.Find(id);
            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                _dbContext.SaveChanges();
                return category;
            }
            return null;
        }
    }
}

