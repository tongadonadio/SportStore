using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using SportStore.Model;
using SportStore.Model.Exceptions;

namespace SportStore.BusinessLogic.V1.Tests
{
    [TestClass]
    public class ProductIntegrationTest : SportStoreIntegrationTest
    {
        private Category[] fakeCategories;
        private Manufacturer[] fakeManufacturers;
        private Product[] fakeProducts;

        protected override bool LoggedUserShouldBeAdministrator => true;

        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();

            SetUpProductData();
        }

        private void SetUpProductData()
        {
            var categories = new Category[]
            {
                new Category() { Name = "Category#1", CustomFields = new List<CustomField>() { new CustomFieldNumber() { Name = "CustomField#1" }, new CustomFieldNumber() { Name = "CustomField#2" } } },
                new Category() { Name = "Category#2" } 
            };

            var manufacturers = new Manufacturer[]
            {
                new Manufacturer() { Name = "Manufacturer#1" },
                new Manufacturer() { Name = "Manufacturer#2" }
            };

            var products = new Product[]
            {
                new Product()
                {
                    Name = "Product#1",
                    Description = "abc",
                    Manufacturer = manufacturers[0],
                    Price = 10.2f,
                    Category = categories[0],
                    CustomFields = new List<CustomFieldValue>()
                    {
                        new CustomFieldValue()
                        {
                            CustomField = new CustomFieldNumber() { Name = "CustomField#1" },
                            Value = "1",
                        }
                    },
                    Stock = 10
                },
                new Product()
                {
                    Name = "Product#2",
                    Description = "abcd",
                    Manufacturer = manufacturers[0],
                    Price = 1.2f,
                    Category = categories[0],
                    CustomFields = null,
                    Stock = 10
                },
                new Product()
                {
                    Name = "Product#3",
                    Description = "abcde",
                    Manufacturer = manufacturers[0],
                    Price = 20.2f,
                    Category = categories[1],
                    CustomFields = null,
                    Stock = 10
                }
            };

            categories[0].Id = this.businessLogic.Category.Create(categories[0]);
            categories[1].Id = this.businessLogic.Category.Create(categories[1]);
            manufacturers[0].Id = this.businessLogic.Manufacturer.Create(manufacturers[0]);
            manufacturers[1].Id = this.businessLogic.Manufacturer.Create(manufacturers[1]);

            this.fakeCategories = categories;
            this.fakeManufacturers = manufacturers;
            this.fakeProducts = products;
        }

