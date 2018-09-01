using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using SportStore.Model;
namespace SportStore.Repository.Entity
{
    public class ReviewRepository : IReviewRepository
    {
        public Review GetById(Guid id)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.ReviewDbSet.Include(i => i.PurchasedProduct).Include(i => i.User).SingleOrDefault(r => r.Id == id);
            }
        }

        public IEnumerable<Review> All()
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.ReviewDbSet.AsEnumerable().ToList();
            }
        }

        public IEnumerable<Review> Find(Predicate<Review> predicate)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.ReviewDbSet.AsEnumerable().Where(r => predicate.Invoke(r)).ToList();
            }
        }

        public void Add(Review review)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                review.PurchasedProduct = dbContext.PurchasedProductDbSet.Find(review.PurchasedProduct.Id);
                review.User = dbContext.UserDbSet.Find(review.User.UserName);

                dbContext.ReviewDbSet.Add(review);

                dbContext.SaveChanges();
            }
        }

        public void Update(Review review)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                var reviewInRepository = dbContext.ReviewDbSet.Include(i => i.PurchasedProduct).Include(i => i.User).SingleOrDefault(r => r.Id == review.Id);

                reviewInRepository.Comment = review.Comment;

                dbContext.SaveChanges();
            }
        }

        public void Remove(Review review)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                var reviewInRepository = dbContext.ReviewDbSet.Include(i => i.PurchasedProduct).Include(i => i.User).SingleOrDefault(r => r.Id == review.Id);

                dbContext.ReviewDbSet.Remove(reviewInRepository);
                dbContext.SaveChanges();
            }
        }

        public Review GetByPurchasedProductId(Guid id)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                return dbContext.ReviewDbSet.SingleOrDefault(r => r.PurchasedProduct.Id == id);
            }
        }
    }
}
