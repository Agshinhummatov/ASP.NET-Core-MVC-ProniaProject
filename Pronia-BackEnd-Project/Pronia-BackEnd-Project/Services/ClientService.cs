using Microsoft.EntityFrameworkCore;
using Pronia_BackEnd_Project.Data;
using Pronia_BackEnd_Project.Models;
using Pronia_BackEnd_Project.Services.Interfaces;

namespace Pronia_BackEnd_Project.Services
{
    public class ClientService : IClientService
    {
        private readonly AppDbContext _context;
        public ClientService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Client>> GetAllAsync() => await _context.Clients.Where(m => !m.SoftDelete).ToListAsync();


        public async  Task<Client> GetFullDataByIdAsync(int id) => await _context.Clients.FindAsync(id);

    }
}
