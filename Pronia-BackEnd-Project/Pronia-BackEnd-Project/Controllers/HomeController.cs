using Microsoft.AspNetCore.Mvc;


namespace Pronia_BackEnd_Project.Controllers
{
    public class HomeController : Controller
    {
      

        public IActionResult Index()
        {
            return View();
        }

        

        
    }
}