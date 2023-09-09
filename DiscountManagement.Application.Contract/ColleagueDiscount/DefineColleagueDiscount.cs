using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace DiscountManagement.Application.Contract.ColleagueDiscount
{
    public  class DefineColleagueDiscount
    {
        [Range( 1,100000 , ErrorMessage = ValidationMessages.NotValid)]
        public long ProductId { get; set; }

        [Range(1, 99, ErrorMessage = ValidationMessages.NotValid)]
        public int DiscountRate { get; set; }
    }
}
