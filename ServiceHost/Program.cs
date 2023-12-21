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
using AccountManagement.Infrastructure.Configuration;
using AccountManagement.Infrastructure.EFCore;
using CommentManagement.Configuration;
using CommentManagement.Infrastructure.EF.Core;
using Microsoft.AspNetCore.Authentication.Cookies;
using _0_Framework.Infrastructure;
using System.Collections.Generic;
using _0_Framework.Application.ZarinPal;
using Microsoft.Extensions.Options;

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

builder.Services.AddDbContext<CommentContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LampshadeDb") ?? throw new InvalidOperationException("Connection string 'LampshadeDb' not found.")));

builder.Services.AddDbContext<AccountContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LampshadeDb") ?? throw new InvalidOperationException("Connection string 'LampshadeDb' not found.")));


builder.Services.AddHttpContextAccessor();

ShopManagementBootstrapper.Configure(builder.Services, "LampshadeDb");
DiscountManagementBootstrapper.Configure(builder.Services, "LampshadeDb");
InventoryBootstrapper.Configure(builder.Services, "LampshadeDb");
BlogManagementBootstrapper.Configure(builder.Services, "LampshadeDb");
CommentManagementBootstarpper.Configure(builder.Services, "LampshadeDb");
AccountManagementBootstrapper.Configure(builder.Services, "LampshadeDb");

builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));//SEO


builder.Services.AddTransient<IFileUploader, FileUploader>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();

builder.Services.AddTransient<IAuthHelper, AuthHelper>();

builder.Services.AddTransient<IZarinPalFactory, ZarinPalFactory>();


builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
    {
        o.LoginPath = new PathString("/Account");
        o.LogoutPath = new PathString("/Account");
        o.AccessDeniedPath = new PathString("/AccessDenied");
    });


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminArea",
        builder => builder.RequireRole(new List<string> { RolesConst.Administrator, RolesConst.ContetntUploader }));

    options.AddPolicy("Shop",
    builder => builder.RequireRole(new List<string> { RolesConst.Administrator }));

    options.AddPolicy("Discount",
   builder => builder.RequireRole(new List<string> { RolesConst.Administrator }));

    options.AddPolicy("Account",
   builder => builder.RequireRole(new List<string> { RolesConst.Administrator }));


});


builder.Services.AddRazorPages()
    .AddMvcOptions(options => options.Filters.Add<SecurityPageFilter>())
.AddRazorPagesOptions(options =>
{
    options.Conventions.AuthorizeAreaFolder("Administration", "/", "AdminArea");
    options.Conventions.AuthorizeAreaFolder("Administration", "/Shop", "Shop");
    options.Conventions.AuthorizeAreaFolder("Administration", "/Discounts", "Discount");
    options.Conventions.AuthorizeAreaFolder("Administration", "/Accounts", "Account");

});





var app = builder.Build();

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseDeveloperExceptionPage();
    //app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCookiePolicy();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

