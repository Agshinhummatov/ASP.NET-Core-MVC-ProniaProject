using Microsoft.EntityFrameworkCore;
using Pronia_BackEnd_Project.Data;
using Pronia_BackEnd_Project.Models;
using Pronia_BackEnd_Project.Services.Interfaces;

namespace Pronia_BackEnd_Project.Services
{
    public class BrandService:IBrandService
    {
        private readonly AppDbContext _context;
        public BrandService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Brand>> GetAllAsync() => await _context.Brands.Where(m => !m.SoftDelete).ToListAsync();

        public async Task<Brand> GetByIdAsync(int id) => await _context.Brands.FindAsync(id);

    }
}
