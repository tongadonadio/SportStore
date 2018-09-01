using Microsoft.VisualStudio.TestTools.UnitTesting;

using SportStore.Model;

namespace SportStore.BusinessLogic.V1.Tests
{
    [TestClass]
    public class PurchaseIntegrationTest : SportStoreIntegrationTest
    {
        private Product[] fakeProducts;
        private PaymentMethod fakePaymentMethod;
        private ShippingAddress fakeShippingAddress;

        protected override bool LoggedUserShouldBeAdministrator => true;

        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();

            SetUpProductData();
            SetUpPurchaseData();
        }

        private void SetUpProductData()
        {
            var category = new Category() { Name = "Category#1" };
            var manufacturer = new Manufacturer() { Name = "Manufacturer#1" };
            var products = new Product[]
            {
                new Product()
                {
                    Name = "Product#1",
                    Description = "",
                    Manufacturer = manufacturer,
                    Price = 10.2f,
                    Category = category,
                    Stock = 10
                }
            };

            category.Id = this.businessLogic.Category.Create(category);
            manufacturer.Id = this.businessLogic.Manufacturer.Create(manufacturer);
            products[0].Code = this.businessLogic.Product.Create(products[0]);

            this.fakeProducts = products;
        }

        private void SetUpPurchaseData()
        {
            var paymentMethod = new PaymentMethod() { Name = "PaymentMethod#1" };
            var shippingAddress = new ShippingAddress() { Address = "AddressMethod#1", FirstName = "Sport", LastName = "Store", Phone = "1234" };

            paymentMethod.Id = this.businessLogic.PaymentMethod.Create(paymentMethod);
            shippingAddress.Id = this.businessLogic.ShippingAddress.Create(shippingAddress);

            this.fakePaymentMethod = paymentMethod;
            this.fakeShippingAddress = shippingAddress;
        }

        [TestMethod]
        public void TestPurchaseGetById()
        {
            // Arrange
            var product = this.fakeProducts[0];

            this.businessLogic.Product.Create(product);
            this.businessLogic.Cart.AddProduct(product, 2);

            var id = this.businessLogic.Cart.CheckOut(fakePaymentMethod, fakeShippingAddress).Id;

            // Assert
            var purchase = this.businessLogic.Purchase.GetById(id);
            Assert.AreEqual(1, purchase.Products.Count);
            Assert.AreEqual(product.Code, purchase.Products[0].Product.Code);
            Assert.AreEqual(2, purchase.Products[0].Quantity);
            Assert.AreEqual(20.4f, purchase.Total);
            Assert.AreEqual(fakePaymentMethod.Id, purchase.PaymentMethod.Id);
            Assert.AreEqual(fakeShippingAddress.Id, purchase.ShippingAddress.Id);
        } 
    }
}
