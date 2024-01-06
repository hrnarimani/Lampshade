using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Application.Sender.Sms;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Infrastructure.EFCore;
using Azure;

namespace AccountManagement.Application
{
    public  class AccountApplication:IAccountApplication
    {
        public AccountApplication(IFileUploader fileUploader, IAccountRepository accountRepository, IPasswordHasher passwordHasher, IAuthHelper authHelper, IRoleRepository roleRepository, ISmsSender smsSender)
        {
            _fileUploader = fileUploader;
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
            _authHelper = authHelper;
            _roleRepository = roleRepository;
            _smsSender = smsSender;
        }


        private readonly IFileUploader _fileUploader;
        private readonly IAccountRepository _accountRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthHelper _authHelper;
        private readonly IRoleRepository _roleRepository;
        private readonly ISmsSender _smsSender;
       



        public OperationResult Edit(EditAccount command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(command.Id);
            if (account == null)
            {
                operation.Failed(ApplicationMessages.RecordNotFound);
                return operation;
            }

            if (_accountRepository.Exists(x => (x.UserName == command.Username || x.Mobile == command.Mobile) && x.Id != command.Id))
            {
                operation.Failed(ApplicationMessages.DuplicatedRecord);
                return operation;
            }


             var path = $"profilePhotos";
             var picturePath = _fileUploader.Upload(command.ProfilePhoto, path);
             account.Edit(command.Fullname, command.Username, command.Mobile, command.RoleId,
                picturePath);
            _accountRepository.SaveChanges();
            operation.Succedded(ApplicationMessages.SuccessMessage);
            return operation;
        }

        public OperationResult Register(RegisterAccount command)
        {
            var operation = new OperationResult();
            if (_accountRepository.Exists(x => x.UserName == command.Username || x.Mobile == command.Mobile))
            {
                operation.Failed(ApplicationMessages.DuplicatedRecord);
                return operation;
            }
            else
            {
                var password = _passwordHasher.Hash(command.Password);
                var codevalidate = CodeGenerator.RandomNumber();
                var path = $"profilePhotos";
                var picturePath = _fileUploader.Upload(command.ProfilePhoto, path);
                var account = new Account(command.Fullname, command.Username, password, command.Mobile, command.RoleId,
                    picturePath,codevalidate);

             //   _smsSender.SendByKavenagarAsync("کد فعالسازی شما در سایت لوازم خانگی حمید :  " + codevalidate , command.Mobile  );  // in r bayad badan faal konam alan be khatere sharj nabodan gheyre faale
                
                _accountRepository.Create(account);
                _accountRepository.SaveChanges();
                operation.Succedded("ثبت نام با موفقیت انجام گردید");
                return operation;

            }
        }

        public OperationResult ChangePassword(ChangePassword command) // vorodi 2 ta meghdar migire bar asase model 
        //agar yeksan bodan onvaght be methide changepassword faght password ro ersal mikone
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(command.Id);
            if (account == null)
            {
                operation.Failed(ApplicationMessages.RecordNotFound);
                return operation;
            }

            if (command.Password != command.RePassword)
            {
                operation.Failed(ApplicationMessages.PasswordNotMatch);
                return operation;
            }

            var password = _passwordHasher.Hash(command.Password);
            account.ChangePassword(password);
            _accountRepository.SaveChanges();
            operation.Succedded(ApplicationMessages.SuccessMessage);
            return operation;
        }

        public EditAccount Getdetails(long id)
        {
            return _accountRepository.Getdetails(id);
        }

        public List<AccountViewModel> Serach(AccountSearchModel searchModel)
        {
            return _accountRepository.Serach(searchModel);
        }

        public OperationResult Login(Login command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetBy(command.Username);
            if (account == null)
            {
                operation.Failed(ApplicationMessages.WrongUserOrPass);
                return operation;
            }


            var permissions = _roleRepository.Get(account.RoleId).Permissions.Select(x => x.Code).ToList();

            (bool Verified, bool NeedsUpgrade) result = _passwordHasher.Check(account.Password,command.Password);

            if(!result.Verified)
            {
                 operation.Failed(ApplicationMessages.WrongUserOrPass);
                return operation;
            }

            if (account.IsActive != true)
            {
                operation.Failed(ApplicationMessages.NotActive);
                return operation;
            }

            var authViewModel = new AuthViewModel(account.Id, account.RoleId, account.Fullname,  account.UserName,account.Mobile,permissions);
           
               
            
            _authHelper.Signin(authViewModel);

            operation.Succedded(ApplicationMessages.SuccessMessage);
            return operation;
        }

        public void Logout()
        {
            _authHelper.Signout();
        }

        public List<AccountViewModel> GetAccounts()
        {
            return _accountRepository.GetAccounts();
        }

        public AccountViewModel GetAccountBy(long id)
        {
            var account = _accountRepository.Get(id);
            return new AccountViewModel()
            {
                Fullname = account.Fullname,
                Mobile = account.Mobile

            };
        }

        public bool ActiveUser(string activeCode)
        {
          return _accountRepository.ActiveUser(activeCode);
        }
    }
}
