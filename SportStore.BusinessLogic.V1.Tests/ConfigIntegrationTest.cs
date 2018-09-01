using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

using SportStore.Model;

namespace SportStore.BusinessLogic.V1.Tests
{
    [TestClass]
    public class ConfigIntegrationTest : SportStoreIntegrationTest
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
        public void TestConfigSetDotPrice()
        {
            // Act
            this.businessLogic.Config.SetDotPrice(100);

            // Assert
            Assert.AreEqual(100, this.businessLogic.Config.GetDotPrice());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestConfigSetDotPriceNotAuthorized()
        {
            // Arrange
            this.businessLogic.User.Create(this.fakeUser);
            this.businessLogic.Auth.Logout();
            this.businessLogic.Auth.Login(this.fakeUser.UserName, this.fakeUser.Password);

            // Act
            this.businessLogic.Config.SetDotPrice(100);
        }
    }
}