using Microsoft.AspNetCore.Mvc;

namespace Pronia_BackEnd_Project.Areas.Admin.Controllers
{
    public class BlogAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
