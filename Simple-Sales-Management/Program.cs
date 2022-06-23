using Microsoft.EntityFrameworkCore;
using Simple_Sales_Management.Models;
using Simple_Sales_Management.Services.CommisionServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Dependency Injection for DbContext
builder.Services.AddDbContext<SaleDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));
builder.Services.AddScoped<ICommisionService, CommisionService>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
