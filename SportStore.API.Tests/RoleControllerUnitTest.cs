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
    public class RoleControllerUnitTest
    {
        [TestMethod]
        public void TestRoleAll()
        {
            // Arrange
            var expectedRoles = FakeRoles();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Role.All())
                .Returns(expectedRoles);

            var controller = new RoleController(mockBusinessLogic.Object);

            // Act
            var obtainedRoles = controller.Get();

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(obtainedRoles);
            Assert.AreEqual(expectedRoles.Count(), obtainedRoles.Count());
        }

        [TestMethod]
        public void TestRoleGetById()
        {
            // Arrange
            var expectedRole = FakeRoles().First();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Role.GetById(expectedRole.Name))
                .Returns(expectedRole);

            var controller = new RoleController(mockBusinessLogic.Object);

            // Act
            var result = controller.Get(expectedRole.Name);
            var obtainedRole = (result as OkNegotiatedContentResult<Role>).Content;

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(obtainedRole);
            Assert.AreEqual(expectedRole.Name, obtainedRole.Name);
        }

        [TestMethod]
        public void TestRoleGetByIdNotFound()
        {
            // Arrange
            var name = "Unexistent";

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Role.GetById(name))
                .Returns(null as Role);

            var controller = new RoleController(mockBusinessLogic.Object);

            // Act
            var result = controller.Get(name);

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void TestRoleCreate()
        {
            // Arrange
            var fakeRole = FakeRoles().First();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Role.Create(fakeRole))
                .Returns(fakeRole.Name);

            var controller = new RoleController(mockBusinessLogic.Object);

            // Act
            var result = controller.Post(fakeRole);
            var createdRoleCode = (result as OkNegotiatedContentResult<string>).Content;

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(createdRoleCode);
            Assert.AreEqual(fakeRole.Name, createdRoleCode);
        }

        [TestMethod]
        public void TestRoleUpdate()
        {
            // Arrange
            var fakeRole = FakeRoles().First();

            fakeRole.Name = "Role#1.1";

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Role.Update(fakeRole));

            var controller = new RoleController(mockBusinessLogic.Object);

            // Act
            var result = controller.Put(fakeRole.Name, fakeRole);
            var updatedRoleId = (result as OkNegotiatedContentResult<string>).Content;

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(updatedRoleId);
            Assert.AreEqual(fakeRole.Name, updatedRoleId);
        }

        [TestMethod]
        public void TestRoleDelete()
        {
            // Arrange
            var fakeRole = FakeRoles().First();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Role.DeleteById(fakeRole.Name));

            var controller = new RoleController(mockBusinessLogic.Object);

            // Act
            var result = controller.Delete(fakeRole.Name);

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private IEnumerable<Role> FakeRoles()
        {
            return new List<Role>()
            {
                new Role()
                {
                    Name = "Role#1",
                    Description = ""
                },
                new Role()
                {
                    Name = "Role#2",
                    Description = "",
                }
            };
        }
    }
}
