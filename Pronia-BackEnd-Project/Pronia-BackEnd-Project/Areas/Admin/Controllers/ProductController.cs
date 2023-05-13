using Microsoft.AspNetCore.Mvc;
using Pronia_BackEnd_Project.Areas.Admin.ViewModels;
using Pronia_BackEnd_Project.Data;
using Pronia_BackEnd_Project.Helpers;
using Pronia_BackEnd_Project.Models;
using Pronia_BackEnd_Project.Services;
using Pronia_BackEnd_Project.Services.Interfaces;

namespace Pronia_BackEnd_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        private readonly ICategoryService _categoryService;

        private readonly IWebHostEnvironment _env;      

        private readonly AppDbContext _context;
        public ProductController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment env, AppDbContext context)
        {
            _productService = productService;
            _categoryService = categoryService;
            _env = env;
            _context = context;
        }




        public async Task<IActionResult> Index(int page = 1, int take = 5)  
        {
            List<Product> products = await _productService.GetPaginatedDatas(page, take); 

            List<ProductListVM> mappedDatas = GetMappedDatas(products); 

            int pageCount = await GetPageCountAsync(take); 

            Paginate<ProductListVM> paginatedDatas = new(mappedDatas, page, pageCount);  

            ViewBag.take = take;

            return View(paginatedDatas);
        }

       

        private async Task<int> GetPageCountAsync(int take)
        {
            var productCount = await _productService.GetCountAsync();  
            return (int)Math.Ceiling((decimal)productCount / take);    
        }

   
        private List<ProductListVM> GetMappedDatas(List<Product> products)
        {
            List<ProductListVM> mappedDatas = new();

            foreach (var product in products)
            {
                ProductListVM prodcutVM = new()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Count = product.StockCount,
                    CategoryName = product.ProductCategories.Select(m =>m.Category).FirstOrDefault()?.Name,
                    MainImage = product.ProductImages.Where(m => m.IsMain).FirstOrDefault()?.Image
                };

                mappedDatas.Add(prodcutVM);

            }

            return mappedDatas;

        }





       



    }
}
