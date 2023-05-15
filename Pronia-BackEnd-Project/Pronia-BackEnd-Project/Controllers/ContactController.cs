using Microsoft.AspNetCore.Mvc;
using Pronia_BackEnd_Project.Data;
using Pronia_BackEnd_Project.Models;
using Pronia_BackEnd_Project.ViewModels;

namespace Pronia_BackEnd_Project.Controllers
{
    public class ContactController : Controller
    {

        private readonly AppDbContext _context;
        public ContactController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ContactVM model = new();
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ContactVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);

            }

            Contact contact = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Phone = model.Phone,
                Message = model.Message
            };


            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();



            return RedirectToAction(nameof(Index));

        }


    }

}
