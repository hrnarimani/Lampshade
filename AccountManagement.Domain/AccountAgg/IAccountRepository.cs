using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using AccountManagement.Application.Contracts.Account;

namespace AccountManagement.Domain.AccountAgg
{
    public  interface IAccountRepository :IRepository<long ,Account >
    {
        EditAccount Getdetails(long id);
        List<AccountViewModel> Serach(AccountSearchModel searchModel);
        Account GetBy(string username);
    }
}
