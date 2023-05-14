using Microsoft.AspNetCore.Mvc;
using Pronia_BackEnd_Project.Areas.Admin.ViewModels;
using Pronia_BackEnd_Project.Data;
using Pronia_BackEnd_Project.Helpers;
using Pronia_BackEnd_Project.Models;
using Pronia_BackEnd_Project.Services;
using Pronia_BackEnd_Project.Services.Interfaces;

namespace Pronia_BackEnd_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ITeamService _teamService;
        public TeamController(AppDbContext context, IWebHostEnvironment env, ITeamService teamService)
        {
            _context = context;
            _env = env;
            _teamService = teamService;
        }


        public async Task<IActionResult> Index()
        {
            IEnumerable<Team> teams = await _teamService.GetAllAsync();
            return View(teams);

        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!model.Photo.CheckFileType("image/"))
            {

                ModelState.AddModelError("Photo", "File type must be image");
                return View();

            }


            if (model.Photo.CheckFileSize(200))
            {

                ModelState.AddModelError("Photo", "Photo size must be max 200Kb");
                return View();

            }

            string fileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;

            string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images/website-images", fileName);

            await FileHelper.SaveFileAsync(path, model.Photo);

            Team newTeam = new()
            {
                Image = fileName,
                Name = model.Name,
                Position = model.Position

            };

            await _context.Teams.AddAsync(newTeam);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));


        }



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    Client client = await _clientService.GetFullDataByIdAsync(id);

        //    _context.Clients.Remove(client);

        //    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images/website-images", client.Image);

        //    FileHelper.DeleteFile(path);

        //    await _context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));
        //}

        //[HttpGet]
        //public async Task<IActionResult> Edit(int id)
        //{
        //    Client client = await _clientService.GetFullDataByIdAsync(id);

        //    ClientEditVM model = new()
        //    {
        //        Name = client.Name,
        //        Description = client.Description,
        //        Image = client.Image
        //    };


        //    return View(model);
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, ClientEditVM model)
        //{
        //    if (id == null) return BadRequest();

        //    Client dbClient = await _context.Clients.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

        //    if (dbClient == null) return NotFound();

        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    if (model.Photo != null)
        //    {
        //        if (!model.Photo.CheckFileType("image/"))
        //        {
        //            ModelState.AddModelError("Photo", "File type must be image");
        //            return View(model);
        //        }

        //        if (model.Photo.CheckFileSize(200))
        //        {
        //            ModelState.AddModelError("Photo", "Photo size must be max 200Kb");
        //            return View(model);
        //        }

        //        string fileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;


        //        string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images/website-images", fileName);


        //        using (FileStream stream = new(path, FileMode.Create))
        //        {
        //            await model.Photo.CopyToAsync(stream);
        //        }


        //        string expath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images/website-images", dbClient.Image);


        //        FileHelper.DeleteFile(expath);

        //        dbClient.Image = fileName;
        //    }
        //    else
        //    {
        //        dbClient.Photo = dbClient.Photo;
        //    }


        //    dbClient.Name = model.Name;
        //    dbClient.Description = model.Description;


        //    _context.Clients.Update(dbClient);

        //    await _context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));
        //}



        //[HttpGet]
        //public async Task<IActionResult> Detail(int id)
        //{
        //    if (id == null) return BadRequest();

        //    Client client = await _clientService.GetFullDataByIdAsync(id);

        //    if (client == null) return NotFound();

        //    ClientDetailVM model = new()
        //    {
        //        Name = client.Name,
        //        Description = client.Description,
        //        Image = client.Image,

        //    };

        //    return View(model);
        //}





    }
}
