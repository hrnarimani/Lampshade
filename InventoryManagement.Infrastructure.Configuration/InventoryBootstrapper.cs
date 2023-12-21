using _0_Framework.Infrastructure;
using InventoryManagement.Application;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain_.InventoryAgg;
using InventoryManagement.Infrasructure.EFCore;
using InventoryManagement.Infrasructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.Infrastructure.Configuration.Permission;

namespace InventoryManagement.Infrastructure.Configuration
{
    public  class InventoryBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IInventoryApplication, InventoryApplication>();
            services.AddTransient<IInventoryRepository, IventoryRepository>();

            services.AddTransient<IPermissionExposer, InventoryPermissionExposer>();


            services.AddDbContext<IventoryContext>(x => x.UseSqlServer(connectionString));

        }
    }
}
