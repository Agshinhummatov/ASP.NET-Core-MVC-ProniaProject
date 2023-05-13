using Microsoft.EntityFrameworkCore;
using Pronia_BackEnd_Project.Data;
using Pronia_BackEnd_Project.Models;
using Pronia_BackEnd_Project.Services.Interfaces;

namespace Pronia_BackEnd_Project.Services
{
    public class SizeService :ISizeService
    {
        private readonly AppDbContext _context;
        public SizeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Size>> GetAllAsync() => await _context.Sizes.Include(m=>m.ProductSize).ThenInclude(m=>m.Product).Where(m => !m.SoftDelete)?.ToListAsync();


        public async Task<Size> GetByIdAsync(int id) => await _context.Sizes.FindAsync(id);

    }
}
