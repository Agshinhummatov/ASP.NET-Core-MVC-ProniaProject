using Pronia_BackEnd_Project.Models;

namespace Pronia_BackEnd_Project.Services.Interfaces
{
    public interface ITagService
    {

        Task<IEnumerable<Tag>> GetAllAsync();

        Task<Tag> GetByIdAsync(int id);
    }
}
