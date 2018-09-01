using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using SportStore.Model;
using SportStore.Model.Exceptions;

namespace SportStore.Repository.Entity
{
    public class ProductRepository : IProductRepository
    {
        public Product GetById(Guid code)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.ProductDbSet.Include(i => i.CustomFields.Select(ii => ii.CustomField)).SingleOrDefault(p => p.Code == code);
            }
        }

        public IEnumerable<Product> All()
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.ProductDbSet.Include(i => i.CustomFields.Select(ii => ii.CustomField)).ToList();
            }
        }

        public IEnumerable<Product> Find(Predicate<Product> predicate)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.ProductDbSet.Include(i => i.CustomFields.Select(ii => ii.CustomField)).AsEnumerable().Where(p => predicate.Invoke(p)).ToList();
            }
        }

        public void Add(Product product)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                product.Category = dbContext.CategoryDbSet.Include(i => i.CustomFields).SingleOrDefault(c => c.Id == product.Category.Id);
                product.Manufacturer = dbContext.ManufacturerDbSet.Find(product.Manufacturer.Id);

                // productInRepository.CustomFields
                if (product.CustomFields != null)
                {
                    foreach (var customFieldValue in product.CustomFields)
                    {
                        customFieldValue.CustomField = TryFindCustomFieldInCategory(product.Category, customFieldValue.CustomField.Name);
                        customFieldValue.CustomFieldName = customFieldValue.CustomField.Name;
                    }
                }

                dbContext.ProductDbSet.Add(product);

                dbContext.SaveChanges();
            }
        }

        public void Update(Product product)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                var productInRepository = dbContext.ProductDbSet.Include(i => i.CustomFields.Select(ii => ii.CustomField)).SingleOrDefault(p => p.Code == product.Code);

                productInRepository.Name = product.Name;
                productInRepository.Description = product.Description;
                productInRepository.Manufacturer = dbContext.ManufacturerDbSet.Find(product.Manufacturer.Id);
                productInRepository.Price = product.Price;
                productInRepository.Category = dbContext.CategoryDbSet.Include(i => i.CustomFields).SingleOrDefault(c => c.Id == product.Category.Id);
                productInRepository.Stock = product.Stock;

                //productInRepository.Photos = dbContext.CategoryDbSet.Find(product.Category.Name);

                // productInRepository.CustomFields
                if (product.CustomFields != null)
                {
                    foreach (var productCustomFieldValue in product.CustomFields)
                    {
                        var customFieldValueInRepository = productInRepository.CustomFields.SingleOrDefault(cf => cf.CustomField.Name == productCustomFieldValue.CustomField.Name);

                        if (customFieldValueInRepository != null)
                        {
                            customFieldValueInRepository.Value = productCustomFieldValue.Value;
                        }
                        else
                        {
                            productCustomFieldValue.CustomField = TryFindCustomFieldInCategory(productInRepository.Category, productCustomFieldValue.CustomField.Name);
                            productCustomFieldValue.CustomFieldName = productCustomFieldValue.CustomField.Name;
                            productInRepository.CustomFields.Add(productCustomFieldValue);
                        }
                    }

                    var customFieldValuesInRepositoryToDelete = productInRepository.CustomFields.FindAll(cf => !product.CustomFields.Any(ccf => ccf.CustomField.Name == cf.CustomField.Name));

                    foreach (var customFieldValueInRepositoryToDelete in customFieldValuesInRepositoryToDelete)
                    {
                        productInRepository.CustomFields.Remove(customFieldValueInRepositoryToDelete);
                    }
                }
                else
                {
                    productInRepository.CustomFields.Clear();
                }

                dbContext.SaveChanges();
            }
        }

        private CustomField TryFindCustomFieldInCategory(Category category, string customFieldName)
        {
            try
            {
                return category.CustomFields.Single(cf => cf.Name == customFieldName);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidCustomFieldException("The CustomField is not in Product's Category: " + customFieldName, ex);
            }
        }

        public void Remove(Product product)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                var productInRepository = dbContext.ProductDbSet.Include(i => i.CustomFields).SingleOrDefault(p => p.Code == product.Code);
                productInRepository.CustomFields.Clear();

                dbContext.ProductDbSet.Remove(productInRepository);
                dbContext.SaveChanges();
            }
        }

        public void RemoveById(Guid code)
        {
            this.Remove(this.GetById(code));
        }
    }
}
