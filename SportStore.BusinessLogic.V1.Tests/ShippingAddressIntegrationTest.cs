using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

using SportStore.BusinessLogic.V1;
using SportStore.Model;

namespace SportStore.BusinessLogic.V1.Tests
{
    [TestClass]
    public class ShippingAddressIntegrationTest : SportStoreIntegrationTest
    {
        private ShippingAddress[] fakeShippingAddresss;

        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();

            SetUpShippingAddressData();
        }

        private void SetUpShippingAddressData()
        {
            var shippingAddress = new ShippingAddress[]
            {
                new ShippingAddress()
                {
                    Address = "ShippingAddress#1",
                    FirstName = "Sport",
                    LastName = "Store",
                    Phone = "1234"
                }
            };

            this.fakeShippingAddresss = shippingAddress;
        }


        [TestMethod]
        public void TestShippingAddressCreate()
        {
            // Act
            var id = this.businessLogic.ShippingAddress.Create(fakeShippingAddresss[0]);

            // Assert
            var shippingAddress = this.businessLogic.ShippingAddress.GetById(id);
            Assert.AreEqual(id, shippingAddress.Id);
            Assert.AreEqual("ShippingAddress#1", shippingAddress.Address);
            Assert.AreEqual("Sport", shippingAddress.FirstName);
            Assert.AreEqual("Store", shippingAddress.LastName);
            Assert.AreEqual("1234", shippingAddress.Phone);
        }

        [TestMethod]
        public void TestShippingAddressUpdate()
        {
            // Arrange
            var id = this.businessLogic.ShippingAddress.Create(fakeShippingAddresss[0]);

            // Act
            this.businessLogic.ShippingAddress.Update(new ShippingAddress()
            {
                Id = id,
                Address = "ShippingAddress#1.1",
                FirstName = "Sport.1",
                LastName = "Store.1",
                Phone = "12345"
            });

            // Assert
            var shippingAddress = this.businessLogic.ShippingAddress.GetById(id);
            Assert.AreEqual(id, shippingAddress.Id);
            Assert.AreEqual("ShippingAddress#1.1", shippingAddress.Address);
            Assert.AreEqual("Sport.1", shippingAddress.FirstName);
            Assert.AreEqual("Store.1", shippingAddress.LastName);
            Assert.AreEqual("12345", shippingAddress.Phone);
        }

        [TestMethod]
        public void TestShippingAddressDelete()
        {
            // Arrange
            var id = this.businessLogic.ShippingAddress.Create(fakeShippingAddresss[0]);

            // Act
            this.businessLogic.ShippingAddress.DeleteById(id);

            // Assert
            Assert.IsNull(this.businessLogic.ShippingAddress.GetById(id));
        }

        [TestMethod]
        public void TestShippingAddressList()
        {
            // Arrange
            this.businessLogic.ShippingAddress.Create(fakeShippingAddresss[0]);

            // Assert
            Assert.AreEqual(1, this.businessLogic.ShippingAddress.All().Count());
        }
    }
}
