using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia_BackEnd_Project.Areas.Admin.ViewModels;
using Pronia_BackEnd_Project.Data;
using Pronia_BackEnd_Project.Models;
using Pronia_BackEnd_Project.Services.Interfaces;

namespace Pronia_BackEnd_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;
        private readonly IAuthorService _authorService;

        public AuthorController(AppDbContext context,
                                IAuthorService authorService,
                                IWebHostEnvironment env)
        {
            _env = env;
            _context = context;
            _authorService = authorService;

        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Author> authors = await _authorService.GetAllAsync();

            List<AuthorIndexVM> model = new();

            foreach (var author in authors)
            {
                AuthorIndexVM mappedData = new()
                {
                    Id = author.Id,
                    Name = author.Name,
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
        public async Task<IActionResult> Create(AuthorCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Author author = new()
            {
                Name = model.Name
            };

            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Author author = await _authorService.GetByIdAsync(id);

            _context.Authors.Remove(author);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Author author = await _authorService.GetByIdAsync(id);

            AuthorEditVM model = new()
            {
                Name = author.Name
            };


            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, AuthorEditVM model)
        {
            if (id == null) return BadRequest();

            Author author = await _context.Authors.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (author == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            author.Name = model.Name;


            _context.Authors.Update(author);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
