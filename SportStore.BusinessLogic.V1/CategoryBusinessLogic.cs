using System;
using System.Collections.Generic;

using SportStore.BusinessLogic.V1.Auth;
using SportStore.Model;
using SportStore.Repository;

namespace SportStore.BusinessLogic.V1
{
    public class CategoryBusinessLogic : ICategoryBusinessLogic
    {
        private ISportStoreRepository repository;

        public CategoryBusinessLogic(ISportStoreRepository repository)
        {
            this.repository = repository;
        }
        
        public IEnumerable<Category> All()
        {
            return this.repository.CategoryRepository.All();
        }

        [AuthRequired(RequiredRole = RoleName.Administrator)]
        public Guid Create(Category category)
        {
            category.Id = Guid.NewGuid();

            this.repository.CategoryRepository.Add(category);

            return category.Id;
        }

        [AuthRequired(RequiredRole = RoleName.Administrator)]
        public void Delete(Category category)
        {
            this.repository.CategoryRepository.Remove(category);
        }

        [AuthRequired(RequiredRole = RoleName.Administrator)]
        public void DeleteById(Guid id)
        {
            this.Delete(GetById(id));
        }

        [AuthRequired(RequiredRole = RoleName.Administrator)]
        public IEnumerable<Category> Find(Predicate<Category> c)
        {
            return this.repository.CategoryRepository.Find(c);
        }

        [AuthRequired(RequiredRole = RoleName.Administrator)]
        public Category GetById(Guid id)
        {
            return this.repository.CategoryRepository.GetById(id);
        }

        [AuthRequired(RequiredRole = RoleName.Administrator)]
        public void Update(Category category)
        {
            this.repository.CategoryRepository.Update(category);
        }
    }
}
