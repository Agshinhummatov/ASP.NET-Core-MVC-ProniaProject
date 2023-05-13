using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        private readonly ISizeService _sizeService;

        private readonly ITagService _tagService;

        private readonly IColorService _colorService;

        private readonly IWebHostEnvironment _env;      

        private readonly AppDbContext _context;
        public ProductController(IProductService productService,
                                 ICategoryService categoryService,
                                       IWebHostEnvironment env,
            AppDbContext context, 
            ISizeService sizeService,
            ITagService tagService,
            IColorService colorService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _env = env;
            _context = context;
            _sizeService = sizeService;
            _tagService = tagService;
            _colorService = colorService;
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



        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.categories = await GetCategoriesAsync();

            ViewBag.sizes = await GetSiziesAsync();

            ViewBag.colors = await GetColorsAsync();

            ViewBag.tags = await GetTagsAsync();

            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdcutCreateVM model)
        {
            try
            {
                IEnumerable<Product> products = await _productService.GetAllAsync();

                //ViewBag.categories = new SelectList(categories, "Id", "Name");  //httpPost methodunda yazirkiqki frumuz submit olanda yeni refresh olanda hemin seletimiz ordan getmesin view bag ile gonderirik datani


                ViewBag.categories = await GetCategoriesAsync();

                ViewBag.tags = await GetTagsAsync();

                ViewBag.sizes = await GetSiziesAsync();

                ViewBag.colors = await GetColorsAsync();

                if (!ModelState.IsValid)
                {
                    return View(model); //  is validi yoxlayirki bos olmasin ve view icine bize gelen model  gonderiki eger biri sehv olarsa inputlari bos saxlamasin
                }


                foreach (var photo in model.Photos)
                {

                    if (!photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View();
                    }

                    if (!photo.CheckFileSize(200))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 200kb");
                        return View();
                    }



                }

                List<ProductTag> tags = new();

                foreach (var product in model.ProductTags)
                {

                    ProductTag tag = new()
                    {
                        TagId = product
                    };

                    tags.Add(tag);
                }


                List<ProductColor> colors = new();

                foreach (var product in model.ProductColors)
                {

                    ProductColor color = new()
                    {
                        ColorId = product
                    };

                    colors.Add(color);
                }


                List<ProductSize> sizes = new();

                foreach (var product in model.ProductSize)
                {

                    ProductSize size = new()
                    {
                        SizeId = product
                    };

                    sizes.Add(size);
                }


                List<ProductCategory> categories = new();

                foreach (var product in model.ProductCategories)
                {

                    ProductCategory category = new()
                    {
                        CategoryId = product
                    };

                    categories.Add(category);
                }


                List<ProductImage> productImages = new();  // list yaradiriq  burda hemin listada asqi methoda add edecik imagleri

                foreach (var photo in model.Photos)
                {
                    string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName; // Guid.NewGuid() bu neynir bir id kimi dusune birerik hemise ferqli herifler verir mene ki men sekilin name qoyanda o ferqli olsun tostring ele deyirem yeni random oalraq ferlqi ferqli sekil adi gelecek  ve  slider.Photo.FileName; ordan gelen ada birslerdir 


                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images/website-images", fileName);

                    await FileHelper.SaveFlieAsync(path, photo);


                    ProductImage productImage = new()   // bir bir sekileri goturur forech icinde
                    {
                        Image = fileName
                    };

                    productImages.Add(productImage); // yuxardaki  List<ProductImage> add edir sekileri yeni nece dene sekili varsa o qederde add edecek

                }

                productImages.FirstOrDefault().IsMain = true; // bu neynir elimizdeki list var icinde imagler var gelir onlardan biricsin defaltunu ture edirki productlarda 1 ci sekili gosdersin

                decimal convertedPrice = decimal.Parse(model.Price); 

                Product newProduct = new()
                {
                    Name = model.Name,      
                    Price = convertedPrice, 
                    SaleCount = model.SaleCount, 
                    Sku = model.Sku,
                    StockCount = model.StockCount,
                    Information = model.Information,
                    Description = model.Description,
                    ProductCategories = categories,
                    ProductSize = sizes,
                    ProductColors = colors,
                    ProductTags = tags,
                    ProductImages = productImages,
                    
                    
                };

                await _context.ProductImages.AddRangeAsync(productImages); // AddRangeAsync bu method bize listi yigir add edir 
                await _context.Products.AddAsync(newProduct);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }







        private async Task<SelectList> GetCategoriesAsync()
        {

            IEnumerable<Category> categories = await _categoryService.GetAllAsync();

            return new SelectList(categories, "Id", "Name"); 

        }


        private async Task<SelectList> GetSiziesAsync()
        {
            IEnumerable<Size> sizes = await _sizeService.GetAllAsync();

            return new SelectList(sizes, "Id", "Name");
        }


        private async Task<SelectList> GetColorsAsync()
        {
            IEnumerable<Color> colors = await _colorService.GetAllAsync();

            return new SelectList(colors, "Id", "Name"); 

        }



        private async Task<SelectList> GetTagsAsync()
        {
            IEnumerable<Tag> tags = await _tagService.GetAllAsync();

            return new SelectList(tags, "Id", "Name"); 

        }



    }
}
