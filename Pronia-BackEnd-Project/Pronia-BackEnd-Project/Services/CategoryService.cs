using Microsoft.EntityFrameworkCore;
using Pronia_BackEnd_Project.Data;
using Pronia_BackEnd_Project.Models;
using Pronia_BackEnd_Project.Services.Interfaces;

namespace Pronia_BackEnd_Project.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync() => await _context.Categories.Include(m => m.ProductCategories).
            ThenInclude(m => m.Product).
            Where(m => !m.SoftDelete).ToListAsync();

        public async Task<Category> GetByIdAsync(int id) => await _context.Categories.FindAsync(id);

    }
}
