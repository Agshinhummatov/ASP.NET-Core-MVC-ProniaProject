using Microsoft.AspNetCore.Mvc;
using Pronia_BackEnd_Project.Models;
using Pronia_BackEnd_Project.Services;
using Pronia_BackEnd_Project.Services.Interfaces;
using Pronia_BackEnd_Project.ViewModels;

namespace Pronia_BackEnd_Project.Controllers
{
    public class AboutController : Controller
    {
        private readonly ITeamService _teamService;

        public AboutController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Team> teams = await _teamService.GetAllAsync();

            AboutVM model = new()
            {
                Teams = teams
            };
            return View(model);
        }
    }
}
