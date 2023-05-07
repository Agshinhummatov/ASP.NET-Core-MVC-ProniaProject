using Pronia_BackEnd_Project.Models;

namespace Pronia_BackEnd_Project.Services.Interfaces
{
    public interface IColorService
    {

        Task<IEnumerable<Color>> GetAllAsync();

        Task<Color> GetByIdAsync(int id);

    }
}
