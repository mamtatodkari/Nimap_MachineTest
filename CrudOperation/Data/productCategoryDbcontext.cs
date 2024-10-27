using CrudOperation.Models.Model;
using Microsoft.EntityFrameworkCore;

namespace CrudOperation.Data
{
    public class productCategoryDbcontext:DbContext
    {
        public productCategoryDbcontext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<Category>Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
