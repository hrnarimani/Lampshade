using _0_Framework.Application;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Domain_.InventoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application
{
    public class InventoryApplication : IInventoryApplication
    {
        private readonly IInventoryRepository _inventoryRepository;

        
        public InventoryApplication(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public OperationResult Create(CreateInventory command)
        {
            var operation = new OperationResult();
            if (_inventoryRepository.Exists(x => x.ProductId == command.ProductId))
            {
                operation.Failed(ApplicationMessages.DuplicatedRecord);
                return operation;
            }
            else
            {
              

                var inventory = new Inventory (command.ProductId, command.UnitPrice);

                _inventoryRepository.Create(inventory);
                _inventoryRepository.SaveChanges();

                operation.Succedded("عملیات با موفقیت انجام گردید");
                return operation;
            }
        }

        public OperationResult Edit(EditInventory command)
        {

            var operation = new OperationResult();

            var inventory = _inventoryRepository.Get(command.Id);

            if (inventory == null)
            {
                operation.Failed(ApplicationMessages.RecordNotFound);
                return operation;
            }


            if (_inventoryRepository.Exists(x => x.ProductId  == command.ProductId && x.Id != command.Id))
            {
                operation.Failed(ApplicationMessages.DuplicatedRecord);
                return operation;
            }

           
            inventory.Edit(command.ProductId, command.UnitPrice);

            _inventoryRepository.SaveChanges();

            operation.Succedded(ApplicationMessages.SuccessMessage);
            return operation;
        }

        public EditInventory GetDetais(long id)
        {
            return _inventoryRepository.GetDetais(id);
        }

        public OperationResult Increase(IncreaseInventory command)
        {
            var operation = new OperationResult();
            var  inventory = _inventoryRepository.Get(command.InventoryId);
            if(inventory==null)
            {
                operation.Failed(ApplicationMessages.RecordNotFound);
                return operation;
            }
            const long operatorId = 1;
            inventory.Increase(command.Count, operatorId, command.Description);
            _inventoryRepository.SaveChanges();
            operation.Succedded(ApplicationMessages.SuccessMessage);
            return operation;

        }

        public OperationResult Reduce(ReduceInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.InventoryId);
            if (inventory == null)
            {
                operation.Failed(ApplicationMessages.RecordNotFound);
                return operation;
            }
            const long operatorId = 1;
            inventory.Reduce(command.Count, operatorId, command.Description,0);
            _inventoryRepository.SaveChanges();
            operation.Succedded(ApplicationMessages.SuccessMessage);
            return operation;
        }

        public OperationResult Reduce(List<ReduceInventory> command)
        {
            var operation = new OperationResult();
            const long operatorId = 1;

            foreach (var item in command)
            {
                var inventory = _inventoryRepository.GetBy(item.ProductId);
                inventory.Reduce(item.Count, operatorId, item.Description, item.OrderId);

            }
             
                      
            _inventoryRepository.SaveChanges();
            operation.Succedded(ApplicationMessages.SuccessMessage);
            return operation;
        }

        public List<InventoryViewMOdel> Seearch(InventorySearchModel searchModel)
        {
            return _inventoryRepository.Seearch(searchModel);
        }

        public List<InventoryOperationViewModel> GetOperationLog(long iventoryId)
        {
            return _inventoryRepository.GetOperationLog(iventoryId);
        }
    }
}
