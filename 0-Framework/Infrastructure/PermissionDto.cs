using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Infrastructure
{
    public  class PermissionDto
    {
       public int Code { get; private set; }
       public string Name { get; private set; }

       public PermissionDto(int code, string name)
       {
           Code = code;
           Name = name;
       }

    }
}
