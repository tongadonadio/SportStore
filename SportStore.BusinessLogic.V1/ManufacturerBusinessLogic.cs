using System;
using System.Collections.Generic;

using SportStore.BusinessLogic.V1.Auth;
using SportStore.Model;
using SportStore.Repository;

namespace SportStore.BusinessLogic.V1
{
    public class ManufacturerBusinessLogic : IManufacturerBusinessLogic
    {
        private ISportStoreRepository repository;

        public ManufacturerBusinessLogic(ISportStoreRepository repository)
        {
            this.repository = repository;
        }

        [AuthRequired(RequiredRole = RoleName.Administrator)]
        public Manufacturer GetById(Guid id)
        {
            return this.repository.ManufacturerRepository.GetById(id);
        }

        [AuthRequired(RequiredRole = RoleName.Administrator)]
        public IEnumerable<Manufacturer> All()
        {
            return this.repository.ManufacturerRepository.All();
        }

        [AuthRequired(RequiredRole = RoleName.Administrator)]
        public IEnumerable<Manufacturer> Find(Predicate<Manufacturer> m)
        {
            return this.repository.ManufacturerRepository.Find(m);
        }

        [AuthRequired(RequiredRole = RoleName.Administrator)]
        public Guid Create(Manufacturer manufacturer)
        {
            manufacturer.Id = Guid.NewGuid();

            this.repository.ManufacturerRepository.Add(manufacturer);

            return manufacturer.Id;
        }

        [AuthRequired(RequiredRole = RoleName.Administrator)]
        public void Update(Manufacturer manufacturer)
        {
            this.repository.ManufacturerRepository.Update(manufacturer);
        }

        [AuthRequired(RequiredRole = RoleName.Administrator)]
        public void Delete(Manufacturer manufacturer)
        {
            this.repository.ManufacturerRepository.Remove(manufacturer);
        }

        [AuthRequired(RequiredRole = RoleName.Administrator)]
        public void DeleteById(Guid id)
        {
            this.Delete(this.GetById(id));
        }
    }
}
