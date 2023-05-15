using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia_BackEnd_Project.Areas.Admin.ViewModels;
using Pronia_BackEnd_Project.Data;
using Pronia_BackEnd_Project.Helpers.Enums;
using Pronia_BackEnd_Project.Models;
using Pronia_BackEnd_Project.Services.Interfaces;

namespace Pronia_BackEnd_Project.Areas.Admin.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    [Area("Admin")]
    public class SizeController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;
        private readonly ISizeService _sizeService;

        public SizeController(AppDbContext context,
                              ISizeService sizeService,
                                IWebHostEnvironment env)
        {
            _env = env;
            _context = context;
            _sizeService = sizeService;

        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Size> sizes = await _sizeService.GetAllAsync();

            List<SizeIndexVM> model = new();

            foreach (var color in sizes)
            {
                SizeIndexVM mappedData = new()
                {
                    Id = color.Id,
                    Name = color.Name,
                };

                model.Add(mappedData);
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SizeCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Size size = new()
            {
                Name = model.Name
            };

            await _context.Sizes.AddAsync(size);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Size size = await _sizeService.GetByIdAsync(id);

            _context.Sizes.Remove(size);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Size size = await _sizeService.GetByIdAsync(id);

            SizeEditVM model = new()
            {
                Name = size.Name
            };


            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SizeEditVM model)
        {

            if (id == null) return BadRequest();

            Size size = await _context.Sizes.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (size == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            size.Name = model.Name;


            _context.Sizes.Update(size);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }

}
