using System;
using System.Collections.Generic;
using System.Linq;

using SportStore.BusinessLogic.V1.Auth;
using SportStore.Model;
using SportStore.Repository;

namespace SportStore.BusinessLogic.V1
{
    public class ReviewBusinessLogic : IReviewBusinessLogic
    {
        private IAuthBusinessLogic authBusinessLogic;
        private ISportStoreRepository repository;

        public ReviewBusinessLogic(IAuthBusinessLogic authBusinessLogic, ISportStoreRepository repository)
        {
            this.authBusinessLogic = authBusinessLogic;
            this.repository = repository;
        }

        [AuthRequired]
        public Review GetById(Guid id)
        {
            return this.repository.ReviewRepository.GetById(id);
        }

        [AuthRequired]
        public IEnumerable<Review> All()
        {
            return this.repository.ReviewRepository.All();
        }

        [AuthRequired]
        public IEnumerable<Review> Find(Predicate<Review> r)
        {
            return this.repository.ReviewRepository.Find(r); ;
        }

        [AuthRequired]
        public Guid Create(Review review)
        {
            review.Id = Guid.NewGuid();
            review.User = this.authBusinessLogic.CurrentSession.User;

            this.repository.ReviewRepository.Add(review);

            return review.Id;
        }

        [AuthRequired]
        public void Update(Review review)
        {
            this.repository.ReviewRepository.Update(review);
        }

        [AuthRequired]
        public void Delete(Review review)
        {
            this.repository.ReviewRepository.Remove(review);
        }

        [AuthRequired]
        public void DeleteById(Guid id)
        {
            this.Delete(this.GetById(id));
        }
    }
}
