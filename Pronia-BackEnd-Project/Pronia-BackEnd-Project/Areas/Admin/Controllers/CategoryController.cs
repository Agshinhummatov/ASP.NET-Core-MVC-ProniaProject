using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia_BackEnd_Project.Areas.Admin.ViewModels;
using Pronia_BackEnd_Project.Data;
using Pronia_BackEnd_Project.Models;
using Pronia_BackEnd_Project.Services.Interfaces;

namespace Pronia_BackEnd_Project.Areas.Admin.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;
        private readonly ICategoryService _categoryService;

        public CategoryController(AppDbContext context,
                                ICategoryService categoryService,
                                IWebHostEnvironment env)
        {
            _env = env;
            _context = context;
            _categoryService = categoryService;

        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> categories = await _categoryService.GetAllAsync();

            List<CategoryIndexVM> model = new();

            foreach (var category in categories)
            {
                CategoryIndexVM mappedData = new()
                {
                    Id = category.Id,
                    Name = category.Name,
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
        public async Task<IActionResult> Create(CategoryCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Category category = new()
            {
                Name = model.Name
            };

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Category category = await _categoryService.GetByIdAsync(id);

            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Category category = await _categoryService.GetByIdAsync(id);

            CategoryEditVM model = new()
            {
                Name = category.Name
            };


            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CategoryEditVM model)
        {
            if (id == null) return BadRequest();

            Category category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (category == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            category.Name = model.Name;


            _context.Categories.Update(category);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }



}
