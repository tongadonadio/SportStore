using System;
using System.Collections.Generic;
using System.Linq;

using SportStore.BusinessLogic.V1.Auth;
using SportStore.BusinessLogic.V1.Log;
using SportStore.Log.Events;
using SportStore.Model;
using SportStore.Repository;

namespace SportStore.BusinessLogic.V1
{
    public class ProductBusinessLogic : IProductBusinessLogic
    {
        private ISportStoreRepository repository;

        public ProductBusinessLogic(ISportStoreRepository repository)
        {
            this.repository = repository;
        }
        
        public Product GetById(Guid id)
        {
            return this.repository.ProductRepository.GetById(id);
        }
        
        public IEnumerable<Product> All()
        {
            return this.repository.ProductRepository.All();
        }
        
        public IEnumerable<Product> Find(Predicate<Product> p)
        {
            return this.repository.ProductRepository.Find(p);
        }

        [AuthRequired(RequiredRole = RoleName.Administrator)]
        public Guid Create(Product product)
        {
            product.Code = Guid.NewGuid();

            this.repository.ProductRepository.Add(product);

            return product.Code;
        }

        [AuthRequired(RequiredRole = RoleName.Administrator)]
        public void CreateRangeWithoutThrowingException(IEnumerable<Product> products, out int createdProducts)
        {
            createdProducts = 0;

            foreach (var product in products)
            {
                try
                {
                    this.Create(product);

                    createdProducts++;
                }
                catch (Exception ex)
                {
                    // TODO: SportStoreLog.Instance.WriteEvent(new ExceptionEvent());
                }
            }
        }

        [AuthRequired(RequiredRole = RoleName.Administrator)]
        public void Update(Product product)
        {
            this.repository.ProductRepository.Update(product);
        }

        [AuthRequired(RequiredRole = RoleName.Administrator)]
        public void Delete(Product product)
        {
            this.repository.ProductRepository.Remove(product);
        }

        [AuthRequired(RequiredRole = RoleName.Administrator)]
        public void DeleteById(Guid id)
        {
            this.Delete(this.GetById(id));
        }

        [AuthRequired]
        public IEnumerable<Review> AllReviews(Product product)
        {
            return this.repository.ReviewRepository.Find(r => r.PurchasedProduct.Product.Code == product.Code);
        }
    }
}
