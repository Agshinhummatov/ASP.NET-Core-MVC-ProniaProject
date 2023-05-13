using Pronia_BackEnd_Project.Data;
using Pronia_BackEnd_Project.Services.Interfaces;

namespace Pronia_BackEnd_Project.Services
{
    public class SettingsService : ISettingsService
    {

        private readonly AppDbContext _context;
        public SettingsService(AppDbContext context)
        {
            _context = context;
        }


       public  Dictionary<string, string> SettingAll() => _context.Settings.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);


    }
}
