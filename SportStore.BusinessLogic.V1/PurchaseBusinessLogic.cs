using System;
using System.Collections.Generic;

using SportStore.BusinessLogic.V1.Auth;
using SportStore.Model;
using SportStore.Repository;

namespace SportStore.BusinessLogic.V1
{
    public class PurchaseBusinessLogic : IPurchaseBusinessLogic
    {
        private ISportStoreRepository repository;

        public PurchaseBusinessLogic(ISportStoreRepository repository)
        {
            this.repository = repository;
        }
        
        [AuthRequired]
        public Purchase GetById(Guid id)
        {
            return this.repository.PurchaseRepository.GetById(id);
        }
    }
}
