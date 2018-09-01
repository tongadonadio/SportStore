using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportStore.Model;
using System.Linq;

namespace SportStore.BusinessLogic.V1.Tests
{
    [TestClass]
    public class PaymentMethodIntegrationTest : SportStoreIntegrationTest
    {
        protected override bool LoggedUserShouldBeAdministrator => true;

        private PaymentMethod[] fakePaymentMethods;

        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();

            SetUpPaymentMethodData();
        }

        private void SetUpPaymentMethodData()
        {
            var paymentMethod = new PaymentMethod[]
            {
                new PaymentMethod()
                {
                    Name = "PaymentMethod#1"
                }
            };

            this.fakePaymentMethods = paymentMethod;
        }

        [TestMethod]
        public void TestPaymentMethodCreate()
        {
            // Act
            var id = this.businessLogic.PaymentMethod.Create(fakePaymentMethods[0]);

            // Assert
            var paymentMethod = this.businessLogic.PaymentMethod.GetById(id);
            Assert.AreEqual(id, paymentMethod.Id);
            Assert.AreEqual("PaymentMethod#1", paymentMethod.Name);
        }

        [TestMethod]
        public void TestPaymentMethodUpdate()
        {
            // Arrange
            var id = this.businessLogic.PaymentMethod.Create(fakePaymentMethods[0]);

            // Act
            this.businessLogic.PaymentMethod.Update(new PaymentMethod()
            {
                Id = id,
                Name = "PaymentMethod#1.1"
            });

            // Assert
            var paymentMethod = this.businessLogic.PaymentMethod.GetById(id);
            Assert.AreEqual(id, paymentMethod.Id);
            Assert.AreEqual("PaymentMethod#1.1", paymentMethod.Name);
        }

        [TestMethod]
        public void TestPaymentMethodDelete()
        {
            // Arrange
            var id = this.businessLogic.PaymentMethod.Create(fakePaymentMethods[0]);

            // Act
            this.businessLogic.PaymentMethod.DeleteById(id);

            // Assert
            Assert.IsNull(this.businessLogic.PaymentMethod.GetById(id));
        }

        [TestMethod]
        public void TestPaymentMethodList()
        {
            // Arrange
            this.businessLogic.PaymentMethod.Create(fakePaymentMethods[0]);

            // Assert
            Assert.AreEqual(1, this.businessLogic.PaymentMethod.All().Count());
        }
    }
}
