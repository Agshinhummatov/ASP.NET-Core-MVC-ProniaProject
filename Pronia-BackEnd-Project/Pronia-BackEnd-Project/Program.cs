var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();

    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles(); //static fayllar uchun yeni jc css falan

app.UseSession(); // session istifade edeceyik

app.UseRouting();

app.UseAuthentication(); // login edende ede bilsin deye yaziriq

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
