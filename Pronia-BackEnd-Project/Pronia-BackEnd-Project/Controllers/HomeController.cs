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
        private readonly IAdvertisingService _advertisingService;
        private readonly IClientService _clientService;
      

        private readonly IBlogService _blogService;

        public HomeController(AppDbContext context,
            ISliderService sliderService,
            IAdvertisingService advertisingService,
            IClientService clientService,
            IBrandService brandService,
            IBlogService blogService)
        {
            _context = context;
            _sliderService = sliderService;
            _advertisingService = advertisingService;
            _clientService = clientService;
          
            _blogService = blogService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Slider> sliders = await _sliderService.GetAllAsync();
            //IEnumerable<Advertising> advertisings = await _advertisingService.GetAllAsync();
            IEnumerable<Client> clients = await _clientService.GetAllAsync();
         

            IEnumerable<Blog> blogs = await _blogService.GetAllAsync();

            HomeVM model = new()
            {
                Sliders = sliders,
                //Advertisings = advertisings,
                Clients = clients,
             
                Blogs = blogs

            };

            return View(model);
        }

        

        
    }
}