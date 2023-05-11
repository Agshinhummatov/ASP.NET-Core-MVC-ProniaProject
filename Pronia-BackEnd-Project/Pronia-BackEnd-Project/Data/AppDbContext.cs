using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pronia_BackEnd_Project.Models;

namespace Pronia_BackEnd_Project.Data
{
    public class AppDbContext:IdentityDbContext<AppUser> // IdentityDbContext noralda dbcontectden miras alir bunu yazanda ise login registir isleyir // AppUser de olsun icinde yeni full name ve yaxud basqa nese var o
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
        
        public DbSet<Slider> Sliders { get; set; }

        public DbSet<Advertising> Advertisings { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Blog> Blogs { get; set; }


        public DbSet<BlogImage> BlogImages { get; set; }


        public DbSet<Author> Authors { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductColor> ProductColors { get; set; } 
        public DbSet<Category> Categories { get; set; } 

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Size> Sizes { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; } 
        public DbSet<Tag> Tags { get; set; } 
        public DbSet<ProductTag> ProductTags { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<Banner> Banners { get; set; }

        public DbSet<Team> Teams { get; set; }








    }
}
