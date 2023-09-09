using _0_Framework.Application;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagement.Application
{
    public class CustomerDiscountApplication : ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository _customerDiscountRepository;

        public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepository)
        {
            _customerDiscountRepository = customerDiscountRepository;
        }

        public OperationResult Define(DefineCustomerDiscount command)
        {
            var operation = new OperationResult();
            if (_customerDiscountRepository.Exists(x=>x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
            {
                operation.Failed(ApplicationMessages.DuplicatedRecord);
                return operation;
            }
            else
            {

                var startDate = command.StartDate.ToGeorgianDateTime();
                var endDate =command.EndDate.ToGeorgianDateTime();
                var customerDiscount = new CustomerDiscount(command.ProductId , command.DiscountRate ,startDate,endDate,
                    command.Reason);

                _customerDiscountRepository .Create(customerDiscount);
                _customerDiscountRepository.SaveChanges();

                operation.Succedded("عملیات با موفقیت انجام گردید");
                return operation;
            }
        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            var operation = new OperationResult();

            var customerDiscount = _customerDiscountRepository.Get(command.Id);

            if (customerDiscount == null)
            {
                operation.Failed(ApplicationMessages.RecordNotFound);
                return operation;
            }


            if (_customerDiscountRepository.Exists(x => x.ProductId == command.ProductId &&
            x.DiscountRate == command.DiscountRate && x.Id != command.Id)) 
            {
                operation.Failed(ApplicationMessages.DuplicatedRecord);
                return operation;
            }

            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();
            customerDiscount.Edit(command.ProductId, command.DiscountRate,startDate,endDate,command.Reason);

            _customerDiscountRepository.SaveChanges();

            operation.Succedded(ApplicationMessages.SuccessMessage);
            return operation;
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _customerDiscountRepository.GetDetails (id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
           return _customerDiscountRepository.Search(searchModel);
        }
    }
}
