using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Infrastructure
{
    public static class RolesConst
    {
        public const string Administrator = "1";
        public const string ContetntUploader = "3";
        public const string SystemUser = "2";
        public const string Colleague = "4";

        public static string GetByRole(long id)
        {
            switch (id)
            {
                case 1:
                    return "مدیر سیستم";

                case 2:
                    return "کاربر عادی";

                case 3:
                    return "کاربر محتوا گذار";

                default:
                    return "";
            }
        }
    }
}
