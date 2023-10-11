using DiscountManagement.Configuration;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrasructure.EFCore;
using InventoryManagement.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Configuration;
using ShopManagement.Infrastructur.EFCore;
using System.Configuration;
using _0_Framework.Application;
using BlogManagement.Infrastructure.Configuration;
using BlogManagement.Infrastructure.EFCore;
using ServiceHost;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ShopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LampshadeDb") ?? throw new InvalidOperationException("Connection string 'LampshadeDb' not found.")));

builder.Services.AddDbContext<DiscountContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LampshadeDb") ?? throw new InvalidOperationException("Connection string 'LampshadeDb' not found.")));


builder.Services.AddDbContext<IventoryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LampshadeDb") ?? throw new InvalidOperationException("Connection string 'LampshadeDb' not found.")));


builder.Services.AddDbContext<BlogContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LampshadeDb") ?? throw new InvalidOperationException("Connection string 'LampshadeDb' not found.")));


ShopManagementBootstrapper.Configure(builder.Services, "LampshadeDb");
DiscountManagementBootstrapper.Configure(builder.Services, "LampshadeDb");
InventoryBootstrapper.Configure(builder.Services, "LampshadeDb");
BlogManagementBootstrapper.Configure(builder.Services, "LampshadeDb");

builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));//SEO


builder.Services.AddTransient<IFileUploader, FileUploader>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
