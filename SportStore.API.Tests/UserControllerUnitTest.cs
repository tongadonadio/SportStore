using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;

using SportStore.API.Controllers;
using SportStore.BusinessLogic;
using SportStore.Model;

namespace SportStore.API.Tests
{
    [TestClass]
    public class UserControllerUnitTest
    {
        [TestMethod]
        public void TestUserAll()
        {
            // Arrange
            var expectedUsers = FakeUsers();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.User.All())
                .Returns(expectedUsers);

            var controller = new UserController(mockBusinessLogic.Object);

            // Act
            var obtainedUsers = controller.Get();

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(obtainedUsers);
            Assert.AreEqual(expectedUsers.Count(), obtainedUsers.Count());
        }

        [TestMethod]
        public void TestUserGetById()
        {
            // Arrange
            var expectedUser = FakeUsers().First();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.User.GetById(expectedUser.UserName))
                .Returns(expectedUser);

            var controller = new UserController(mockBusinessLogic.Object);

            // Act
            var result = controller.Get(expectedUser.UserName);
            var obtainedUser = (result as OkNegotiatedContentResult<User>).Content;

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(obtainedUser);
            Assert.AreEqual(expectedUser.UserName, obtainedUser.UserName);
        }

        [TestMethod]
        public void TestUserGetByIdNotFound()
        {
            // Arrange
            var userName = "UnexistentUserName";

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.User.GetById(userName))
                .Returns(null as User);

            var controller = new UserController(mockBusinessLogic.Object);

            // Act
            var result = controller.Get(userName);

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void TestUserCreate()
        {
            // Arrange
            var fakeUser = FakeUsers().First();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.User.Create(fakeUser))
                .Returns(fakeUser.UserName);

            var controller = new UserController(mockBusinessLogic.Object);

            // Act
            var result = controller.Post(fakeUser);
            var createdUserName = (result as OkNegotiatedContentResult<string>).Content;

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(createdUserName);
            Assert.AreEqual(fakeUser.UserName, createdUserName);
        }

        [TestMethod]
        public void TestUserUpdate()
        {
            // Arrange
            var fakeUser = FakeUsers().First();

            fakeUser.FirstName = "Updated Test";

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.User.Update(fakeUser));

            var controller = new UserController(mockBusinessLogic.Object);

            // Act
            var result = controller.Put(fakeUser.UserName, fakeUser);
            var updatedUserName = (result as OkNegotiatedContentResult<string>).Content;

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(updatedUserName);
            Assert.AreEqual(fakeUser.UserName, updatedUserName);
        }

        [TestMethod]
        public void TestUserDelete()
        {
            // Arrange
            var fakeUser = FakeUsers().First();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.User.DeleteById(fakeUser.UserName));

            var controller = new UserController(mockBusinessLogic.Object);

            // Act
            var result = controller.Delete(fakeUser.UserName);

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void TestUserSignUp()
        {
            // Arrange
            var fakeUser = FakeUsers().First();
            var fakeSession = new Session(fakeUser);

            var mockAuthBusinessLogic = new Mock<IAuthBusinessLogic>();
            mockAuthBusinessLogic.SetupGet(abl => abl.CurrentSession).Returns(fakeSession);

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic.SetupGet(bl => bl.Auth).Returns(mockAuthBusinessLogic.Object);

            var controller = new UserController(mockBusinessLogic.Object);

            // Act
            var result = controller.SignUp(fakeUser);
            var session = (result as OkNegotiatedContentResult<Session>).Content;

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(session);
            Assert.AreEqual(fakeSession.Token, session.Token);
            Assert.AreEqual(fakeSession.User.UserName, session.User.UserName);
        }

        [TestMethod]
        public void TestUserLogin()
        {
            // Arrange
            var fakeUser = FakeUsers().First();
            var fakeSession = new Session(fakeUser);

            var mockAuthBusinessLogic = new Mock<IAuthBusinessLogic>();
            mockAuthBusinessLogic.SetupGet(abl => abl.CurrentSession).Returns(fakeSession);

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic.SetupGet(bl => bl.Auth).Returns(mockAuthBusinessLogic.Object);
            mockBusinessLogic.SetupGet(bl => bl.User).Returns(new Mock<IUserBusinessLogic>().Object);

            var controller = new UserController(mockBusinessLogic.Object);
            controller.Post(fakeUser);

            // Act
            var result = controller.Login(fakeUser.UserName, fakeUser.Password);
            var session = (result as OkNegotiatedContentResult<Session>).Content;

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(session);
            Assert.AreEqual(fakeSession.Token, session.Token);
            Assert.AreEqual(fakeSession.User.UserName, session.User.UserName);
        }

        [TestMethod]
        public void TestUserLoginFailed()
        {
            // Arrange
            var fakeUser = FakeUsers().First();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic.SetupGet(bl => bl.User).Returns(new Mock<IUserBusinessLogic>().Object);

            var controller = new UserController(mockBusinessLogic.Object);
            controller.Post(fakeUser);

            // Act
            var result = controller.Login("UnexistentUserName", "******");

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void TestUserLogout()
        {
            // Arrange
            var fakeUser = FakeUsers().First();

            var mockAuthBusinessLogic = new Mock<IAuthBusinessLogic>();
            mockAuthBusinessLogic.SetupGet(abl => abl.CurrentSession).Returns(null as Session);

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic.SetupGet(bl => bl.Auth).Returns(mockAuthBusinessLogic.Object);
            mockBusinessLogic.SetupGet(bl => bl.User).Returns(new Mock<IUserBusinessLogic>().Object);

            var controller = new UserController(mockBusinessLogic.Object);
            controller.Post(fakeUser);
            controller.Login(fakeUser.UserName, fakeUser.Password);

            // Act
            var result = controller.Logout();

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNull(mockAuthBusinessLogic.Object.CurrentSession);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private IEnumerable<User> FakeUsers()
        {
            return new List<User>()
            {
                new User()
                {
                    UserName = "TestUserName",
                    Password = "***",
                    FirstName = "Test",
                    LastName = "UserName",
                    Address = "",
                    Email = "test@example.com"
                },
                new User()
                {
                    UserName = "TestAnotherUserName",
                    Password = "****",
                    FirstName = "Test",
                    LastName = "AnotherUserName",
                    Address = "",
                    Email = "test_another@example.com"
                }
            };
        }
    }
}
