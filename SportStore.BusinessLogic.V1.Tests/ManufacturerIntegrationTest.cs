using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportStore.Model;
using System.Linq;

namespace SportStore.BusinessLogic.V1.Tests
{
    [TestClass]
    public class ManufacturerIntegrationTest : SportStoreIntegrationTest
    {
        protected override bool LoggedUserShouldBeAdministrator => true;

        private Manufacturer[] fakeManufacturers;

        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();

            SetUpManufacturerData();
        }

        private void SetUpManufacturerData()
        {
            var manufacturer = new Manufacturer[]
            {
                new Manufacturer()
                {
                    Name = "Manufacturer#1"
                }
            };

            this.fakeManufacturers = manufacturer;
        }


        [TestMethod]
        public void TestManufacturerCreate()
        {
            // Act
            var id = this.businessLogic.Manufacturer.Create(fakeManufacturers[0]);

            // Assert
            var manufacturer = this.businessLogic.Manufacturer.GetById(id);
            Assert.AreEqual(id, manufacturer.Id);
            Assert.AreEqual("Manufacturer#1", manufacturer.Name);
        }

        [TestMethod]
        public void TestManufacturerUpdate()
        {
            // Arrange
            var id = this.businessLogic.Manufacturer.Create(fakeManufacturers[0]);

            // Act
            this.businessLogic.Manufacturer.Update(new Manufacturer()
            {
                Id = id,
                Name = "Manufacturer#1.1"
            });

            // Assert
            var manufacturer = this.businessLogic.Manufacturer.GetById(id);
            Assert.AreEqual(id, manufacturer.Id);
            Assert.AreEqual("Manufacturer#1.1", manufacturer.Name);
        }

        [TestMethod]
        public void TestManufacturerDelete()
        {
            // Arrange
            var id = this.businessLogic.Manufacturer.Create(fakeManufacturers[0]);

            // Act
            this.businessLogic.Manufacturer.DeleteById(id);

            // Assert
            Assert.IsNull(this.businessLogic.Manufacturer.GetById(id));
        }

        [TestMethod]
        public void TestManufacturerList()
        {
            // Arrange
            this.businessLogic.Manufacturer.Create(fakeManufacturers[0]);

            // Assert
            Assert.AreEqual(1, this.businessLogic.Manufacturer.All().Count());
        }
    }
}
