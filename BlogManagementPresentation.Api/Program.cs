using _0_Framework.Application;
using AccountManagement.Infrastructure.EFCore;
using BlogManagement.Infrastructure.Configuration;
using BlogManagement.Infrastructure.EFCore;
using CommentManagement.Infrastructure.EF.Core;
using InventoryManagement.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using ServiceHost;
using ShopManagement.Infrastructur.EFCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddDbContext<BlogContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LampshadeDb") ?? throw new InvalidOperationException("Connection string 'LampshadeDb' not found.")));

builder.Services.AddDbContext<CommentContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LampshadeDb") ?? throw new InvalidOperationException("Connection string 'LampshadeDb' not found.")));

builder.Services.AddDbContext<ShopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LampshadeDb") ?? throw new InvalidOperationException("Connection string 'LampshadeDb' not found.")));

builder.Services.AddDbContext<AccountContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LampshadeDb") ?? throw new InvalidOperationException("Connection string 'LampshadeDb' not found.")));


builder.Services.AddHttpContextAccessor();


InventoryBootstrapper.Configure(builder.Services, "LampshadeDb");
BlogManagementBootstrapper.Configure(builder.Services, "LampshadeDb");

builder.Services.AddTransient<IFileUploader, FileUploader>();


builder.Services.AddTransient<IAuthHelper, AuthHelper>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
