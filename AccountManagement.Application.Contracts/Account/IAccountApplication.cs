﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.Account
{
    public  interface IAccountApplication
    {
        OperationResult Edit(EditAccount command);
        OperationResult Create (CreateAccount command);
        OperationResult ChangePassword(ChangePassword command);
        EditAccount Getdetails(long id);
        List<AccountViewModel> Serach(AccountSearchModel searchModel);
        OperationResult Login(Login command);
        void Logout();

    }
}
