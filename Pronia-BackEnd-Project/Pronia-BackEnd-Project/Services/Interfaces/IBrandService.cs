using Pronia_BackEnd_Project.Models;

namespace Pronia_BackEnd_Project.Services.Interfaces
{
    public interface IBrandService
    {
        Task<Brand> GetByIdAsync(int id);
        Task<IEnumerable<Brand>> GetAllAsync();
    }
}
