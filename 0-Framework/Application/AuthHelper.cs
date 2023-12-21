using System.Security.Claims;
using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace _0_Framework.Application
  {
    
    
    public class AuthHelper : IAuthHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }


        public void Signout()
        {
            _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public bool IsAuthenticated()
        {
            return _contextAccessor.HttpContext.User.Identity.IsAuthenticated;
            //var claims = _contextAccessor.HttpContext.User.Claims.ToList();
           
            //return claims.Count > 0;
        }

        public void Signin(AuthViewModel account)
        {

            var permissions = JsonConvert.SerializeObject(account.Permissions);
            var claims = new List<Claim>
            {
                new Claim("AccountId", account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.Fullname),
                new Claim(ClaimTypes.Role, account.RoleId.ToString()),
                new Claim("Username", account.Username), // Or Use ClaimTypes.NameIdentifier
                new Claim("Permission", permissions),

            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
            };

            _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        public string CurrentAccountRole()
        {
            if(IsAuthenticated())
            
                return _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;

            return null;
            
            
        }

        public AuthViewModel CurrentAccountInfo()
        {
            var result = new AuthViewModel();
            if(!IsAuthenticated())
                return  result;
            var claims = _contextAccessor.HttpContext.User.Claims.ToList();
            result.Id = long.Parse( claims.FirstOrDefault(x => x.Type == "AccountId").Value);
            result.Username = claims.FirstOrDefault(x => x.Type == "Username").Value;
            result.Fullname =claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            result.RoleId =long.Parse( claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value);
            result.Role = RolesConst.GetByRole(result.RoleId);
            return result;

        }

        public long CurrentAccountId()
        {
            if (IsAuthenticated())

                return long.Parse(_contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type =="AccountId")?.Value);

            return 0;

          
        }

        public List<int> GetPrimissions()
        {
            if (!IsAuthenticated())
                return new List<int>();

            var permissions = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Permission")
                ?.Value;

            return JsonConvert.DeserializeObject<List<int>>(permissions);
        }
    }
}