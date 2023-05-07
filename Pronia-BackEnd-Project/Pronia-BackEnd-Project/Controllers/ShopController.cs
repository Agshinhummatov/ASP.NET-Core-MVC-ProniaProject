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

        private readonly IColorService _colorService;

        private readonly ITagService _tagService;

        public ShopController(IProductService productService, ICategoryService categoryService, IColorService colorService, ITagService tagService)
        {

            _productService = productService;
            _categoryService = categoryService;
            _colorService = colorService;
            _tagService = tagService;
        }
        public async Task<IActionResult> Index()
        {

            IEnumerable<Product> products = await _productService.GetAllAsync();

            IEnumerable<Category> categories = await _categoryService.GetAllAsync();

            IEnumerable<Color> colors = await _colorService.GetAllAsync();

            IEnumerable<Tag> tags = await _tagService.GetAllAsync();

            ShopVM model = new()
            {
                Products = products,
                Categories = categories,
                Colors = colors,
                Tags = tags


            };


            return View(model);
        }
    }
}
