using Microsoft.AspNetCore.Mvc;
using Pronia_BackEnd_Project.Data;
using Pronia_BackEnd_Project.Models;
using Pronia_BackEnd_Project.Services.Interfaces;
using Pronia_BackEnd_Project.ViewModels;

namespace Pronia_BackEnd_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISliderService _sliderService;
       
        private readonly IClientService _clientService;
        private readonly IBannerService _bannerService;

        private readonly IBlogService _blogService;
        private readonly IProductService _productService;

        public HomeController(AppDbContext context,
            ISliderService sliderService,
            IAdvertisingService advertisingService,
            IClientService clientService,
            IBrandService brandService,
            IBlogService blogService,
            IBannerService bannerService, IProductService productService)
        {
            _context = context;
            _sliderService = sliderService;
           
            _clientService = clientService;
          
            _blogService = blogService;
            _bannerService = bannerService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Slider> sliders = await _sliderService.GetAllAsync();
         
            IEnumerable<Client> clients = await _clientService.GetAllAsync();

            IEnumerable<Banner> banners = await _bannerService.GetAllAsync();
         

            IEnumerable<Blog> blogs = await _blogService.GetAllAsync();

            IEnumerable<Product> products = await _productService.GetAllAsync();


            HomeVM model = new()
            {
                Sliders = sliders,
                
                Clients = clients,
             
                Blogs = blogs,
                Banners = banners,
                Product = products

            };

            return View(model);
        }

        

        
    }
}