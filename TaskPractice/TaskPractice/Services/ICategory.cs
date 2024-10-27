using Microsoft.AspNetCore.Mvc;
using TaskPractice.Data.dto;
using TaskPractice.Data.Model;

namespace TaskPractice.Services
{
    public interface ICategory
    {
        IEnumerable<Category> GetAll();
        Category GetById(int id);
        void Add(dto_Category categoryDto);
        Category Update(dto_UpdateCategory categoryDto);
        Category Delete(int id);
    }
}
