using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using SportStore.Model;

namespace SportStore.Repository.Entity
{
    public class UserRepository : IUserRepository
    {
        public User GetById(string userName)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.UserDbSet.Find(userName);
            }
        }

        public IEnumerable<User> All()
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.UserDbSet.AsEnumerable().ToList();
            }
        }

        public IEnumerable<User> Find(Predicate<User> predicate)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.UserDbSet.AsEnumerable().Where(u => predicate(u)).ToList();
            }
        }

        public void Add(User user)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                user.Role = dbContext.RoleDbSet.Find(user.Role.Name);
                user.Cart = dbContext.CartDbSet.Find(user.Cart.Id);

                dbContext.UserDbSet.Add(user);
                dbContext.SaveChanges();
            }
        }

        public void Update(User user)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                var userInRepository = dbContext.UserDbSet.Find(user.UserName);
                userInRepository.FirstName = user.FirstName;
                userInRepository.LastName = user.LastName;
                userInRepository.Email = user.Email;
                userInRepository.Address = user.Address;
                userInRepository.Role = dbContext.RoleDbSet.Find(user.Role.Name);
                userInRepository.Dots = user.Dots;

                dbContext.SaveChanges();
            }
        }

        public void Remove(User user)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                var userInRepository = dbContext.UserDbSet.Find(user.UserName);
                dbContext.UserDbSet.Remove(userInRepository);

                dbContext.SaveChanges();
            }
        }

        public void RemoveByCode(string userName)
        {
            this.Remove(this.GetById(userName));
        }
    }
}
