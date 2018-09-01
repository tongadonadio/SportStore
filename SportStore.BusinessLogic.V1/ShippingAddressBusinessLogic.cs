using System;
using System.Collections.Generic;
using System.Linq;

using SportStore.BusinessLogic.V1.Auth;
using SportStore.Model;
using SportStore.Repository;

namespace SportStore.BusinessLogic.V1
{
    public class ShippingAddressBusinessLogic : IShippingAddressBusinessLogic
    {
        private ISportStoreRepository repository;

        public ShippingAddressBusinessLogic(ISportStoreRepository repository)
        {
            this.repository = repository;
        }

        [AuthRequired]
        public ShippingAddress GetById(Guid id)
        {
            return this.repository.ShippingAddressRepository.Find(p => p.Id == id).FirstOrDefault();
        }

        [AuthRequired]
        public IEnumerable<ShippingAddress> All()
        {
            return this.repository.ShippingAddressRepository.All();
        }

        [AuthRequired]
        public IEnumerable<ShippingAddress> Find(Predicate<ShippingAddress> p)
        {
            return this.repository.ShippingAddressRepository.Find(p);
        }

        [AuthRequired]
        public Guid Create(ShippingAddress shippingAddress)
        {
            shippingAddress.Id = Guid.NewGuid();
            this.repository.ShippingAddressRepository.Add(shippingAddress);
            return shippingAddress.Id;
        }

        [AuthRequired]
        public void Update(ShippingAddress shippingAddress)
        {
            this.repository.ShippingAddressRepository.Update(shippingAddress);
        }

        [AuthRequired]
        public void Delete(ShippingAddress shippingAddress)
        {
            this.repository.ShippingAddressRepository.Remove(shippingAddress);
        }

        [AuthRequired]
        public void DeleteById(Guid id)
        {
            this.Delete(this.GetById(id));
        }
    }
}
