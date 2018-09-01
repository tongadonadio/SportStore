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
    public class ManufacturerControllerUnitTest
    {
        [TestMethod]
        public void TestManufacturerAll()
        {
            // Arrange
            var expectedManufacturers = FakeManufacturers();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Manufacturer.All())
                .Returns(expectedManufacturers);

            var controller = new ManufacturerController(mockBusinessLogic.Object);

            // Act
            var obtainedManufacturers = controller.Get();

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(obtainedManufacturers);
            Assert.AreEqual(expectedManufacturers.Count(), obtainedManufacturers.Count());
        }

        [TestMethod]
        public void TestManufacturerGetById()
        {
            // Arrange
            var expectedManufacturer = FakeManufacturers().First();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Manufacturer.GetById(expectedManufacturer.Id))
                .Returns(expectedManufacturer);

            var controller = new ManufacturerController(mockBusinessLogic.Object);

            // Act
            var result = controller.Get(expectedManufacturer.Id);
            var obtainedManufacturer = (result as OkNegotiatedContentResult<Manufacturer>).Content;

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(obtainedManufacturer);
            Assert.AreEqual(expectedManufacturer.Id, obtainedManufacturer.Id);
        }

        [TestMethod]
        public void TestManufacturerGetByIdNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Manufacturer.GetById(id))
                .Returns(null as Manufacturer);

            var controller = new ManufacturerController(mockBusinessLogic.Object);

            // Act
            var result = controller.Get(id);

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void TestManufacturerCreate()
        {
            // Arrange
            var fakeManufacturer = FakeManufacturers().First();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Manufacturer.Create(fakeManufacturer))
                .Returns(fakeManufacturer.Id);

            var controller = new ManufacturerController(mockBusinessLogic.Object);

            // Act
            var result = controller.Post(fakeManufacturer);
            var createdManufacturerCode = (result as OkNegotiatedContentResult<Guid>).Content;

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(createdManufacturerCode);
            Assert.AreEqual(fakeManufacturer.Id, createdManufacturerCode);
        }

        [TestMethod]
        public void TestManufacturerUpdate()
        {
            // Arrange
            var fakeManufacturer = FakeManufacturers().First();

            fakeManufacturer.Name = "Manufacturer#1.1";

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Manufacturer.Update(fakeManufacturer));

            var controller = new ManufacturerController(mockBusinessLogic.Object);

            // Act
            var result = controller.Put(fakeManufacturer.Id, fakeManufacturer);
            var updatedManufacturerId = (result as OkNegotiatedContentResult<Guid>).Content;

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(updatedManufacturerId);
            Assert.AreEqual(fakeManufacturer.Id, updatedManufacturerId);
        }

        [TestMethod]
        public void TestManufacturerDelete()
        {
            // Arrange
            var fakeManufacturer = FakeManufacturers().First();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Manufacturer.DeleteById(fakeManufacturer.Id));

            var controller = new ManufacturerController(mockBusinessLogic.Object);

            // Act
            var result = controller.Delete(fakeManufacturer.Id);

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private IEnumerable<Manufacturer> FakeManufacturers()
        {
            return new List<Manufacturer>()
            {
                new Manufacturer()
                {
                    Id = Guid.NewGuid(),
                    Name = "Manufacturer#1",
                },
                new Manufacturer()
                {
                    Id = Guid.NewGuid(),
                    Name = "Manufacturer#2",
                }
            };
        }
    }
}
