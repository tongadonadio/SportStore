using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using SportStore.Model;

namespace SportStore.Repository.Entity
{
    public class CategoryRepository : ICategoryRepository
    {
        public Category GetById(Guid id)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.CategoryDbSet.Include(i => i.CustomFields).SingleOrDefault(c => c.Id == id);
            }
        }

        public IEnumerable<Category> All()
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.CategoryDbSet.Include(i => i.CustomFields).ToList();
            }
        }

        public IEnumerable<Category> Find(Predicate<Category> predicate)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.CategoryDbSet.Include(i => i.CustomFields).AsEnumerable().Where(c => predicate.Invoke(c)).ToList();
            }
        }

        public void Add(Category category)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                dbContext.CategoryDbSet.Add(category);
                dbContext.SaveChanges();
            }
        }

        public void Update(Category category)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                var categoryInRepository = dbContext.CategoryDbSet.Include(i => i.CustomFields).SingleOrDefault(c => c.Id == category.Id);

                categoryInRepository.Name = category.Name;
                categoryInRepository.Description = category.Description;

                foreach (var categoryCustomField in category.CustomFields)
                {
                    var customFieldInRepository = categoryInRepository.CustomFields.SingleOrDefault(cf => cf.Name == categoryCustomField.Name);

                    if (customFieldInRepository != null)
                    {
                        customFieldInRepository.Description = categoryCustomField.Description;
                    }
                    else
                    {
                        categoryInRepository.CustomFields.Add(categoryCustomField);
                    }
                }

                var customFieldsInRepositoryToDelete = categoryInRepository.CustomFields.FindAll(cf => !category.CustomFields.Any(ccf => ccf.Name == cf.Name));

                foreach (var customFieldInRepositoryToDelete in customFieldsInRepositoryToDelete)
                {
                    categoryInRepository.CustomFields.Remove(customFieldInRepositoryToDelete);
                }

                dbContext.SaveChanges();
            }
        }

        public void Remove(Category category)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                var categoryInRepository = dbContext.CategoryDbSet.Include(i => i.CustomFields).SingleOrDefault(c => c.Id == category.Id);
                categoryInRepository.CustomFields.Clear();

                dbContext.CategoryDbSet.Remove(categoryInRepository);
                dbContext.SaveChanges();
            }
        }
    }
}
