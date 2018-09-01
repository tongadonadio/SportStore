using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using SportStore.Model;

namespace SportStore.Repository.Entity
{
    public class PurchasedProductRepository : IPurchasedProductRepository
    {
        public PurchasedProduct GetById(Guid id)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.PurchasedProductDbSet.Find(id);
            }
                
        }

        public IEnumerable<PurchasedProduct> All()
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.PurchasedProductDbSet.AsEnumerable();
            }
        }

        public IEnumerable<PurchasedProduct> Find(Predicate<PurchasedProduct> predicate)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.PurchasedProductDbSet.AsEnumerable().Where(p => predicate.Invoke(p));
            }
            
        }

        public void Add(PurchasedProduct purchasedProduct)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                dbContext.PurchasedProductDbSet.Add(purchasedProduct);
                dbContext.SaveChanges();
            }  
        }

        public void Update(PurchasedProduct purchasedProduct)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                var purchasedProductInRepository = GetById(purchasedProduct.Id);
                dbContext.Entry(purchasedProductInRepository).CurrentValues.SetValues(purchasedProduct);
                dbContext.SaveChanges();
            }
            
        }

        public void Remove(PurchasedProduct purchasedProduct)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                dbContext.PurchasedProductDbSet.Remove(purchasedProduct);
                dbContext.SaveChanges();
            }
        }
    }
}
