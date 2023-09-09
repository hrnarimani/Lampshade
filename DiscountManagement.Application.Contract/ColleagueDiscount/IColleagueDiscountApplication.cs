using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace DiscountManagement.Application.Contract.ColleagueDiscount
{
    public interface  IColleagueDiscountApplication
    {
        OperationResult Define(DefineColleagueDiscount command);
        OperationResult Edit (EditColleagueDiscount command);   
        EditColleagueDiscount GetDetails (long id);
        List <ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel );
        OperationResult Remove (long id);   
        OperationResult Restore (long id);
    }
}
