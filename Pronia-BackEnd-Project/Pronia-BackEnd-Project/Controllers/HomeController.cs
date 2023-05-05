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
        public HomeController(AppDbContext context,
            ISliderService sliderService)
        {
            _context = context;
            _sliderService = sliderService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Slider> sliders = await _sliderService.GetAll();

            HomeVM model = new()
            {
                Sliders = sliders
            };

            return View(model);
        }

        

        
    }
}