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
    public class PurchaseControllerUnitTest
    {
        //[TestMethod]
        //public void TestPurchaseAll()
        //{
        //    // Arrange
        //    var expectedPurchases = FakePurchases();

        //    var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
        //    mockBusinessLogic
        //        .Setup(bl => bl.Purchase.All())
        //        .Returns(expectedPurchases);

        //    var controller = new PurchaseController(mockBusinessLogic.Object);

        //    // Act
        //    var obtainedPurchases = controller.Get();

        //    // Assert
        //    mockBusinessLogic.VerifyAll();

        //    Assert.IsNotNull(obtainedPurchases);
        //    Assert.AreEqual(expectedPurchases.Count(), obtainedPurchases.Count());
        //}

        [TestMethod]
        public void TestPurchaseGetById()
        {
            // Arrange
            var expectedPurchase = FakePurchases().First();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Purchase.GetById(expectedPurchase.Id))
                .Returns(expectedPurchase);

            var controller = new PurchaseController(mockBusinessLogic.Object);

            // Act
            var result = controller.Get(expectedPurchase.Id);
            var obtainedPurchase = (result as OkNegotiatedContentResult<Purchase>).Content;

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(obtainedPurchase);
            Assert.AreEqual(expectedPurchase.Id, obtainedPurchase.Id);
        }

        [TestMethod]
        public void TestPurchaseGetByIdNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Purchase.GetById(id))
                .Returns(null as Purchase);

            var controller = new PurchaseController(mockBusinessLogic.Object);

            // Act
            var result = controller.Get(id);

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        private IEnumerable<Purchase> FakePurchases()
        {
            return new List<Purchase>()
            {
                new Purchase()
                {
                    Id = Guid.NewGuid(),
                },
                new Purchase()
                {
                    Id = Guid.NewGuid(),
                }
            };
        }
    }
}
