using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using SportStore.Model;

namespace SportStore.Repository.Entity
{
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        public PaymentMethod GetById(Guid id)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.PaymentMethodDbSet.Find(id);
            } 
        }

        public IEnumerable<PaymentMethod> All()
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.PaymentMethodDbSet.AsEnumerable().ToList();
            }
        }

        public IEnumerable<PaymentMethod> Find(Predicate<PaymentMethod> predicate)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.PaymentMethodDbSet.AsEnumerable().Where(p => predicate(p)).ToList();
            }
        }

        public void Add(PaymentMethod payment)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                dbContext.PaymentMethodDbSet.Add(payment);
                dbContext.SaveChanges();
            }
        }

        public void Update(PaymentMethod payment)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                var paymentMethodInRepository = dbContext.PaymentMethodDbSet.Find(payment.Id);
                dbContext.Entry(paymentMethodInRepository).CurrentValues.SetValues(payment);
                dbContext.SaveChanges();
            }
        }

        public void Remove(PaymentMethod payment)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                var paymentMethodInRepository = dbContext.PaymentMethodDbSet.Find(payment.Id);
                dbContext.PaymentMethodDbSet.Remove(paymentMethodInRepository);
                dbContext.SaveChanges();
            }
        }
    }
}
