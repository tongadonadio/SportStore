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
    public class PaymentMethodControllerUnitTest
    {
        [TestMethod]
        public void TestPaymentMethodAll()
        {
            // Arrange
            var expectedPaymentMethods = FakePaymentMethods();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.PaymentMethod.All())
                .Returns(expectedPaymentMethods);

            var controller = new PaymentMethodController(mockBusinessLogic.Object);

            // Act
            var obtainedPaymentMethods = controller.Get();

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(obtainedPaymentMethods);
            Assert.AreEqual(expectedPaymentMethods.Count(), obtainedPaymentMethods.Count());
        }

        [TestMethod]
        public void TestPaymentMethodGetById()
        {
            // Arrange
            var expectedPaymentMethod = FakePaymentMethods().First();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.PaymentMethod.GetById(expectedPaymentMethod.Id))
                .Returns(expectedPaymentMethod);

            var controller = new PaymentMethodController(mockBusinessLogic.Object);

            // Act
            var result = controller.Get(expectedPaymentMethod.Id);
            var obtainedPaymentMethod = (result as OkNegotiatedContentResult<PaymentMethod>).Content;

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(obtainedPaymentMethod);
            Assert.AreEqual(expectedPaymentMethod.Id, obtainedPaymentMethod.Id);
        }

        [TestMethod]
        public void TestPaymentMethodGetByIdNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.PaymentMethod.GetById(id))
                .Returns(null as PaymentMethod);

            var controller = new PaymentMethodController(mockBusinessLogic.Object);

            // Act
            var result = controller.Get(id);

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void TestPaymentMethodCreate()
        {
            // Arrange
            var fakePaymentMethod = FakePaymentMethods().First();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.PaymentMethod.Create(fakePaymentMethod))
                .Returns(fakePaymentMethod.Id);

            var controller = new PaymentMethodController(mockBusinessLogic.Object);

            // Act
            var result = controller.Post(fakePaymentMethod);
            var createdPaymentMethodCode = (result as OkNegotiatedContentResult<Guid>).Content;

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(createdPaymentMethodCode);
            Assert.AreEqual(fakePaymentMethod.Id, createdPaymentMethodCode);
        }

        [TestMethod]
        public void TestPaymentMethodUpdate()
        {
            // Arrange
            var fakePaymentMethod = FakePaymentMethods().First();

            fakePaymentMethod.Name = "PaymentMethod#1.1";

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.PaymentMethod.Update(fakePaymentMethod));

            var controller = new PaymentMethodController(mockBusinessLogic.Object);

            // Act
            var result = controller.Put(fakePaymentMethod.Id, fakePaymentMethod);
            var updatedPaymentMethodId = (result as OkNegotiatedContentResult<Guid>).Content;

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(updatedPaymentMethodId);
            Assert.AreEqual(fakePaymentMethod.Id, updatedPaymentMethodId);
        }

        [TestMethod]
        public void TestPaymentMethodDelete()
        {
            // Arrange
            var fakePaymentMethod = FakePaymentMethods().First();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.PaymentMethod.DeleteById(fakePaymentMethod.Id));

            var controller = new PaymentMethodController(mockBusinessLogic.Object);

            // Act
            var result = controller.Delete(fakePaymentMethod.Id);

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private IEnumerable<PaymentMethod> FakePaymentMethods()
        {
            return new List<PaymentMethod>()
            {
                new PaymentMethod()
                {
                    Id = Guid.NewGuid(),
                    Name = "PaymentMethod#1",
                },
                new PaymentMethod()
                {
                    Id = Guid.NewGuid(),
                    Name = "PaymentMethod#2",
                }
            };
        }
    }
}
