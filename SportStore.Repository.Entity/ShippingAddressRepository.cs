using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using SportStore.Model;

namespace SportStore.Repository.Entity
{
    public class ShippingAddressRepository : IShippingAddressRepository
    {
        public ShippingAddress GetById(Guid id)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.ShippingAddressDbSet.Find(id);
            }
        }

        public IEnumerable<ShippingAddress> All()
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.ShippingAddressDbSet.AsEnumerable().ToList();
            }
        }

        public IEnumerable<ShippingAddress> Find(Predicate<ShippingAddress> predicate)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.ShippingAddressDbSet.AsEnumerable().Where(sa => predicate.Invoke(sa)).ToList();
            }
        }

        public void Add(ShippingAddress shippingAddress)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                dbContext.ShippingAddressDbSet.Add(shippingAddress);
                dbContext.SaveChanges();
            }
        }

        public void Update(ShippingAddress shippingAddress)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                var shippingAddressInRepository = dbContext.ShippingAddressDbSet.Find(shippingAddress.Id);
                dbContext.Entry(shippingAddressInRepository).CurrentValues.SetValues(shippingAddress);
                dbContext.SaveChanges();
            }
        }

        public void Remove(ShippingAddress shippingAddress)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                var shippingAddressInRepository = dbContext.ShippingAddressDbSet.Find(shippingAddress.Id);
                dbContext.ShippingAddressDbSet.Remove(shippingAddressInRepository);
                dbContext.SaveChanges();
            }
        }

        public void RemoveById(Guid id)
        {
            this.Remove(this.GetById(id));
        }
    }
}
