using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Web.Http.Results;

using SportStore.API.Controllers;
using SportStore.BusinessLogic;
using SportStore.Model;

namespace SportStore.API.Tests
{
    [TestClass]
    public class CartControllerUnitTest
    {
        [TestMethod]
        public void TestCartGetForCurrentSession()
        {
            var expectedCart = FakeCart();

            // Arrange
            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Cart.GetForCurrentSession())
                .Returns(expectedCart);

            var controller = new CartController(mockBusinessLogic.Object);

            // Act
            var result = controller.Get();
            var obtainedCart = (result as OkNegotiatedContentResult<Cart>).Content;

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(obtainedCart);
            Assert.AreEqual(1, obtainedCart.Products.Count);
            Assert.AreEqual(10.9f * 2, obtainedCart.Total);
        }

        [TestMethod]
        public void TestCartAddProduct()
        {
            var expectedCart = FakeCart();
            var product = expectedCart.Products[0];

            // Arrange
            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Cart.AddProduct(product.Product, product.Quantity))
                .Returns(expectedCart);

            var controller = new CartController(mockBusinessLogic.Object);

            // Act
            var result = controller.Put(product);

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void TestCartRemoveProduct()
        {
            var expectedCart = FakeCart();
            var product = expectedCart.Products[0];

            expectedCart.Products.Clear();

            // Arrange
            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Cart.RemoveProduct(product.Product, product.Quantity))
                .Returns(expectedCart);

            var controller = new CartController(mockBusinessLogic.Object);

            // Act
            //var result = controller.Delete(product);

            // Assert
            mockBusinessLogic.VerifyAll();

            //Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void TestCartCheckOut()
        {
            var expectedPurchase = FakePurchase();
            var products = expectedPurchase.Products;
            var product = products[0].Product;
            var paymentMethod = expectedPurchase.PaymentMethod;
            var shippingAddress = expectedPurchase.ShippingAddress;

            // Arrange
            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Cart.CheckOut(paymentMethod, shippingAddress))
                .Returns(expectedPurchase);

            var controller = new CartController(mockBusinessLogic.Object);

            // Act
            controller.Put(new ProductInCart(products[0]));

            var result = controller.CheckOut(new CartController.CheckOutArguments(paymentMethod, shippingAddress));
            var obtainedPurchase = (result as OkNegotiatedContentResult<Purchase>).Content;

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(obtainedPurchase);
            Assert.AreEqual(1, obtainedPurchase.Products.Count);
            Assert.AreEqual(10.9f * 2, obtainedPurchase.Total);
        }

        private Cart FakeCart()
        {
            return new Cart()
            {
                Products = new List<ProductInCart>
                {
                    new ProductInCart()
                    {
                        Product =  new Product()
                        {
                            Code = Guid.NewGuid(),
                            Name = "Product#1",
                            Description = "",
                            Price = 10.9f,
                            Category = new Category()
                            {
                                Name = "Category#1",
                            }
                        },
                        Quantity = 2,
                    }
                }
            };
        }

        private Purchase FakePurchase()
        {
            var cart = FakeCart();

            return new Purchase()
            {
                Id = Guid.NewGuid(),
                User = new User()
                {
                    UserName = "Username",
                    FirstName = "FirstName",
                    LastName = "LastName",
                },
                Products = new List<PurchasedProduct>()
                {
                    new PurchasedProduct(cart.Products[0])
                },
                PaymentMethod = new PaymentMethod()
                {
                    Id = Guid.NewGuid(),
                    Name = "Credit card",
                },
                ShippingAddress = new ShippingAddress()
                {
                    Address = "Address#1",
                    FirstName = "FirstName",
                    LastName = "LastName"
                }
            };
        }
    }
}
