using Microsoft.EntityFrameworkCore;
using Pronia_BackEnd_Project.Data;
using Pronia_BackEnd_Project.Services.Interfaces;
using Pronia_BackEnd_Project.Services;
using Pronia_BackEnd_Project.Models;
using Microsoft.AspNetCore.Identity;
using Pronia_BackEnd_Project.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));  
});


builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders(); /// bunu login registr ucun yazriq


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 8; // bu paswordun lensi en azi 8 olmalidir yeni girilen sifreye en azi 8 simvol daxil elemlidir
    options.Password.RequireDigit = true; // passworda reqem mutleq sekilde olsun
    options.Password.RequireLowercase = true; // balaca herifler mutlreq sekilde olsun
    options.Password.RequireUppercase = true; // boyuk herif en azi 1 dene olsun
    options.Password.RequireNonAlphanumeric = true;  // simvolar en azi 1 dene oslun yeni herif ve reqemden basqa  altdan xet meselcun noqte ve s

    options.User.RequireUniqueEmail = true; // her istifadeci ucun bir emale olmalidir yeni bir emailden 2 istifadeci istifade edib registir ola bilmez
    options.SignIn.RequireConfirmedEmail = true;  /// bunu yazanda emila mesaj gedirki tesdiqle

    options.Lockout.MaxFailedAccessAttempts = 3; // 3 defe   logini tekrar tekerar kece  biler en azi 3 defe sehv ede biler

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);  // bu ise 2 defe sehv edenden sonra bloklayir 30 deqiqelik

    options.Lockout.AllowedForNewUsers = true; // bu ise odurki yeni registerden kecen adam en azi 1 defe login olmalidir yuxarida yazilanlar ona ait deyil yeni sehv ede biler

});





builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddScoped<ISliderService,SliderService>();

builder.Services.AddScoped<IAdvertisingService, AdvertisingService>();

builder.Services.AddScoped<IClientService, ClientService>();

builder.Services.AddScoped<IBrandService, BrandService>();

builder.Services.AddScoped<IBlogService, BlogService>();


builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<ICategoryService, CategoryService>();


builder.Services.AddScoped<IColorService, ColorService>();


builder.Services.AddScoped<ITagService, TagService>();

builder.Services.AddScoped<IBannerService, BannerService>();

builder.Services.AddScoped<IEmailService, EmailService>(); // email confrim ucundur



builder.Services.AddScoped<EmailSettings>();


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
