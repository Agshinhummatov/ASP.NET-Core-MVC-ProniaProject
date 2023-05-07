using Microsoft.EntityFrameworkCore;
using Pronia_BackEnd_Project.Data;
using Pronia_BackEnd_Project.Models;
using Pronia_BackEnd_Project.Services.Interfaces;

namespace Pronia_BackEnd_Project.Services
{
    public class TagService : ITagService
    {

        private readonly AppDbContext _context;
        public TagService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync() => await _context.Tags.Where(m => !m.SoftDelete).ToListAsync();


        public  async Task<Tag> GetByIdAsync(int id) => await _context.Tags.FindAsync(id);



    }
}
