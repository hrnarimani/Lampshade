using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrasructure.EFCore.Mapping;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructur.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrasructure.EFCore
{
    public class IventoryContext : DbContext
    {
        public DbSet<Inventory> Inventory { get; set; }
        public IventoryContext(DbContextOptions<IventoryContext> options) : base(options)
        {
        }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(InventoryMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}