        [TestMethod]
        public void TestProductCreate()
        {
            // Act
            var code = this.businessLogic.Product.Create(fakeProducts[0]);

            // Assert
            var product = this.businessLogic.Product.GetById(code);
            Assert.AreEqual(code, product.Code);
            Assert.AreEqual("Product#1", product.Name);
            Assert.AreEqual("abc", product.Description);
            Assert.AreEqual("Manufacturer#1", product.Manufacturer.Name);
            Assert.AreEqual(10.2f, product.Price);
            Assert.AreEqual("Category#1", product.Category.Name);
            Assert.AreEqual(1, product.CustomFields.Count());
            Assert.AreEqual("1", product.CustomFields[0].Value);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCustomFieldException))]
        public void TestProductCreateWithWrongCustomField()
        {
            // Arrange
            fakeProducts[0].CustomFields = new List<CustomFieldValue>()
            {
                new CustomFieldValue()
                {
                    CustomField = new CustomFieldNumber() { Name = "UnexistentCustomField#1" },
                    Value = "1",
                },
            };

            // Act
            this.businessLogic.Product.Create(fakeProducts[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCustomFieldValueException))]
        public void TestProductCreateWithWrongCustomFieldValue()
        {
            // Arrange
            fakeProducts[0].CustomFields = new List<CustomFieldValue>()
            {
                new CustomFieldValue()
                {
                    CustomField = new CustomFieldNumber() { Name = "CustomField#1" },
                    Value = "This is not a number",
                },
            };

            // Act
            this.businessLogic.Product.Create(fakeProducts[0]);
        }

        [TestMethod]
        public void TestProductUpdate()
        {
            // Arrange
            var code = this.businessLogic.Product.Create(fakeProducts[0]);

            // Act
            this.businessLogic.Product.Update(new Product()
            {
                Code = code,
                Name = "Product#2",
                Description = "test",
                Manufacturer = fakeManufacturers[1],
                Price = 20.2f,
                Category = fakeCategories[1],
                CustomFields = null
            });

            // Assert
            var product = this.businessLogic.Product.GetById(code);
            Assert.AreEqual(code, product.Code);
            Assert.AreEqual("Product#2", product.Name);
            Assert.AreEqual("test", product.Description);
            Assert.AreEqual("Manufacturer#2", product.Manufacturer.Name);
            Assert.AreEqual(20.2f, product.Price);
            Assert.AreEqual("Category#2", product.Category.Name);
            Assert.AreEqual(0, product.CustomFields.Count());
        }

        [TestMethod]
        public void TestProductUpdateAddCustomField()
        {
            // Arrange
            var code = this.businessLogic.Product.Create(fakeProducts[0]);

            // Act
            this.businessLogic.Product.Update(new Product()
            {
                Code = code,
                Name = "Product#2",
                Description = "test",
                Manufacturer = fakeManufacturers[1],
                Price = 20.2f,
                Category = fakeCategories[0],
                CustomFields = new List<CustomFieldValue>()
                {
                    new CustomFieldValue()
                    {
                        CustomField = new CustomFieldNumber() { Name = "CustomField#1" },
                        Value = "1",
                    },
                    new CustomFieldValue()
                    {
                        CustomField = new CustomFieldNumber() { Name = "CustomField#2" },
                        Value = "2",
                    }
                }
            });

            // Assert
            var product = this.businessLogic.Product.GetById(code);
            Assert.AreEqual(code, product.Code);
            Assert.AreEqual("Product#2", product.Name);
            Assert.AreEqual("test", product.Description);
            Assert.AreEqual("Manufacturer#2", product.Manufacturer.Name);
            Assert.AreEqual(20.2f, product.Price);
            Assert.AreEqual("Category#1", product.Category.Name);
            Assert.AreEqual(2, product.CustomFields.Count());
            Assert.AreEqual("1", product.CustomFields[0].Value);
            Assert.AreEqual("2", product.CustomFields[1].Value);
        }

        [TestMethod]
        public void TestProductUpdateRemoveCustomField()
        {
            // Arrange
            fakeProducts[0].CustomFields = new List<CustomFieldValue>()
            {
                new CustomFieldValue()
                {
                    CustomField = new CustomFieldNumber() { Name = "CustomField#1" },
                    Value = "1",
                },
                new CustomFieldValue()
                {
                    CustomField = new CustomFieldNumber() { Name = "CustomField#2" },
                    Value = "2",
                }
            };

            var code = this.businessLogic.Product.Create(fakeProducts[0]);

            // Act
            this.businessLogic.Product.Update(new Product()
            {
                Code = code,
                Name = "Product#1",
                Description = "",
                Manufacturer = fakeManufacturers[0],
                Price = 10.2f,
                Category = fakeCategories[0],
                CustomFields = new List<CustomFieldValue>()
                {
                    new CustomFieldValue()
                    {
                        CustomField = new CustomFieldNumber() { Name = "CustomField#1" },
                        Value = "1",
                    }
                },
            });

            // Assert
            var product = this.businessLogic.Product.GetById(code);
            Assert.AreEqual(code, product.Code);
            Assert.AreEqual("Product#1", product.Name);
            Assert.AreEqual("", product.Description);
            Assert.AreEqual("Manufacturer#1", product.Manufacturer.Name);
            Assert.AreEqual(10.2f, product.Price);
            Assert.AreEqual("Category#1", product.Category.Name);
            Assert.AreEqual(1, product.CustomFields.Count());
            Assert.AreEqual("1", product.CustomFields[0].Value);
        }

        [TestMethod]
        public void TestProductDelete()
        {
            // Arrange
            var code = this.businessLogic.Product.Create(fakeProducts[0]);

            // Act
            this.businessLogic.Product.DeleteById(code);

            // Assert
            Assert.IsNull(this.businessLogic.Product.GetById(code));
        }

        [TestMethod]
        public void TestProductList()
        {
            // Arrange
            this.businessLogic.Product.Create(fakeProducts[0]);

            // Assert
            Assert.AreEqual(1, this.businessLogic.Product.All().Count());
        }

        [TestMethod]
        public void TestProductFindByName()
        {
            // Arrange
            fakeProducts[0].Code = this.businessLogic.Product.Create(fakeProducts[0]);
            fakeProducts[1].Code = this.businessLogic.Product.Create(fakeProducts[1]);

            // Assert
            var results = this.businessLogic.Product.Find(p => p.Name.Contains("#1"));
            var product = results.ElementAt(0);
            Assert.AreEqual(1, results.Count());
            Assert.AreEqual(fakeProducts[0].Code, product.Code);
            Assert.AreEqual("Product#1", product.Name);
            Assert.AreEqual("abc", product.Description);
            Assert.AreEqual("Manufacturer#1", product.Manufacturer.Name);
            Assert.AreEqual(10.2f, product.Price);
            Assert.AreEqual("Category#1", product.Category.Name);
            Assert.AreEqual(1, product.CustomFields.Count());
            Assert.AreEqual("1", product.CustomFields[0].Value);
        }

        [TestMethod]
        public void TestProductFindByCategory()
        {
            // Arrange
            fakeProducts[0].Code = this.businessLogic.Product.Create(fakeProducts[0]);
            fakeProducts[1].Code = this.businessLogic.Product.Create(fakeProducts[1]);
            fakeProducts[2].Code = this.businessLogic.Product.Create(fakeProducts[2]);

            // Assert
            var results = this.businessLogic.Product.Find(p => p.Category.Id == fakeCategories[0].Id).OrderBy(p => p.Name);
            Assert.AreEqual(2, results.Count());
            Assert.AreEqual(fakeProducts[0].Code, results.ElementAt(0).Code);
            Assert.AreEqual(fakeProducts[1].Code, results.ElementAt(1).Code);
        }

        [TestMethod]
        public void TestProductFindByPriceLowerThan()
        {
            // Arrange
            fakeProducts[0].Code = this.businessLogic.Product.Create(fakeProducts[0]);
            fakeProducts[1].Code = this.businessLogic.Product.Create(fakeProducts[1]);
            fakeProducts[2].Code = this.businessLogic.Product.Create(fakeProducts[2]);

            // Assert
            var results = this.businessLogic.Product.Find(p => p.Price < 10);
            Assert.AreEqual(1, results.Count());
            Assert.AreEqual(fakeProducts[1].Code, results.ElementAt(0).Code);
        }

        [TestMethod]
        public void TestProductFindByPriceGreaterThan()
        {
            // Arrange
            fakeProducts[0].Code = this.businessLogic.Product.Create(fakeProducts[0]);
            fakeProducts[1].Code = this.businessLogic.Product.Create(fakeProducts[1]);
            fakeProducts[2].Code = this.businessLogic.Product.Create(fakeProducts[2]);

            // Assert
            var results = this.businessLogic.Product.Find(p => p.Price > 10).OrderBy(p => p.Name);
            Assert.AreEqual(2, results.Count());
            Assert.AreEqual(fakeProducts[0].Code, results.ElementAt(0).Code);
            Assert.AreEqual(fakeProducts[2].Code, results.ElementAt(1).Code);
        }

        [TestMethod]
        public void TestProductListReviews()
        {
            // Arrange
            var paymentMethod = new PaymentMethod() { Name = "PaymentMethod#1" };
            var shippingAddress = new ShippingAddress() { Address = "AddressMethod#1", FirstName = "Sport", LastName = "Store", Phone = "1234" };

            this.businessLogic.Product.Create(fakeProducts[0]);
            this.businessLogic.PaymentMethod.Create(paymentMethod);
            this.businessLogic.ShippingAddress.Create(shippingAddress);

            this.businessLogic.Cart.AddProduct(fakeProducts[0], 2);
            var purchase = this.businessLogic.Cart.CheckOut(paymentMethod, shippingAddress);

            this.businessLogic.Review.Create(new Review()
            {
                Comment = "Comment#1",
                PurchasedProduct = purchase.Products[0]
            });

            // Assert
            var reviews = this.businessLogic.Product.AllReviews(fakeProducts[0]);
            var review = reviews.ElementAt(0);
            Assert.AreEqual(1, reviews.Count());
            Assert.AreEqual("Comment#1", review.Comment);
            Assert.AreEqual(fakeProducts[0].Code, review.PurchasedProduct.Product.Code);
            Assert.AreEqual(2, review.PurchasedProduct.Quantity);
        }
    }
}
