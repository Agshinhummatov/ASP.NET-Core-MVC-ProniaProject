using Microsoft.EntityFrameworkCore;
using Pronia_BackEnd_Project.Data;
using Pronia_BackEnd_Project.Models;
using Pronia_BackEnd_Project.Services.Interfaces;

namespace Pronia_BackEnd_Project.Services
{
    public class BannerService : IBannerService
    {
        private readonly AppDbContext _context;

        public BannerService(AppDbContext context)
        {
            _context = context; 
        }
        public async Task<IEnumerable<Banner>> GetAllAsync() => await _context.Banners.Where(s => !s.SoftDelete).ToListAsync();
        
    }
}
