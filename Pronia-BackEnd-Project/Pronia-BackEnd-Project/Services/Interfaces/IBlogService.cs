using Pronia_BackEnd_Project.Models;

namespace Pronia_BackEnd_Project.Services.Interfaces
{
    public interface IBlogService
    {
        Task<IEnumerable<Blog>> GetAllAsync();

        Task<Blog> GetByIdAsync(int id);

        Task<Blog> GetFullDataById(int id);
    }
}
