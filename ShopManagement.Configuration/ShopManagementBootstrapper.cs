﻿using _0_Framework.Infrastructure;
using _01_LamphadeQuery.Contracts;
using _01_LamphadeQuery.Contracts.Product;
using _01_LamphadeQuery.Contracts.ProductCategory;
using _01_LamphadeQuery.Contracts.Slide;
using _01_LamphadeQuery.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Configuration.Permissions;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Domain.Services;
using ShopManagement.Domain.SlideAgg;
using ShopManagement.Infrastructur.EFCore;
using ShopManagement.Infrastructur.EFCore.Repository;
using ShopManagement.Infrastructur.InventoryACl;
using ShopManagement.Infrastructure.AccountAcl;

namespace ShopManagement.Configuration
{
    public class ShopManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();


            services.AddTransient<IProductApplication , ProductApplication>();
            services.AddTransient<IProductRepository,ProductRepository>();

            services.AddTransient<IProductPictureApplication, ProductPictureApplication>();
            services.AddTransient<IProductPictureRepository, ProductPictureRepository>();

            services.AddTransient<ISlideApplication, SlideApplication >();
            services.AddTransient<ISlideRepository , SlideRepository >();

            services.AddTransient<ISlideQuery, SlideQuery>();
            services.AddTransient<IProductCategoryQuery,ProductCategoryQuery >();
            services.AddTransient<IProductQuery, ProductQuery>();

            services.AddTransient<IPermissionExposer, ShopPermissionExposer>();

            services.AddTransient<ICartCalculateService, CartCalculateService>();


            services.AddTransient<IOrderApplication, OrderApplication>();
            services.AddTransient<IOrderRepository, OrderRepository>();

            services.AddTransient<IShopInventoryAcl, ShopInventoryAcl>();

            services.AddSingleton<ICartService , CartService>();

            services.AddTransient<IShopAccountAcl, ShopAccountAcl>();

            





            services.AddDbContext<ShopContext>(x => x.UseSqlServer(connectionString));

        }



    }
}