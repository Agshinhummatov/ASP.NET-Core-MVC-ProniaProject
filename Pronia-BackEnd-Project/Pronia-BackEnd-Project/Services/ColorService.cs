using Microsoft.EntityFrameworkCore;
using Pronia_BackEnd_Project.Data;
using Pronia_BackEnd_Project.Models;
using Pronia_BackEnd_Project.Services.Interfaces;

namespace Pronia_BackEnd_Project.Services
{
    public class ColorService : IColorService
    {
        private readonly AppDbContext _context;
        public ColorService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Color>> GetAllAsync() => await _context.Colors.Include(m => m.ProductColors).ThenInclude(m =>m.Product).Where(m => !m.SoftDelete)?.ToListAsync();

        public async Task<Color> GetByIdAsync(int id) => await _context.Colors.FindAsync(id);


    }
}
