using Microsoft.EntityFrameworkCore;
using Pronia_BackEnd_Project.Models;

namespace Pronia_BackEnd_Project.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Slider> Sliders { get; set; }


    }
}
