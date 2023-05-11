using Microsoft.EntityFrameworkCore;
using Pronia_BackEnd_Project.Data;
using Pronia_BackEnd_Project.Models;
using Pronia_BackEnd_Project.Services.Interfaces;

namespace Pronia_BackEnd_Project.Services
{
    public class TeamService :ITeamService
    {

        private readonly AppDbContext _context;
        public TeamService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Team>> GetAllAsync() => await _context.Teams.Where(m => !m.SoftDelete).ToListAsync();

    }
}
