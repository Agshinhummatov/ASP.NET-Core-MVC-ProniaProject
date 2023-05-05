using Microsoft.AspNetCore.Mvc;

namespace Pronia_BackEnd_Project.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
