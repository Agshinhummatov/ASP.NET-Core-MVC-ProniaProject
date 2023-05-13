using Microsoft.EntityFrameworkCore;
using Pronia_BackEnd_Project.Data;
using Pronia_BackEnd_Project.Models;
using Pronia_BackEnd_Project.Services.Interfaces;

namespace Pronia_BackEnd_Project.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync() => await _context.Products.Include(m => m.ProductImages).
            Include(m => m.ProductCategories).
            ThenInclude(m => m.Category).
            Include(m => m.ProductTags).
            ThenInclude(m => m.Tag).
            Include(m => m.ProductColors).
            ThenInclude(m =>m.Color).
            Include(m => m.ProductSize).
            ThenInclude(m => m.Size).ToListAsync();



        public async Task<Product> GetByIdAsync(int id) => await _context.Products.FindAsync(id);


        public async Task<Product> GetFullDataByIdAsync(int id) => await _context.Products.Include(m => m.ProductImages).
            Include(m => m.ProductCategories).
            ThenInclude(m => m.Category).
            Include(m => m.ProductTags).
            ThenInclude(m => m.Tag).
            Include(m => m.ProductColors).
            ThenInclude(m => m.Color).
            Include(m => m.ProductSize).
            ThenInclude(m => m.Size)?.FirstOrDefaultAsync(m => m.Id == id);



        public async Task<List<Product>> GetPaginatedDatas(int page, int take)  
        {
            return await _context.Products.Include(m => m.ProductImages).
            Include(m => m.ProductCategories).
            ThenInclude(m => m.Category).
            Include(m => m.ProductTags).
            ThenInclude(m => m.Tag).
            Include(m => m.ProductColors).
            ThenInclude(m => m.Color).
            Include(m => m.ProductSize).
            ThenInclude(m => m.Size).Skip((page * take) - take).Take(take).ToListAsync();  
        }


        public async Task<int> GetCountAsync()   
        {
            return await _context.Products.CountAsync(); 
        }

    }
}
