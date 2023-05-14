using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia_BackEnd_Project.Areas.Admin.ViewModels;
using Pronia_BackEnd_Project.Data;
using Pronia_BackEnd_Project.Helpers;
using Pronia_BackEnd_Project.Models;
using Pronia_BackEnd_Project.Services.Interfaces;

namespace Pronia_BackEnd_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;
        private readonly ISliderService _sliderService;

        public SliderController(AppDbContext context,
                                ISliderService sliderService,
                                IWebHostEnvironment env)
        {
            _env = env;
            _context = context;
            _sliderService = sliderService;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Slider> sliders = await _sliderService.GetAllAsync();

            return View(sliders);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM slider)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return View();
                }

                if (!slider.Photo.CheckFileType("image/"))
                {

                    ModelState.AddModelError("Photos", "File type must be image");
                    return View();

                }


                if (!slider.Photo.CheckFileSize(200))
                {

                    ModelState.AddModelError("Photos", "Photo size must be max 200Kb");
                    return View();

                }






                string fileName = Guid.NewGuid().ToString() + "_" + slider.Photo.FileName;

                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images/website-images", fileName);

                await FileHelper.SaveFileAsync(path, slider.Photo);





                if (!slider.BackGroundPhoto.CheckFileType("image/"))
                {

                    ModelState.AddModelError("BackGroundPhotos", "File type must be image");
                    return View();

                }


                if (!slider.BackGroundPhoto.CheckFileSize(200))
                {

                    ModelState.AddModelError("BackGroundPhotos", "Photo size must be max 200Kb");
                    return View();

                }


                string fileBackGroundName = Guid.NewGuid().ToString() + "_" + slider.BackGroundPhoto.FileName;

                string pathBackGround = FileHelper.GetFilePath(_env.WebRootPath, "assets/images/website-images", fileBackGroundName);

                await FileHelper.SaveFileAsync(pathBackGround, slider.BackGroundPhoto);

                Slider newSlider = new()
                {
                    Image = fileName,
                    BackgroundImage = fileBackGroundName,
                    Title = slider.Title,
                    Offer = slider.Offer,
                    Description = slider.Description
                };

                await _context.Sliders.AddAsync(newSlider);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();

            Slider slider = await _sliderService.GetFullDataByIdAsync((int)id);

            if (slider == null) return NotFound();

            SliderDetailVM model = new()
            {
                Title = slider.Title,
                Description = slider.Description,
                Offer = slider.Offer,
                Image = slider.Image,
                BackGroundImage = slider.BackgroundImage
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            Slider slider = await _sliderService.GetFullDataByIdAsync((int)id);

            _context.Sliders.Remove(slider);

            string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images/website-images", slider.Image);

            FileHelper.DeleteFile(path);

            string pathBackGround = FileHelper.GetFilePath(_env.WebRootPath, "assets/images/website-images", slider.BackgroundImage);

            FileHelper.DeleteFile(pathBackGround);


            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null) return BadRequest();

            Slider dbSlider = await _sliderService.GetFullDataByIdAsync((int)id);

            if (dbSlider == null) return NotFound();


            SliderEditVM sliderEdit = new SliderEditVM()
            {

                Title = dbSlider.Title,
                Description = dbSlider.Description,
                Offer = dbSlider.Offer,
                Image = dbSlider.Image,
                BackGroundImage = dbSlider.BackgroundImage
            };



            return View(sliderEdit);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SliderEditVM newSlider)
        {
            if (id == null) return BadRequest();

            Slider slider = await _context.Sliders.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (slider == null) return NotFound();

            if (!ModelState.IsValid)
            {

                return View(newSlider);
            }

            if (newSlider.Photo != null)
            {
                if (!newSlider.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(newSlider);
                }

                if (!newSlider.Photo.CheckFileSize(200))
                {
                    ModelState.AddModelError("Photo", "Photo size must be max 200Kb");
                    return View(newSlider);
                }

                string fileName = Guid.NewGuid().ToString() + "_" + newSlider.Photo.FileName;


                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images/website-images", fileName);


                using (FileStream stream = new(path, FileMode.Create))
                {
                    await newSlider.Photo.CopyToAsync(stream);
                }


                string expath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images/website-images", slider.Image);


                FileHelper.DeleteFile(expath);

                slider.Image = fileName;
            }
            else
            {
                slider.Photo = slider.Photo;
            }


            if (newSlider.PhotoBackGround != null)
            {
                if (!newSlider.PhotoBackGround.CheckFileType("image/"))
                {
                    ModelState.AddModelError("BackGroundPhoto", "File type must be image");
                    return View(newSlider);
                }


                if (!newSlider.PhotoBackGround.CheckFileSize(200))
                {
                    ModelState.AddModelError("BackGroundPhoto", "Photo size must be max 200Kb");
                    return View(slider);
                }

                string fileBackGroundName = Guid.NewGuid().ToString() + "_" + newSlider.PhotoBackGround.FileName;

                string pathBackGround = FileHelper.GetFilePath(_env.WebRootPath, "assets/images/website-images", fileBackGroundName);

                using (FileStream stream = new(pathBackGround, FileMode.Create))
                {
                    await newSlider.PhotoBackGround.CopyToAsync(stream);
                }

                string expathBackGround = FileHelper.GetFilePath(_env.WebRootPath, "assets/images/website-images", slider.BackgroundImage);

                FileHelper.DeleteFile(expathBackGround);

                slider.BackgroundImage = fileBackGroundName;
            }
            else
            {
                slider.BackGroundPhoto = slider.Photo;
            }





            slider.Title = newSlider.Title;
            slider.Description = newSlider.Description;
            slider.Offer = newSlider.Offer;

            _context.Sliders.Update(slider);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
