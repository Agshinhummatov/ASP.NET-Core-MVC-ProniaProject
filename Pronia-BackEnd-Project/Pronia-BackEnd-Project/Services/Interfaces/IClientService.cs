using Pronia_BackEnd_Project.Models;

namespace Pronia_BackEnd_Project.Services.Interfaces
{
    public interface IClientService
    {
        Task<Client> GetByIdAsync(int id);
        Task<IEnumerable<Client>> GetAllAsync();
    }
}
