using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

using SportStore.Model;

namespace SportStore.BusinessLogic.V1.Tests
{
    [TestClass]
    public class UserIntegrationTest : SportStoreIntegrationTest
    {
        protected override bool LoggedUserShouldBeAdministrator => true;

        private User fakeUser;

        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();

            SetUpUserData();
        }

        private void SetUpUserData()
        {
            this.fakeUser = new User()
            {
                UserName = "username",
                Password = "123456",
                FirstName = "first name",
                LastName = "last name",
                Email = "test@email.com",
                Address = "address",
                Role = new Role { Name = RoleName.Default },
            };
        }

        [TestMethod]
        public void TestUserCreate()
        {
            // Act
            this.businessLogic.User.Create(this.fakeUser);

            // Assert
            Assert.AreEqual(1, this.businessLogic.User.Find(u => u.UserName == this.fakeUser.UserName).Count());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        public void TestUserCreateRepeated()
        {
            // Arrange
            this.businessLogic.User.Create(this.fakeUser);

            // Assert
            this.businessLogic.User.Create(this.fakeUser);
        }

        [TestMethod]
        public void TestUserUpdate()
        {
            // Arrange
            this.businessLogic.User.Create(this.fakeUser);

            // Act
            this.businessLogic.User.Update(new User()
            {
                UserName = "username",
                Password = "***",
                FirstName = "New first name",
                LastName = "New last name",
                Email = "new@email.com",
                Address = "New address",
                Role = new Role { Name = RoleName.Default },
            });

            // Assert
            var modifiedUser = this.businessLogic.User.Find(u => u.UserName == this.fakeUser.UserName).First();
            Assert.IsNotNull(modifiedUser);
            Assert.AreEqual("123456", modifiedUser.Password); // No se debería haber modificado
            Assert.AreEqual("New first name", modifiedUser.FirstName);
            Assert.AreEqual("New last name", modifiedUser.LastName);
            Assert.AreEqual("new@email.com", modifiedUser.Email);
            Assert.AreEqual("New address", modifiedUser.Address);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        public void TestUserUpdateUnexistent()
        {
            // Arrange
            this.businessLogic.User.Create(this.fakeUser);

            // Act
            this.businessLogic.User.Update(new User()
            {
                UserName = "unexistent username",
                Password = "***",
            });
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestUserUpdateNotAuthorized()
        {
            // Arrange
            this.businessLogic.User.Create(this.fakeUser);
            this.businessLogic.Auth.Logout();
            this.businessLogic.Auth.Login(this.fakeUser.UserName, this.fakeUser.Password);

            // Act
            this.businessLogic.User.Update(new User()
            {
                UserName = "username",
                Password = "***",
                FirstName = "New first name",
                LastName = "New last name",
                Email = "new@email.com",
                Address = "New address",
                Role = Role.Administrator,
            });
        }

        [TestMethod]
        public void TestUserDelete()
        {
            // Arrange
            this.businessLogic.User.Create(this.fakeUser);
            
            // Act
            this.businessLogic.User.DeleteById(this.fakeUser.UserName);

            // Assert
            Assert.AreEqual(0, this.businessLogic.User.Find(u => u.UserName == this.fakeUser.UserName).Count());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestUserDeleteNotAuthorized()
        {
            // Arrange
            this.businessLogic.User.Create(this.fakeUser);
            this.businessLogic.Auth.Logout();
            this.businessLogic.Auth.Login(this.fakeUser.UserName, this.fakeUser.Password);

            // Assert
            this.businessLogic.User.DeleteById(this.fakeUser.UserName);
        }

        [TestMethod]
        public void TestUserList()
        {
            // Arrange
            this.businessLogic.User.Create(this.fakeUser);

            // Assert
            Assert.AreEqual(2, this.businessLogic.User.All().Count());
        }

        [TestMethod]
        public void TestUserLogin()
        {
            // Arrange
            this.businessLogic.User.Create(this.fakeUser);
            this.businessLogic.Auth.Logout();
            
            // Act
            this.businessLogic.Auth.Login(this.fakeUser.UserName, this.fakeUser.Password);

            // Assert
            Assert.AreEqual(this.fakeUser.UserName, this.businessLogic.Auth.CurrentSession.User.UserName);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestUserNotAuthorizedAction()
        {
            // Arrange
            this.businessLogic.Auth.Logout();
            
            // Assert
            this.businessLogic.User.DeleteById(this.fakeUser.UserName);
        }

    }
}
