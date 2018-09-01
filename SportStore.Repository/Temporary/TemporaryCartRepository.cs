using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportStore.Model;

namespace SportStore.Repository.Temporary
{
    public class TemporaryCartRepository : ICartRepository
    {
        List<Cart> list = new List<Cart>();

        public void Add(Cart element)
        {
            this.list.Add(element);
        }

        public IEnumerable<Cart> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cart> Find(Predicate<Cart> p)
        {
            throw new NotImplementedException();
        }

        public Cart GetById(string id)
        {
            return list.Find(c => c.User.UserName == id); ;
        }

        public void Remove(Cart element)
        {
            throw new NotImplementedException();
        }

        public void Update(Cart cart)
        {
            var cartInRepository = list.Find(c => c.User.UserName == cart.User.UserName);
            cartInRepository.Products = cart.Products;
        }
    }
}
