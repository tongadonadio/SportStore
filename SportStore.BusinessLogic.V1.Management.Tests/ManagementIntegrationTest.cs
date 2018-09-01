using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

using SportStore.BusinessLogic.V1.Tests;
using SportStore.Model;

namespace SportStore.BusinessLogic.V1.Management.Tests
{
    [TestClass]
    public class ManagementIntegrationTest : SportStoreIntegrationTest
    {
        protected override bool LoggedUserShouldBeAdministrator => true;

        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();

            SetUpPurchaseData();
        }

        private void SetUpPurchaseData()
        {
            var categories = new Category[] { new Category() { Name = "Category#1" }, new Category() { Name = "Category#2" } };
            var manufacturer = new Manufacturer() { Name = "Manufacturer#1" };
            var products = new Product[]
            {
                new Product()
                {
                    Name = "Product#1",
                    Description = "",
                    Manufacturer = manufacturer,
                    Price = 10.2f,
                    Category = categories[0],
                    Stock = 10
                },
                new Product()
                {
                    Name = "Product#2",
                    Description = "",
                    Manufacturer = manufacturer,
                    Price = 20.2f,
                    Category = categories[0],
                    Stock = 10
                },
                new Product()
                {
                    Name = "Product#3",
                    Description = "",
                    Manufacturer = manufacturer,
                    Price = 1.2f,
                    Category = categories[1],
                    Stock = 10
                }
            };

            categories[0].Id = this.businessLogic.Category.Create(categories[0]);
            categories[1].Id = this.businessLogic.Category.Create(categories[1]);
            manufacturer.Id = this.businessLogic.Manufacturer.Create(manufacturer);
            products[0].Code = this.businessLogic.Product.Create(products[0]);
            products[1].Code = this.businessLogic.Product.Create(products[1]);
            products[2].Code = this.businessLogic.Product.Create(products[2]);

            var paymentMethod = new PaymentMethod() { Name = "PaymentMethod#1" };
            var shippingAddress = new ShippingAddress() { Address = "AddressMethod#1", FirstName = "Sport", LastName = "Store", Phone = "1234" };

            paymentMethod.Id = this.businessLogic.PaymentMethod.Create(paymentMethod);
            shippingAddress.Id = this.businessLogic.ShippingAddress.Create(shippingAddress);

            this.businessLogic.Cart.AddProduct(products[0], 1);
            this.businessLogic.Cart.CheckOut(paymentMethod, shippingAddress);

            this.businessLogic.Cart.AddProduct(products[0], 1);
            this.businessLogic.Cart.AddProduct(products[1], 1);
            this.businessLogic.Cart.AddProduct(products[2], 1);
            this.businessLogic.Cart.CheckOut(paymentMethod, shippingAddress);
        }

        [TestMethod]
        public void TestPurchaseByCategoryReport()
        {
            // Act
            var report = this.businessLogic.Management.PurchaseByCategoryReport();

            // Assert
            var results = report.Results;
            Assert.AreEqual(2, results.Count());
            Assert.AreEqual("Category#1", results.ElementAt(0).Category.Name);
            Assert.AreEqual(40.6f.ToString(), results.ElementAt(0).SubTotal.ToString());
            Assert.AreEqual("Category#2", results.ElementAt(1).Category.Name);
            Assert.AreEqual(1.2f.ToString(), results.ElementAt(1).SubTotal.ToString());
        }

        [TestMethod]
        public void TestPurchasedProductRankingReport()
        {
            // Act
            var report = this.businessLogic.Management.PurchasedProductRankingReport();

            // Assert
            var results = report.Results;
            Assert.AreEqual(3, results.Count());
            Assert.AreEqual("Product#1", results.ElementAt(0).Product.Name);
            Assert.AreEqual(2, results.ElementAt(0).Quantity);
            Assert.AreEqual("Product#2", results.ElementAt(1).Product.Name);
            Assert.AreEqual(1, results.ElementAt(1).Quantity);
            Assert.AreEqual("Product#3", results.ElementAt(2).Product.Name);
            Assert.AreEqual(1, results.ElementAt(2).Quantity);
        }
    }
}
