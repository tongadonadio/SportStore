using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using SportStore.Model;

namespace SportStore.Repository.Entity
{
    public class RoleRepository : IRoleRepository
    {
        public Role GetById(string name)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.RoleDbSet.Find(name);
            }
        }

        public IEnumerable<Role> All()
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.RoleDbSet.AsEnumerable().ToList();
            }
        }

        public IEnumerable<Role> Find(Predicate<Role> predicate)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.RoleDbSet.AsEnumerable().Where(r => predicate(r)).ToList();
            }
        }

        public void Add(Role role)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                dbContext.RoleDbSet.Add(role);
                dbContext.SaveChanges();
            }
        }

        public void Update(Role role)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                var roleInRepository = dbContext.RoleDbSet.Find(role.Name);
                roleInRepository.Description = role.Description;

                dbContext.SaveChanges();
            }
        }

        public void Remove(Role role)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                var roleInRepository = dbContext.RoleDbSet.Find(role.Name);
                dbContext.RoleDbSet.Remove(roleInRepository);

                dbContext.SaveChanges();
            }
        }

        public void RemoveById(string name)
        {
            this.Remove(this.GetById(name));
        }
    }
}
