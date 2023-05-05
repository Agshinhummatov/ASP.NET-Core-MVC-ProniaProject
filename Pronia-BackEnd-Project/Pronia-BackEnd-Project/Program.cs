using Microsoft.EntityFrameworkCore;
using Pronia_BackEnd_Project.Data;
using Pronia_BackEnd_Project.Services.Interfaces;
using Pronia_BackEnd_Project.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));  
});


builder.Services.AddScoped<ISliderService,SliderService>();


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
