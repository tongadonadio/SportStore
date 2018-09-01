using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;

using SportStore.API.Controllers;
using SportStore.API.Controllers.RequestParams;
using SportStore.BusinessLogic;
using SportStore.Model;

namespace SportStore.API.Tests
{
    [TestClass]
    public class ProductControllerUnitTest
    {
        [TestMethod]
        public void TestProductAll()
        {
            // Arrange
            var expectedProducts = FakeProducts();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Product.All())
                .Returns(expectedProducts);

            var controller = new ProductController(mockBusinessLogic.Object);

            // Act
            var obtainedProducts = controller.Get();

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(obtainedProducts);
            Assert.AreEqual(expectedProducts.Count(), obtainedProducts.Count());
        }

        [TestMethod]
        public void TestProductFind()
        {
            // Arrange
            var expectedProducts = FakeProducts();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Product.Find(It.IsAny<Predicate<Product>>()))
                .Returns(expectedProducts.Where(p => p.Name.Contains("Nike")));

            var controller = new ProductController(mockBusinessLogic.Object);

            // Act
            //var obtainedProducts = controller.Get(new ProductParams()
            //{
            //    Name = "Nike"
            //}); 

            // Assert
            mockBusinessLogic.VerifyAll();

            //Assert.IsNotNull(obtainedProducts);
            //Assert.AreEqual(1, obtainedProducts.Count());
        }

        [TestMethod]
        public void TestProductGetById()
        {
            // Arrange
            var expectedProduct = FakeProducts().First();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Product.GetById(expectedProduct.Code))
                .Returns(expectedProduct);

            var controller = new ProductController(mockBusinessLogic.Object);

            // Act
            var result = controller.Get(expectedProduct.Code);
            var obtainedProduct = (result as OkNegotiatedContentResult<Product>).Content;

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(obtainedProduct);
            Assert.AreEqual(expectedProduct.Code, obtainedProduct.Code);
        }

        [TestMethod]
        public void TestProductGetByIdNotFound()
        {
            // Arrange
            var code = Guid.NewGuid();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Product.GetById(code))
                .Returns(null as Product);

            var controller = new ProductController(mockBusinessLogic.Object);

            // Act
            var result = controller.Get(code);

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void TestProductCreate()
        {
            // Arrange
            var fakeProduct = FakeProducts().First();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Product.Create(fakeProduct))
                .Returns(fakeProduct.Code);

            var controller = new ProductController(mockBusinessLogic.Object);

            // Act
            var result = controller.Post(fakeProduct);
            var createdProductCode = (result as OkNegotiatedContentResult<Guid>).Content;

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(createdProductCode);
            Assert.AreEqual(fakeProduct.Code, createdProductCode);
        }

        [TestMethod]
        public void TestProductUpdate()
        {
            // Arrange
            var fakeProduct = FakeProducts().First();

            fakeProduct.Description = "Remera roja";

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Product.Update(fakeProduct));

            var controller = new ProductController(mockBusinessLogic.Object);

            // Act
            var result = controller.Put(fakeProduct.Code, fakeProduct);
            var updatedProductCode = (result as OkNegotiatedContentResult<Guid>).Content;

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(updatedProductCode);
            Assert.AreEqual(fakeProduct.Code, updatedProductCode);
        }

        [TestMethod]
        public void TestProductDelete()
        {
            // Arrange
            var fakeProduct = FakeProducts().First();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Product.DeleteById(fakeProduct.Code));

            var controller = new ProductController(mockBusinessLogic.Object);

            // Act
            var result = controller.Delete(fakeProduct.Code);

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private IEnumerable<Product> FakeProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Code = Guid.NewGuid(),
                    Name = "Remera Nike",
                    Description = "Remera roja, marca Nike",
                    Price = 10.9f,
                    Category = new Category(),
                },
                new Product()
                {
                    Code = Guid.NewGuid(),
                    Name = "Campera Dry-Fit",
                    Description = "Campera Dry-Fit negra, marca Nike",
                    Price = 19.9f,
                    Category = new Category(),
                }
            };
        }
    }
}
