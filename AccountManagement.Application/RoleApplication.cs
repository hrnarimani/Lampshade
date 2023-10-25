using _0_Framework.Application;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Application
{
    public class RoleApplication : IRoleAplication
    {
        private readonly IRoleRepository _roleRepository;

        public RoleApplication(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public OperationResult Create(CreateRole command)
        {
            var operation = new OperationResult ();
            if(_roleRepository.Exists (x=>x.Name == command.Name))
            {
                operation.Failed(ApplicationMessages.DuplicatedRecord);
                return operation;
            }
            else
            {
                var role= new Role (command.Name);
                _roleRepository.Create (role);
                _roleRepository.SaveChanges ();
                operation.Succedded("عملیات با موفقیت انجام گردید");
                return operation;
            }
        }

        public OperationResult Edit(EditRole command)
        {
            var operation = new OperationResult();
            var role = _roleRepository.Get(command.Id);
            if (role == null) 
            {
                operation.Failed(ApplicationMessages.RecordNotFound);
                return operation;
            }
            if (_roleRepository.Exists(x => x.Name == command.Name &&x.Id != command.Id))
            {
                operation.Failed(ValidationMessages.DuplicatedRecord);
                return operation;
            }

                 role.Edit(command.Name);
                _roleRepository.SaveChanges();
                operation.Succedded(ApplicationMessages.SuccessMessage);
                return operation;
            
        }

        public EditRole GetDetails(long id)
        {
            return _roleRepository.GetDetails(id);
        }

     

        public List<RoleViewModel> List()
        {
            return _roleRepository.list();
        }
    }
}
