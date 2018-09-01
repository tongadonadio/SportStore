using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

using SportStore.Model;

namespace SportStore.BusinessLogic.V1.Tests
{
    [TestClass]
    public class ReviewIntegrationTest : SportStoreIntegrationTest
    {
        private Purchase fakePurchase;
        private Review fakeReview;

        protected override bool LoggedUserShouldBeAdministrator => true;

        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();

            SetUpPurchaseData();
            SetUpReviewData();
        }

        private void SetUpPurchaseData()
        {
            var category = new Category() { Name = "Category#1" };
            var manufacturer = new Manufacturer() { Name = "Manufacturer#1" };
            var products = new Product[]
            {
                new Product()
                {
                    Name = "Product#1",
                    Description = "",
                    Manufacturer = manufacturer,
                    Price = 10.2f,
                    Category = category,
                    Stock = 10
                }
            };

            category.Id = this.businessLogic.Category.Create(category);
            manufacturer.Id = this.businessLogic.Manufacturer.Create(manufacturer);
            products[0].Code = this.businessLogic.Product.Create(products[0]);

            var paymentMethod = new PaymentMethod() { Name = "PaymentMethod#1" };
            var shippingAddress = new ShippingAddress() { Address = "AddressMethod#1", FirstName = "Sport", LastName = "Store", Phone = "1234" };

            paymentMethod.Id = this.businessLogic.PaymentMethod.Create(paymentMethod);
            shippingAddress.Id = this.businessLogic.ShippingAddress.Create(shippingAddress);
            
            this.businessLogic.Cart.AddProduct(products[0], 2);

            this.fakePurchase = this.businessLogic.Cart.CheckOut(paymentMethod, shippingAddress);
        }

        private void SetUpReviewData()
        {
            var review = new Review()
            {
                Comment = "Comment#1",
                PurchasedProduct = this.fakePurchase.Products[0]
            };

            this.fakeReview = review;
        }

        [TestMethod]
        public void TestReviewCreate()
        {
            // Act
            var id = this.businessLogic.Review.Create(this.fakeReview);

            // Assert
            var review = this.businessLogic.Review.GetById(id);
            Assert.AreEqual("Comment#1", review.Comment);
            Assert.AreEqual(this.fakeReview.PurchasedProduct.Id, review.PurchasedProduct.Id);
            Assert.AreEqual(this.businessLogic.Auth.CurrentSession.User.UserName, review.User.UserName);
        }

        [TestMethod]
        public void TestReviewUpdate()
        {
            // Arrange
            var id = this.businessLogic.Review.Create(this.fakeReview);

            // Act
            this.fakeReview.Comment = "Comment#1.1";
            this.businessLogic.Review.Update(this.fakeReview);

            // Assert
            var review = this.businessLogic.Review.GetById(id);
            Assert.AreEqual("Comment#1.1", review.Comment);
            Assert.AreEqual(this.fakeReview.PurchasedProduct.Id, review.PurchasedProduct.Id);
            Assert.AreEqual(this.businessLogic.Auth.CurrentSession.User.UserName, review.User.UserName);
        }

        [TestMethod]
        public void TestReviewDelete()
        {
            // Arrange
            var id = this.businessLogic.Review.Create(this.fakeReview);

            // Act
            this.businessLogic.Review.DeleteById(id);

            // Assert
            Assert.IsNull(this.businessLogic.Review.GetById(id));
        }

        [TestMethod]
        public void TestReviewList()
        {
            // Arrange
            var id = this.businessLogic.Review.Create(this.fakeReview);
            
            // Assert
            Assert.AreEqual(1, this.businessLogic.Review.All().Count());
        }
    }
}
