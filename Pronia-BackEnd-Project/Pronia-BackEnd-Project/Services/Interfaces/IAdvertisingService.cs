using Pronia_BackEnd_Project.Models;

namespace Pronia_BackEnd_Project.Services.Interfaces
{
    public interface IAdvertisingService
    {
        Task<IEnumerable<Advertising>> GetAllAsync();

        Task<Advertising> GetByIdAsync(int id);
    }
}
