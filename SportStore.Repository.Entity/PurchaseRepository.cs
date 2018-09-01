using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using SportStore.Model;

namespace SportStore.Repository.Entity
{
    public class PurchaseRepository : IPurchaseRepository
    {
        public Purchase GetById(Guid id)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.PurchaseDbSet.SingleOrDefault(p => p.Id == id);
            }   
        }

        public IEnumerable<Purchase> All()
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.PurchaseDbSet.ToList();
            }
        }

        public IEnumerable<Purchase> Find(Predicate<Purchase> predicate)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.PurchaseDbSet.AsEnumerable().Where(p => predicate.Invoke(p)).ToList();
            }
        }

        public void Add(Purchase purchase)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                foreach (var purchasedProduct in purchase.Products)
                {
                    purchasedProduct.Product = dbContext.ProductDbSet.Find(purchasedProduct.Product.Code);
                }

                purchase.PaymentMethod = dbContext.PaymentMethodDbSet.Find(purchase.PaymentMethod.Id);
                purchase.ShippingAddress = dbContext.ShippingAddressDbSet.Find(purchase.ShippingAddress.Id);

                dbContext.PurchaseDbSet.Add(purchase);
                dbContext.SaveChanges();
            }
        }

        public void Update(Purchase purchase)
        {
            throw new Exception("Invalid operation"); // TODO: Custom exception;
        }

        public void Remove(Purchase purchase)
        {
            throw new Exception("Invalid operation"); // TODO: Custom exception;
        }
    }
}
