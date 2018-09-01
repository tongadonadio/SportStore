using System;
using System.Collections.Generic;

using SportStore.BusinessLogic.V1.Auth;
using SportStore.Model;
using SportStore.Repository;

namespace SportStore.BusinessLogic.V1
{
    public class PaymentMethodBusinessLogic : IPaymentMethodBusinessLogic
    {
        private ISportStoreRepository repository;

        public PaymentMethodBusinessLogic(ISportStoreRepository repository)
        {
            this.repository = repository;
        }

        [AuthRequired(RequiredRole = RoleName.Administrator)]
        public PaymentMethod GetById(Guid id)
        {
            return this.repository.PaymentRepository.GetById(id);
        }

        [AuthRequired(RequiredRole = RoleName.Administrator)]
        public IEnumerable<PaymentMethod> All()
        {
            return this.repository.PaymentRepository.All();
        }

        [AuthRequired(RequiredRole = RoleName.Administrator)]
        public IEnumerable<PaymentMethod> Find(Predicate<PaymentMethod> p)
        {
            return this.repository.PaymentRepository.Find(p);
        }

        [AuthRequired(RequiredRole = RoleName.Administrator)]
        public Guid Create(PaymentMethod paymentMethod)
        {
            paymentMethod.Id = Guid.NewGuid();

            this.repository.PaymentRepository.Add(paymentMethod);

            return paymentMethod.Id;
        }

        [AuthRequired(RequiredRole = RoleName.Administrator)]
        public void Update(PaymentMethod paymentMethod)
        {
            this.repository.PaymentRepository.Update(paymentMethod);
        }

        [AuthRequired(RequiredRole = RoleName.Administrator)]
        public void Delete(PaymentMethod paymentMethod)
        {
            this.repository.PaymentRepository.Remove(paymentMethod);
        }

        [AuthRequired(RequiredRole = RoleName.Administrator)]
        public void DeleteById(Guid id)
        {
            this.Delete(this.GetById(id));
        }
    }
}
