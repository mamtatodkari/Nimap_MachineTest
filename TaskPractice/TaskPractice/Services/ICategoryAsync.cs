using TaskPractice.Data.dto;
using TaskPractice.Data.Model;

namespace TaskPractice.Services
{
    public interface ICategoryAsync
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task AddAsync(dto_Category categoryDto);
        Task<Category> UpdateAsync(dto_UpdateCategory categoryDto);
        Task<Category> DeleteAsync(int id);

    }
}
