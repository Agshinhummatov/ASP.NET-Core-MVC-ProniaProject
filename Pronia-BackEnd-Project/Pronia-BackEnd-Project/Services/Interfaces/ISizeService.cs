using Pronia_BackEnd_Project.Models;

namespace Pronia_BackEnd_Project.Services.Interfaces
{
    public interface ISizeService
    {

        Task<IEnumerable<Size>> GetAllAsync();

        Task<Size> GetByIdAsync(int id);
    }
}
