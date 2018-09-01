using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using SportStore.Model;

namespace SportStore.Repository.Entity
{
    public class CartRepository : ICartRepository
    {
        public Cart GetById(Guid id)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.CartDbSet.Include(i => i.Products).SingleOrDefault(c => c.Id == id);
            }
        }

        public IEnumerable<Cart> All()
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.CartDbSet.AsEnumerable().ToList();
            }
        }

        public IEnumerable<Cart> Find(Predicate<Cart> predicate)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.CartDbSet.AsEnumerable().Where(c => predicate.Invoke(c)).ToList();
            }
        }

        public void Add(Cart cart)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                dbContext.CartDbSet.Add(cart);
                dbContext.SaveChanges();
            }
        }

        public void Update(Cart cart)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                var cartInRepository = dbContext.CartDbSet.Include(i => i.Products).SingleOrDefault(c => c.Id == cart.Id);

                foreach (var cartProduct in cart.Products)
                {
                    var productInCartInRepository = cartInRepository.Products.SingleOrDefault(p => p.Product.Code == cartProduct.Product.Code);

                    if (cartProduct.Quantity > 0)
                    {
                        if (productInCartInRepository != null)
                        {
                            productInCartInRepository.Quantity = cartProduct.Quantity;
                        }
                        else
                        {
                            var productInRepository = dbContext.ProductDbSet.Find(cartProduct.Product.Code);

                            cartInRepository.Products.Add(new ProductInCart()
                            {
                                Product = productInRepository,
                                ProductCode = productInRepository.Code,
                                Quantity = cartProduct.Quantity,
                            });
                        }
                    }
                    else
                    {
                        cartInRepository.Products.Remove(productInCartInRepository);
                    }
                }

                var productsInCartInRepositoryDeleted = cartInRepository.Products.FindAll(p => !cart.Products.Any(cp => cp.Product.Code == p.Product.Code));

                foreach (var productInCartInRepositoryDeleted in productsInCartInRepositoryDeleted)
                {
                    cartInRepository.Products.Remove(productInCartInRepositoryDeleted);
                }

                if (cartInRepository.Products.Count == 0)
                {
                    cartInRepository.Products.Clear();
                }

                dbContext.SaveChanges();
            }
        }

        public void Remove(Cart cart)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                var cartInRepository = dbContext.CartDbSet.Include(i => i.Products).SingleOrDefault(c => c.Id == cart.Id);
                dbContext.CartDbSet.Remove(cartInRepository);

                dbContext.SaveChanges();
            }
        }
    }
}
