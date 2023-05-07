using Microsoft.EntityFrameworkCore;
using Pronia_BackEnd_Project.Data;
using Pronia_BackEnd_Project.Services.Interfaces;
using Pronia_BackEnd_Project.Services;
using Pronia_BackEnd_Project.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));  
});


builder.Services.AddScoped<ISliderService,SliderService>();

builder.Services.AddScoped<IAdvertisingService, AdvertisingService>();

builder.Services.AddScoped<IClientService, ClientService>();

builder.Services.AddScoped<IBrandService, BrandService>();

builder.Services.AddScoped<IBlogService, BlogService>();


builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<ICategoryService, CategoryService>();


builder.Services.AddScoped<IColorService, ColorService>();


builder.Services.AddScoped<ITagService, TagService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();

    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles(); 

app.UseSession(); 

app.UseRouting();

app.UseAuthentication(); 

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
