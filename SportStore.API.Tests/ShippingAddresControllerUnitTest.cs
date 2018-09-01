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
    public class ShippingAddresControllerUnitTest
    {
        [TestMethod]
        public void TestShippingAddressAll()
        {
            // Arrange
            var expectedShippingAddres = FakeShippingAddresses();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.ShippingAddress.All())
                .Returns(expectedShippingAddres);

            var controller = new ShippingAddressController(mockBusinessLogic.Object);

            // Act
            var obtainedShippingAddresses = controller.Get();

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(obtainedShippingAddresses);
            Assert.AreEqual(expectedShippingAddres.Count(), obtainedShippingAddresses.Count());
        }

        [TestMethod]
        public void TestShippingAddressGetById()
        {
            // Arrange
            var expectedShippingAddress = FakeShippingAddresses().First();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.ShippingAddress.GetById(expectedShippingAddress.Id))
                .Returns(expectedShippingAddress);

            var controller = new ShippingAddressController(mockBusinessLogic.Object);

            // Act
            var result = controller.Get(expectedShippingAddress.Id);
            var obtainedShippingAddress = (result as OkNegotiatedContentResult<ShippingAddress>).Content;

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(obtainedShippingAddress);
            Assert.AreEqual(expectedShippingAddress.Id, obtainedShippingAddress.Id);
        }

        [TestMethod]
        public void TestShippingAddressGetByIdNotFound()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.ShippingAddress.GetById(id))
                .Returns(null as ShippingAddress);

            var controller = new ShippingAddressController(mockBusinessLogic.Object);

            // Act
            var result = controller.Get(id);

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void TestShippingAddressCreate()
        {
            // Arrange
            var fakeShippingAddress = FakeShippingAddresses().First();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.ShippingAddress.Create(fakeShippingAddress))
                .Returns(fakeShippingAddress.Id);

            var controller = new ShippingAddressController(mockBusinessLogic.Object);

            // Act
            var result = controller.Post(fakeShippingAddress);
            var createdShippingAddressId = (result as OkNegotiatedContentResult<Guid>).Content;

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(createdShippingAddressId);
            Assert.AreEqual(fakeShippingAddress.Id, createdShippingAddressId);
        }

        [TestMethod]
        public void TestShippingAddressUpdate()
        {
            // Arrange
            var fakeShippingAddress = FakeShippingAddresses().First();

            fakeShippingAddress.Phone = "099 111 222";

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.ShippingAddress.Update(fakeShippingAddress));

            var controller = new ShippingAddressController(mockBusinessLogic.Object);

            // Act
            var result = controller.Put(fakeShippingAddress.Id, fakeShippingAddress);
            var updatedShippingAddressId = (result as OkNegotiatedContentResult<Guid>).Content;

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(updatedShippingAddressId);
            Assert.AreEqual(fakeShippingAddress.Id, updatedShippingAddressId);
        }

        [TestMethod]
        public void TestShippingAddressDelete()
        {
            // Arrange
            var fakeShippingAddress = FakeShippingAddresses().First();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.ShippingAddress.DeleteById(fakeShippingAddress.Id));

            var controller = new ShippingAddressController(mockBusinessLogic.Object);

            // Act
            var result = controller.Delete(fakeShippingAddress.Id);

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private IEnumerable<ShippingAddress> FakeShippingAddresses()
        {
            return new List<ShippingAddress>()
            {
                new ShippingAddress()
                {
                    Address = "Test address",
                    FirstName = "Test first name",
                    Id = Guid.NewGuid(),
                    LastName = "Test last name",
                    Phone = "094 123 456"
                },
                new ShippingAddress()
                {
                    Address = "Test address two",
                    FirstName = "Test first name two",
                    Id = Guid.NewGuid(),
                    LastName = "Test last name two",
                    Phone = "094 456 123"
                }
            };
        }
    }
}
