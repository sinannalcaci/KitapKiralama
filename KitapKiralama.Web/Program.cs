using KitapKiralama.Business.Abstract;
using KitapKiralama.Business.Concrete;
using KitapKiralama.DataAccess.Abstract.DataManagement;
using KitapKiralama.DataAccess.Concrete.EntityFramework.Context;
using KitapKiralama.DataAccess.Concrete.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddRazorPages();

builder.Services.AddScoped<IKitapTuruRepository, EfKitapTuruRepository>();
builder.Services.AddScoped<IKitapRepository, EfKitapRepository>();
builder.Services.AddScoped<IKiralamaRepository, EfKiralamaRepository>();
builder.Services.AddScoped<IKitapTuruServices,KitapTuruServices >();
builder.Services.AddScoped<IKitapServices,KitapServices >();
builder.Services.AddScoped<IKiralamaServices,KiralamaServices >();
builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
