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



        public async Task<IEnumerable<Product>> GetPaginatedDatas(int page, int take)  //bu method seyfeye uygun olaraq (int page ) data bazadan (int take) qeder datani gpturub gelir 
        {
            return await _context.Products.Include(m => m.ProductImages).
            Include(m => m.ProductCategories).
            ThenInclude(m => m.Category).
            Include(m => m.ProductTags).
            ThenInclude(m => m.Tag).
            Include(m => m.ProductColors).
            ThenInclude(m => m.Color).
            Include(m => m.ProductSize).
            ThenInclude(m => m.Size).Skip((page * take) - take).Take(take).ToListAsync();  // (page*take) - take)  vurub taki cixirkiqki hemise  skipe elediyim qeder onu gosdersin novebetilerde nedise diger seyfede onu gosdersin yeni bir seyfede 5 product geldi digerinde 6 dan baslasin 10 kimi gosdersin bu mentiqde 
        }


        public async Task<int> GetCountAsync()   // productlarin sayini tapmaq ucun yazdiqimiz methodur adinda Async vermisiki asixron olsun
        {
            return await _context.Products.CountAsync(); // CountAsync() ssitemin verdiyi methodur
        }

    }
}
