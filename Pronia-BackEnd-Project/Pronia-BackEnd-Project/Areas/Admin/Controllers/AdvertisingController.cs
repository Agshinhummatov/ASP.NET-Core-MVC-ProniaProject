using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia_BackEnd_Project.Areas.Admin.ViewModels;
using Pronia_BackEnd_Project.Data;
using Pronia_BackEnd_Project.Helpers;
using Pronia_BackEnd_Project.Helpers.Enums;
using Pronia_BackEnd_Project.Models;
using Pronia_BackEnd_Project.Services.Interfaces;

namespace Pronia_BackEnd_Project.Areas.Admin.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    [Area("Admin")]
    public class AdvertisingController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;
        private readonly IAdvertisingService _advertisingService;

        public AdvertisingController(AppDbContext context,
                                IAdvertisingService advertisingService,
                                IWebHostEnvironment env)
        {
            _env = env;
            _context = context;
            _advertisingService = advertisingService;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            IEnumerable<Advertising> advertisings = await _advertisingService.GetAllAsync();


            return View(advertisings);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdvertisingCreateVM model)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                if (!model.Photo.CheckFileType("image/"))
                {

                    ModelState.AddModelError("Photos", "File type must be image");
                    return View();

                }


                if (!model.Photo.CheckFileSize(200))
                {

                    ModelState.AddModelError("Photos", "Photo size must be max 200Kb");
                    return View();

                }

                string fileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;

                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images/website-images", fileName);

                await FileHelper.SaveFileAsync(path, model.Photo);


                Advertising advertising = new()
                {
                    Image = fileName,
                    Name = model.Name,
                    Description = model.Description
                };

                await _context.Advertisings.AddAsync(advertising);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Advertising advertising = await _advertisingService.GetFullDataByIdAsync(id);

            _context.Advertisings.Remove(advertising);

            string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images/website-images", advertising.Image);

            FileHelper.DeleteFile(path);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Advertising dbAdvertising = await _advertisingService.GetFullDataByIdAsync(id);

            AdvertisingEditVM model = new()
            {
                Name = dbAdvertising.Name,
                Description = dbAdvertising.Description,
                Image = dbAdvertising.Image
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, AdvertisingEditVM model)
        {
            if (id == null) return BadRequest();

            Advertising dbAdvertising = await _context.Advertisings.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (dbAdvertising == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Photo != null)
            {
                if (!model.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(model);
                }

                if (!model.Photo.CheckFileSize(200))
                {
                    ModelState.AddModelError("Photo", "Photo size must be max 200Kb");
                    return View(model);
                }

                string fileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;


                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images/website-images", fileName);


                using (FileStream stream = new(path, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(stream);
                }


                string expath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images/website-images", dbAdvertising.Image);


                FileHelper.DeleteFile(expath);

                dbAdvertising.Image = fileName;
            }
            else
            {
                dbAdvertising.Photo = dbAdvertising.Photo;
            }


            dbAdvertising.Name = model.Name;
            dbAdvertising.Description = model.Description;


            _context.Advertisings.Update(dbAdvertising);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();

            Advertising advertising = await _advertisingService.GetFullDataByIdAsync((int)id);

            if (advertising == null) return NotFound();

            AdvertisingDetailVM model = new()
            {
                Name = advertising.Name,
                Description = advertising.Description,
                Image = advertising.Image,

            };

            return View(model);
        }
    }



}
