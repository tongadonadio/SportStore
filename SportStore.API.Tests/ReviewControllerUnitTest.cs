using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;

using SportStore.API.Controllers;
using SportStore.BusinessLogic;
using SportStore.Model;

namespace SportStore.API.Tests
{
    [TestClass]
    public class ReviewControllerUnitTest
    {
        [TestMethod]
        public void TestReviewAll()
        {
            // Arrange
            var expectedReviews = FakeReviews();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Review.All())
                .Returns(expectedReviews);

            var controller = new ReviewController(mockBusinessLogic.Object);

            // Act
            var obtainedReviews = controller.Get();

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(obtainedReviews);
            Assert.AreEqual(expectedReviews.Count(), obtainedReviews.Count());
        }

        [TestMethod]
        public void TestReviewGetById()
        {
            // Arrange
            var expectedReview = FakeReviews().First();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Review.GetById(expectedReview.Id))
                .Returns(expectedReview);

            var controller = new ReviewController(mockBusinessLogic.Object);

            // Act
            var result = controller.Get(expectedReview.Id);
            var obtainedReview = (result as OkNegotiatedContentResult<Review>).Content;

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(obtainedReview);
            Assert.AreEqual(expectedReview.Id, obtainedReview.Id);
        }

        [TestMethod]
        public void TestReviewGetByIdNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Review.GetById(id))
                .Returns(null as Review);

            var controller = new ReviewController(mockBusinessLogic.Object);

            // Act
            var result = controller.Get(id);

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void TestReviewCreate()
        {
            // Arrange
            var fakeReview = FakeReviews().First();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Review.Create(fakeReview))
                .Returns(fakeReview.Id);

            var controller = new ReviewController(mockBusinessLogic.Object);

            // Act
            var result = controller.Post(fakeReview);
            var createdReviewCode = (result as OkNegotiatedContentResult<Guid>).Content;

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(createdReviewCode);
            Assert.AreEqual(fakeReview.Id, createdReviewCode);
        }

        [TestMethod]
        public void TestReviewUpdate()
        {
            // Arrange
            var fakeReview = FakeReviews().First();

            fakeReview.Comment = "Review#1.1";

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Review.Update(fakeReview));

            var controller = new ReviewController(mockBusinessLogic.Object);

            // Act
            var result = controller.Put(fakeReview.Id, fakeReview);
            var updatedReviewId = (result as OkNegotiatedContentResult<Guid>).Content;

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(updatedReviewId);
            Assert.AreEqual(fakeReview.Id, updatedReviewId);
        }

        [TestMethod]
        public void TestReviewDelete()
        {
            // Arrange
            var fakeReview = FakeReviews().First();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Review.DeleteById(fakeReview.Id));

            var controller = new ReviewController(mockBusinessLogic.Object);

            // Act
            var result = controller.Delete(fakeReview.Id);

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private IEnumerable<Review> FakeReviews()
        {
            return new List<Review>()
            {
                new Review()
                {
                    Id = Guid.NewGuid(),
                    PurchasedProduct = new PurchasedProduct()
                    {
                        Id = Guid.NewGuid(),
                        Product = new Product()
                        {
                            Code = Guid.NewGuid(),
                        },
                        Price = 10,
                        Quantity = 1,
                    }
                },
                new Review()
                {
                    Id = Guid.NewGuid(),
                    PurchasedProduct = new PurchasedProduct()
                    {
                        Id = Guid.NewGuid(),
                        Product = new Product()
                        {
                            Code = Guid.NewGuid(),
                        },
                        Price = 20,
                        Quantity = 2,
                    }
                }
            };
        }
    }
}
