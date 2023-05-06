using Microsoft.AspNetCore.Mvc;
using Pronia_BackEnd_Project.Models;
using Pronia_BackEnd_Project.Services.Interfaces;
using Pronia_BackEnd_Project.ViewModels;

namespace Pronia_BackEnd_Project.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;

        private readonly ICategoryService _categoryService;

        public ShopController(IProductService productService, ICategoryService categoryService)
        {

            _productService = productService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {

            IEnumerable<Product> products = await _productService.GetAllAsync();

            IEnumerable<Category> categories = await _categoryService.GetAllAsync();

            ShopVM model = new()
            {
                Products = products,
                Categories = categories

            };


            return View(model);
        }
    }
}
