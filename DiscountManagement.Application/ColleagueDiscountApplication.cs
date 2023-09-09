using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using DiscountManagement.Domain.CustomerDiscountAgg;

namespace DiscountManagement.Application
{
    public  class ColleagueDiscountApplication: IColleagueDiscountApplication
    {
        private readonly IColleagueDiscountRepository _colleagueDiscountRepository;

        public ColleagueDiscountApplication(IColleagueDiscountRepository colleagueDiscountRepository)
        {
            _colleagueDiscountRepository = colleagueDiscountRepository;
        }

        public OperationResult Define(DefineColleagueDiscount command)
        {
            var operation = new OperationResult();
            if (_colleagueDiscountRepository.Exists(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
            {
                operation.Failed(ApplicationMessages.DuplicatedRecord);
                return operation;
            }
            else
            {

               
                var colleagueDiscount = new ColleagueDiscount(command.ProductId, command.DiscountRate);

                _colleagueDiscountRepository.Create(colleagueDiscount);
                _colleagueDiscountRepository.SaveChanges();

                operation.Succedded("عملیات با موفقیت انجام گردید");
                return operation;
            }
        }

        public OperationResult Edit(EditColleagueDiscount command)
        {
            var operation = new OperationResult();

            var colleagueDiscount = _colleagueDiscountRepository.Get(command.Id);

            if (colleagueDiscount == null)
            {
                operation.Failed(ApplicationMessages.RecordNotFound);
                return operation;
            }


            if (_colleagueDiscountRepository.Exists(x => x.ProductId == command.ProductId &&
                                                        x.DiscountRate == command.DiscountRate && x.Id != command.Id))
            {
                operation.Failed(ApplicationMessages.DuplicatedRecord);
                return operation;
            }


            colleagueDiscount.Edit(command.ProductId, command.DiscountRate);

            _colleagueDiscountRepository.SaveChanges();

            operation.Succedded(ApplicationMessages.SuccessMessage);
            return operation;
        }

        public EditColleagueDiscount GetDetails(long id)
        {
            return _colleagueDiscountRepository.GetDetails(id);
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            return _colleagueDiscountRepository.Search(searchModel);
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();

            var colleagueDiscount = _colleagueDiscountRepository.Get(id);

            if (colleagueDiscount == null)
            {
                operation.Failed(ApplicationMessages.RecordNotFound);
                return operation;
            }
            
            colleagueDiscount.Remove();

            _colleagueDiscountRepository.SaveChanges();

            operation.Succedded(ApplicationMessages.SuccessMessage);
            return operation;
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();

            var colleagueDiscount = _colleagueDiscountRepository.Get(id);

            if (colleagueDiscount == null)
            {
                operation.Failed(ApplicationMessages.RecordNotFound);
                return operation;
            }

            colleagueDiscount.Restore();

            _colleagueDiscountRepository.SaveChanges();

            operation.Succedded(ApplicationMessages.SuccessMessage);
            return operation;
        }
    }
}
