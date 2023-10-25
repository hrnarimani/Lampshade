using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Domain.AccountAgg
{
    public class Account :EntityBase
    {
        public string Fullname { get;private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string Mobile { get; private set; }
        public long RoleId { get; private set; }
        public string ProfilePhoto { get; private set; }
        public Role Role { get; private set; }

        public Account(string fullname, string userName, string password, string mobile, long roleId, string profilePhoto)
        {
            Fullname = fullname;
            UserName = userName;
            Password = password;
            Mobile = mobile;
            RoleId = roleId;
            ProfilePhoto = profilePhoto;
        }

        public void Edit(string fullname, string userName,  string mobile, long roleId,
            string profilePhoto)
        {
            Fullname = fullname;
            UserName = userName;
            Mobile = mobile;
            RoleId = roleId;
            if(!string.IsNullOrWhiteSpace(profilePhoto))
            ProfilePhoto = profilePhoto;
        }

        public void ChangePassword(string password)
        {
            Password = password;
        }

    }
}
