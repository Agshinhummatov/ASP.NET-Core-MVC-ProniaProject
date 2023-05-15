using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia_BackEnd_Project.Areas.Admin.ViewModels;
using Pronia_BackEnd_Project.Data;
using Pronia_BackEnd_Project.Helpers.Enums;
using Pronia_BackEnd_Project.Models;

namespace Pronia_BackEnd_Project.Areas.Admin.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        public ContactController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            IEnumerable<Contact> dbContacts = await _context.Contacts.ToListAsync();

            List<ContactIndexVM> contacts = new();

            foreach (var contact in dbContacts)
            {
                ContactIndexVM model = new()
                {
                    Id = contact.Id,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Email = contact.Email
                };

                contacts.Add(model);
            }

            return View(contacts);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            Contact contact = await _context.Contacts.FindAsync((int)id);

            if (contact == null) return NotFound();

            _context.Contacts.Remove(contact);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();

            Contact contact = await _context.Contacts.FindAsync((int)id);

            if (contact == null) return NotFound();

            ContactDetailVM model = new()
            {
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
                Phone = contact.Phone,
                Message = contact.Message
            };

            return View(model);

        }


    }
}
    

