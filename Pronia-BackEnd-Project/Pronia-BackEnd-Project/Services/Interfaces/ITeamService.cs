using Pronia_BackEnd_Project.Models;

namespace Pronia_BackEnd_Project.Services.Interfaces
{
    public interface ITeamService
    {
        Task<IEnumerable<Team>> GetAllAsync();
    }
}
