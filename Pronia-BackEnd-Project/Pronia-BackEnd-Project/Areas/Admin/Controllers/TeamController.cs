using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia_BackEnd_Project.Areas.Admin.ViewModels;
using Pronia_BackEnd_Project.Data;
using Pronia_BackEnd_Project.Helpers;
using Pronia_BackEnd_Project.Helpers.Enums;
using Pronia_BackEnd_Project.Models;
using Pronia_BackEnd_Project.Services;
using Pronia_BackEnd_Project.Services.Interfaces;

namespace Pronia_BackEnd_Project.Areas.Admin.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin")]
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


            if (!model.Photo.CheckFileSize(200))
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null) return BadRequest();

                Team team = await _context.Teams.FirstOrDefaultAsync(m => m.Id == id);

                if (team is null) return NotFound();

                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images/website-images", team.Image);

                FileHelper.DeleteFile(path);

                _context.Teams.Remove(team);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            Team team = await _context.Teams.FirstOrDefaultAsync(m => m.Id == id);

            if (team is null) return NotFound();

            TeamEditVM model = new()
            {
                Id = team.Id,
                Name = team.Name,
                Position = team.Position,
                Image = team.Image
            };

            return View(model);

        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();

            Team team = await _context.Teams.FirstOrDefaultAsync(m => m.Id == id);

            if (team == null) return NotFound();

            return View(team);

        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, TeamEditVM team)
        {

            try
            {
                if (id == null) return BadRequest();

                Team dbTeam = await _context.Teams.FirstOrDefaultAsync(m => m.Id == id);

                if (team is null) return NotFound();

                if (!ModelState.IsValid)
                {
                    team.Image = dbTeam.Image;

                    return View(team);
                }


                TeamEditVM model = new()
                {
                    Id = team.Id,
                    Name = team.Name,
                    Position = team.Position,
                    Image = team.Image
                };

                if (team.Photo != null)
                {
                    if (!team.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(dbTeam);
                    }

                    if (!team.Photo.CheckFileSize(200))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 200kb");
                        return View(dbTeam);
                    }

                    string oldPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images/website-images", dbTeam.Image);

                    FileHelper.DeleteFile(oldPath);

                    string fileName = Guid.NewGuid().ToString() + "-" + team.Photo.FileName;

                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images/website-images", fileName);

                    await FileHelper.SaveFileAsync(newPath, team.Photo);

                    dbTeam.Image = fileName;
                }
                else
                {
                    Team newTeam = new()
                    {
                        Image = dbTeam.Image
                    };
                }

                dbTeam.Name = team.Name;
                dbTeam.Position = dbTeam.Position;


                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {

                throw;
            }


        }









    }
}
