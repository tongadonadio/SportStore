using System;
using System.Collections.Generic;
using System.Linq;

using SportStore.BusinessLogic.V1.Auth;
using SportStore.Model;
using SportStore.Repository;

namespace SportStore.BusinessLogic.V1
{
    public class UserBusinessLogic : IUserBusinessLogic
    {
        private ISportStoreRepository repository;

        public UserBusinessLogic(ISportStoreRepository repository)
        {
            this.repository = repository;
        }

       
        public User GetById(string userName)
        {
            return this.repository.UserRepository.Find(p => p.UserName == userName).FirstOrDefault();
        }

        [AuthRequired]
        public IEnumerable<User> All()
        {
            return this.repository.UserRepository.All();
        }

        [AuthRequired]
        public IEnumerable<User> Find(Predicate<User> p)
        {
            return this.repository.UserRepository.Find(p);
        }

        
        public string Create(User user)
        {
            this.CreateCart(user);
            this.repository.UserRepository.Add(user);

            return user.UserName;
        }

        private void CreateCart(User user)
        {
            user.Cart = new Cart()
            {
                Id = Guid.NewGuid(),
                Products = new List<ProductInCart>()
            };

            this.repository.CartRepository.Add(user.Cart);
        }

        [AuthRequired(RequiredRole = RoleName.Administrator)]
        public void Update(User user)
        {
            this.repository.UserRepository.Update(user);
        }

        [AuthRequired(RequiredRole = RoleName.Administrator)]
        public void Delete(User user)
        {
            this.repository.UserRepository.Remove(user);
        }

        [AuthRequired(RequiredRole = RoleName.Administrator)]
        public void DeleteById(string userName)
        {
            this.Delete(this.GetById(userName));
        }
    }
}
