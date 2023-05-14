using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia_BackEnd_Project.Data;
using Pronia_BackEnd_Project.Helpers;
using Pronia_BackEnd_Project.Models;
using Pronia_BackEnd_Project.Services.Interfaces;
using Pronia_BackEnd_Project.ViewModels;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace Pronia_BackEnd_Project.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;

        private readonly IProductService _productService;

        private readonly ICategoryService _categoryService;

        private readonly IColorService _colorService;

        private readonly ITagService _tagService;

        private readonly IBannerService _bannerService;

        private readonly IAdvertisingService _advertisingService;


        public ShopController(IProductService productService, 
            ICategoryService categoryService,
            IColorService colorService,
            ITagService tagService,
            AppDbContext context,
            IBannerService bannerService, 
            IAdvertisingService advertisingService)
        {

            _productService = productService;
            _categoryService = categoryService;
            _colorService = colorService;
            _tagService = tagService;
            _context = context;
            _bannerService = bannerService;
            _advertisingService = advertisingService;
        }
        public async Task<IActionResult> Index(int page = 1, int take = 5, int? id = null )
        {
            IEnumerable<Banner> banners = await _bannerService.GetAllAsync();

            IEnumerable<Category> categories = await _categoryService.GetAllAsync();

            IEnumerable<Color> colors = await _colorService.GetAllAsync();

            IEnumerable<Tag> tags = await _tagService.GetAllAsync();

            int pageCount=0;

            if (id != null)
            {

                pageCount =  await GetPageCounByCategoryIdtAsync(take,id);

            }
            else
            {
                 pageCount = await GetPageCountAsync(take);

            }


            IEnumerable<Product> dbproducts = await _productService.GetPaginatedDatas(page, take); //page ve take gonderirik icine hemin methoda yazilibdi Servicde orda qebul edecik 

            //IEnumerable<Product> products = await _productService.GetAllAsync();

            Paginate<Product> paginatedDatas = new(dbproducts, page, pageCount);  /// methodumuz bir generice cixartmisiq Paginate bunda her yerde istifade edecik methoda bizden 1 ci datani isdeyir mappedDatas, 2 ci page yeni curet page  3 cu ise totalPage paglerin sayini gosderen methodu gonderirik icine


            ShopVM model = new()
            {
                //Products = products,
                Categories = categories,
                Colors = colors,
                Tags = tags,
                PaginatedDatas = paginatedDatas,
                Banners = banners

            };


            return View(model);
        }



        private async Task<int> GetPageCountAsync(int take)
        {
            var productCount = await _productService.GetCountAsync();  // bu methoda mene productlarin countunu verir
            return (int)Math.Ceiling((decimal)productCount / take);     /// burda bolurki  product conutumzun nece dene take edirikse o qederde gosdersin yeni asqqidaki 1 2 3 yazir onlarin sayini tapmaq ucun 

            
        }


        private async Task<int> GetPageCounByCategoryIdtAsync(int take, int? id)
        {
            var productCount = await GetProductCountByIDAsync((int)id);  // bu methoda mene productlarin countunu verir
            return (int)Math.Ceiling((decimal)productCount / take);     /// burda bolurki  product conutumzun nece dene take edirikse o qederde gosdersin yeni asqqidaki 1 2 3 yazir onlarin sayini tapmaq ucun 

        }

        private async Task<int> GetProductCountByIDAsync(int catergoryId)
        {


            //int prodCnt = await _context.Products.Include(m=> m.ProductCategories).ThenInclude(m => m.Category;

            int prodCnt = await _context.ProductCategories.
                Include(m =>m.Product)
                .Where(m => m.Category.Id == catergoryId)
                .Select(m=>m.Product).CountAsync();

            return prodCnt;

        }
      


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();


            IEnumerable<Advertising> advertisings = await _advertisingService.GetAllAsync();

            Product product = await _productService.GetFullDataByIdAsync((int)id);

            if (product is null) return NotFound();

            return View(new ProductDetailVM  
            {

                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Rates = product.Rates,
                SaleCount = product.SaleCount,
                StockCount = product.StockCount,
                Sku = product.Sku,
                Information = product.Information,
                ProductCategories = product.ProductCategories,
                ProductImages = product.ProductImages,
                ProductColors = product.ProductColors,
                ProductSize = product.ProductSize,
                ProductTags = product.ProductTags,
                Advertisings = advertisings
               

            });


           
        }



        [HttpGet]
        public async Task<IActionResult> GetCategoryProducts(int? id , int page = 1, int take = 5)
        {
            if (id == null) return BadRequest();

            var products = await _context.ProductCategories.Where(m => m.Category.Id == id).Include(m =>m.Product).ThenInclude(m =>m.ProductImages).Select(m => m.Product)/*.Skip((page * take) - take).Take(take)*/.ToListAsync();

            ViewBag.cateId=id;

            //IEnumerable<Product> dbproducts = await _productService.GetPaginatedDatas(page, take);

            int pageCount = await GetPageCounByCategoryIdtAsync(take, id);

            Paginate<Product> paginate = new(products, page, pageCount);

            return PartialView("_ProductsPartial", paginate);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCategoriesProducts()
        {
            var products = await _context.Products.Include(m => m.ProductImages).ToListAsync();

            return PartialView("_ProductsPartial", products);
        }


        public async Task<IActionResult> MainSearch(string searchText)
        {
            var products = await _context.Products.Include(m => m.ProductImages).Include(m => m.ProductCategories).
           OrderByDescending(m => m.Id).Where(m => !m.SoftDelete && m.Name.ToLower().Trim().Contains(searchText.ToLower().Trim())).Take(6).ToListAsync();

            return View(products);
        }







    }
}
