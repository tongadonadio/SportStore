using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using SportStore.Model;

namespace SportStore.Repository.Entity
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        public Manufacturer GetById(Guid id)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.ManufacturerDbSet.Find(id);
            }
        }

        public IEnumerable<Manufacturer> All()
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.ManufacturerDbSet.AsEnumerable().ToList();
            }
        }

        public IEnumerable<Manufacturer> Find(Predicate<Manufacturer> predicate)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.ManufacturerDbSet.AsEnumerable().Where(m => predicate(m)).ToList();
            }
        }

        public void Add(Manufacturer manufacturer)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                dbContext.ManufacturerDbSet.Add(manufacturer);
                dbContext.SaveChanges();
            }
        }

        public void Update(Manufacturer manufacturer)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                var manufacturerInRepository = dbContext.ManufacturerDbSet.Find(manufacturer.Id);
                dbContext.Entry(manufacturerInRepository).CurrentValues.SetValues(manufacturer);
                dbContext.SaveChanges();
            }
        }

        public void Remove(Manufacturer manufacturer)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                var manufacturerInRepository = dbContext.ManufacturerDbSet.Find(manufacturer.Id);
                dbContext.ManufacturerDbSet.Remove(manufacturerInRepository);
                dbContext.SaveChanges();
            }
        }
    }
}
