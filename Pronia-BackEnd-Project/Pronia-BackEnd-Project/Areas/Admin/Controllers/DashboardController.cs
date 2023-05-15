using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pronia_BackEnd_Project.Helpers.Enums;

namespace Pronia_BackEnd_Project.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        [Authorize(Roles = "SuperAdmin,Admin")]
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
