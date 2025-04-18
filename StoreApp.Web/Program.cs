using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Abstract;
using StoreApp.Data.Concrete;
using StoreApp.Web.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

builder.Services.AddDbContext<StoreDbContext>(options =>
{
    options.UseSqlite(builder.Configuration["ConnectionStrings:StoreDbConnection"],
    b => b.MigrationsAssembly("StoreApp.Web"));
});

// Injecting the EFStoreRepository into the DI container
builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();

var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "products_in_category", "products/{category?}", new { controller = "Home", action = "Index" }
);
// samsung-s24 --> urun detaylari
app.MapControllerRoute(
    name: "product_details", "{name}", new { controller = "Home", action = "Details" }
);


app.MapDefaultControllerRoute();

app.Run();
