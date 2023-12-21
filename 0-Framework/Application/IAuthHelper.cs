﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Application
{
    public  interface  IAuthHelper
    {
        void Signin(AuthViewModel account);
        void Signout ();
        bool IsAuthenticated ();
        string CurrentAccountRole();
        AuthViewModel CurrentAccountInfo();
        long CurrentAccountId();
        List<int> GetPrimissions();

    }
}
