using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;

namespace DiscountManagement.Domain.ColleagueDiscountAgg
{
    public class ColleagueDiscount:EntityBase
    {
        public  long ProductId  { get; set; }
        public int DiscountRate { get; set; }   
        public bool IsRemove { get; set; }

        public ColleagueDiscount(long productId, int discountRate)
        {
            ProductId = productId;
            DiscountRate = discountRate;
            IsRemove = false;
        }

        public void Edit(long productId, int discountRate)
        {
            ProductId = productId;
            DiscountRate = discountRate;
            
        }

        public void Remove()
        {
            IsRemove = true;
        }

        public void Restore()
        {
            IsRemove = false;
        }
    }
}
