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
    public class ColorController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;
        private readonly IColorService _colorService;

        public ColorController(AppDbContext context,
                               IColorService colorService,
                                IWebHostEnvironment env)
        {
            _env = env;
            _context = context;
            _colorService = colorService;

        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Color> colors = await _colorService.GetAllAsync();

            List<ColorIndexVM> model = new();

            foreach (var color in colors)
            {
                ColorIndexVM mappedData = new()
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
        public async Task<IActionResult> Create(ColorCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Color color = new()
            {
                Name = model.Name
            };

            await _context.Colors.AddAsync(color);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Color color = await _colorService.GetByIdAsync(id);

            _context.Colors.Remove(color);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Color color = await _colorService.GetByIdAsync(id);

            ColorEditVM model = new()
            {
                Name = color.Name
            };


            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ColorEditVM model)
        {
            if (id == null) return BadRequest();

            Color color = await _context.Colors.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (color == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            color.Name = model.Name;


            _context.Colors.Update(color);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
