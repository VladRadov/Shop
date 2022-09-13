using Shop.Interfaces;
using Shop.Models.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddTransient<IStorageProducts, StorageProducts>();

var app = builder.Build();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Connection}/{action=ConnectionString}/{id?}");
app.Run();
