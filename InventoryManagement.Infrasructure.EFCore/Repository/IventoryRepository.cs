using _0_Framework.Application;
using _0_Framework.Infrastructure;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Domain_.InventoryAgg;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructur.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrasructure.EFCore.Repository
{
    public class IventoryRepository : RepositoryBase<long, Inventory>, IInventoryRepository
    {
        private readonly IventoryContext _iventoryContext;
        private readonly ShopContext _shopContetx;

        public IventoryRepository(IventoryContext iventoryContext, ShopContext shopContetx) : base(iventoryContext)
        {
            _iventoryContext = iventoryContext;
            _shopContetx = shopContetx;
        }

        public EditInventory GetDetais(long id)
        {
            return _iventoryContext.Inventory.Select(x => new EditInventory()
            {
                Id = x.Id,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice,

            }).FirstOrDefault(x => x.Id == id);
        }

        public List<InventoryViewMOdel> Seearch(InventorySearchModel searchModel)
        {
            var products = _shopContetx.Products.Select(x => new { x.Id, x.Name }).ToList();

            var query = _iventoryContext.Inventory.Select(x => new InventoryViewMOdel
            {
                Id = x.Id,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice,
                CreationDate = x.CreationDate.ToFarsi(),
                CurrentCount = x.CalculateCurrentCount(),
                InStock = x.InStock,


            });
            if (searchModel.ProductId > 0)
            {
                query = query.Where(x => x.ProductId == searchModel.ProductId);
            }

            if (searchModel.InStock)
            {
                query = query.Where(x => !x.InStock);
            }

            var inventories = query.OrderByDescending(x => x.Id).ToList();
            inventories.ForEach(inventory =>
                inventory.Product = products.FirstOrDefault(x => x.Id == inventory.ProductId)?.Name);
            return inventories;
        }

        public Inventory GetBy(long productId)
        {
            return _iventoryContext.Inventory.FirstOrDefault(x => x.ProductId == productId);
        }

        public List<InventoryOperationViewModel> GetOperationLog(long iventoryId)
        {
            var inventory = _iventoryContext.Inventory.FirstOrDefault(x => x.Id == iventoryId);
            return inventory.Operations.Select(x => new InventoryOperationViewModel
            {
                Id = x.Id,
                Count = x.Count,
                Operation = x.Operation,
                Operator = "مدیر سیستم",
                OperatorId = x.OperatorId,
                OperationDate = x.OperationDate.ToFarsi(),
                CurrentCount = x.CurrentCount,
                Description = x.Description,
                OrderId = x.OrderId,

            }).ToList();
        }


    }



}
