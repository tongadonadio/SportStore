using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

using SportStore.Model;

namespace SportStore.BusinessLogic.V1.Tests
{
    [TestClass]
    public class CartIntegrationTest : SportStoreIntegrationTest
    {
        private Product[] fakeProducts;
        private PaymentMethod fakePaymentMethod;
        private ShippingAddress fakeShippingAddress;

        protected override bool LoggedUserShouldBeAdministrator => true;

        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();

            SetUpProductData();
            SetUpPurchaseData();
        }

        private void SetUpProductData()
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
                    Price = 100.2f,
                    Category = category,
                    Stock = 10
                }
            };

            category.Id = this.businessLogic.Category.Create(category);
            manufacturer.Id = this.businessLogic.Manufacturer.Create(manufacturer);
            products[0].Code = this.businessLogic.Product.Create(products[0]);

            this.fakeProducts = products;
        }

        private void SetUpPurchaseData()
        {
            var paymentMethod = new PaymentMethod() { Name = "PaymentMethod#1" };
            var shippingAddress = new ShippingAddress() { Address = "AddressMethod#1", FirstName = "Sport", LastName = "Store", Phone = "1234" };

            paymentMethod.Id = this.businessLogic.PaymentMethod.Create(paymentMethod);
            shippingAddress.Id = this.businessLogic.ShippingAddress.Create(shippingAddress);

            this.fakePaymentMethod = paymentMethod;
            this.fakeShippingAddress = shippingAddress;
        }

        [TestMethod]
        public void TestCartGetForCurrentSession()
        {
            // Act
            Cart cart = this.businessLogic.Cart.GetForCurrentSession();

            // Assert
            Assert.IsNotNull(cart);
        }

        [TestMethod]
        public void TestCartAddProduct()
        {
            // Arrange
            var product = this.fakeProducts[0];

            this.businessLogic.Product.Create(product);

            // Act
            this.businessLogic.Cart.AddProduct(product, 2);

            // Assert
            var cart = this.businessLogic.Cart.GetForCurrentSession();
            Assert.AreEqual(1, cart.Products.Count);
            Assert.AreEqual(2, cart.Products.Find(p => p.ProductCode == product.Code).Quantity);
            Assert.AreEqual(200.4f, cart.Total);
        }

        [TestMethod]
        public void TestCartRemoveProductPartial()
        {
            // Arrange
            var product = this.fakeProducts[0];

            this.businessLogic.Product.Create(product);
            this.businessLogic.Cart.AddProduct(product, 2);

            // Act
            this.businessLogic.Cart.RemoveProduct(product, 1);

            // Assert
            var cart = this.businessLogic.Cart.GetForCurrentSession();
            Assert.AreEqual(1, cart.Products.Count);
            Assert.AreEqual(1, cart.Products.Find(p => p.ProductCode == product.Code).Quantity);
            Assert.AreEqual(100.2f, cart.Total);
        }

        [TestMethod]
        public void TestCartRemoveProductTotal()
        {
            // Arrange
            var product = this.fakeProducts[0];

            this.businessLogic.Product.Create(product);
            this.businessLogic.Cart.AddProduct(product, 2);

            // Act
            this.businessLogic.Cart.RemoveProduct(product, 2);

            // Assert
            var cart = this.businessLogic.Cart.GetForCurrentSession();
            Assert.AreEqual(0, cart.Products.Count);
            Assert.AreEqual(0f, cart.Total);
        }

        [TestMethod]
        public void TestCartCheckOut()
        {
            // Arrange
            var product = this.fakeProducts[0];

            this.businessLogic.Product.Create(product);
            this.businessLogic.Cart.AddProduct(product, 2);

            // Act
            var result = this.businessLogic.Cart.CheckOut(fakePaymentMethod, fakeShippingAddress);

            // Assert
            var purchase = this.businessLogic.Purchase.GetById(result.Id);
            Assert.AreEqual(1, purchase.Products.Count);
            Assert.AreEqual(product.Code, purchase.Products[0].Product.Code);
            Assert.AreEqual(2, purchase.Products[0].Quantity);
            Assert.AreEqual(200.4f, purchase.Total);
            Assert.AreEqual(fakePaymentMethod.Id, purchase.PaymentMethod.Id);
            Assert.AreEqual(fakeShippingAddress.Id, purchase.ShippingAddress.Id);

            var productInRepository = this.businessLogic.Product.GetById(product.Code);
            Assert.AreEqual(8, productInRepository.Stock);

            var currentSessionUserName = this.businessLogic.Auth.CurrentSession.User.UserName;
            var currentSessionUser = this.businessLogic.User.GetById(currentSessionUserName);
            Assert.AreEqual(1, currentSessionUser.Dots);
        }

        [TestMethod]
        public void TestCartCheckOutWithProductInDotBlackList()
        {
            // Arrange
            var product = this.fakeProducts[0];
            product.Code = this.businessLogic.Product.Create(product);
            
            this.businessLogic.Config.AddProductToDotBlackList(product.Code);

            this.businessLogic.Cart.AddProduct(product, 2);

            // Act
            var result = this.businessLogic.Cart.CheckOut(fakePaymentMethod, fakeShippingAddress);

            // Assert
            var purchase = this.businessLogic.Purchase.GetById(result.Id);
            Assert.AreEqual(1, purchase.Products.Count);
            Assert.AreEqual(product.Code, purchase.Products[0].Product.Code);
            Assert.AreEqual(2, purchase.Products[0].Quantity);
            Assert.AreEqual(200.4f, purchase.Total);
            Assert.AreEqual(fakePaymentMethod.Id, purchase.PaymentMethod.Id);
            Assert.AreEqual(fakeShippingAddress.Id, purchase.ShippingAddress.Id);

            var productInRepository = this.businessLogic.Product.GetById(product.Code);
            Assert.AreEqual(8, productInRepository.Stock);

            var currentSessionUserName = this.businessLogic.Auth.CurrentSession.User.UserName;
            var currentSessionUser = this.businessLogic.User.GetById(currentSessionUserName);
            Assert.AreEqual(0, currentSessionUser.Dots);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestCartCheckOutWithMoreProductsThanInStock()
        {
            // Arrange
            var product = this.fakeProducts[0];

            this.businessLogic.Product.Create(product);
            this.businessLogic.Cart.AddProduct(product, 12);

            // Act
            this.businessLogic.Cart.CheckOut(fakePaymentMethod, fakeShippingAddress);
        }
    }
}