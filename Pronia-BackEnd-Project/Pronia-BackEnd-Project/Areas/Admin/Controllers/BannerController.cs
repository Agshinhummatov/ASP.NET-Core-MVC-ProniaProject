using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia_BackEnd_Project.Data;
using Pronia_BackEnd_Project.Models;
using Pronia_BackEnd_Project.Services.Interfaces;

namespace Pronia_BackEnd_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BannerController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IBannerService _bannerService;
        public BannerController(AppDbContext context, IWebHostEnvironment env, IBannerService bannerService)
        {
            _context = context;
            _env = env;
            _bannerService = bannerService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Banner> banners = await _bannerService.GetAllAsync();

            return View(banners);
        }





        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null) return BadRequest();
  
     
            Banner banner = await _bannerService.GetByIdAsync((int)id);
       
            if (banner is null) return NotFound();
       

            return View(banner);
        }


        [HttpGet]
        public IActionResult Create()     
        {
            return View();
        }





    }
}
