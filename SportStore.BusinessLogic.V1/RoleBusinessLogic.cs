using System;
using System.Collections.Generic;
using System.Linq;

using SportStore.BusinessLogic.V1.Auth;
using SportStore.Model;
using SportStore.Repository;

namespace SportStore.BusinessLogic.V1
{
    public class RoleBusinessLogic : IRoleBusinessLogic
    {
        private ISportStoreRepository repository;

        public RoleBusinessLogic(ISportStoreRepository repository)
        {
            this.repository = repository;
        }

        [AuthRequired]
        public Role GetById(string name)
        {
            return this.repository.RoleRepository.Find(p => p.Name == name).FirstOrDefault();
        }

        [AuthRequired]
        public IEnumerable<Role> All()
        {
            return this.repository.RoleRepository.All();
        }

        [AuthRequired]
        public IEnumerable<Role> Find(Predicate<Role> p)
        {
            return this.repository.RoleRepository.Find(p);
        }

        [AuthRequired]
        public string Create(Role role)
        {
            this.repository.RoleRepository.Add(role);

            return role.Name;
        }

        [AuthRequired]
        public void Update(Role role)
        {
            this.repository.RoleRepository.Update(role);
        }

        [AuthRequired]
        public void Delete(Role role)
        {
            this.repository.RoleRepository.Remove(role);
        }

        [AuthRequired]
        public void DeleteById(string name)
        {
            this.Delete(this.GetById(name));
        }
    }
}
