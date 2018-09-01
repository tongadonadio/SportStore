using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

using SportStore.Model;

namespace SportStore.BusinessLogic.V1.Tests
{
    [TestClass]
    public class CategoryIntegrationTest : SportStoreIntegrationTest
    {
        protected override bool LoggedUserShouldBeAdministrator => true;

        private Category[] fakeCategories;

        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();

            SetUpCategoryData();
        }

        private void SetUpCategoryData()
        {
            var categories = new Category[]
            {
                new Category()
                {
                    Name = "Category#1",
                    Description = "test",
                    CustomFields = new List<CustomField>()
                    {
                        new CustomFieldNumber
                        {
                            Name = "CustomField#1",
                            Description = "test"
                        },
                        new CustomFieldNumber
                        {
                            Name = "CustomField#2",
                            Description = "test"
                        }
                    }
                }
            };

            this.fakeCategories = categories;
        }

        [TestMethod]
        public void TestCategoryCreate()
        {
            // Act
            var id = this.businessLogic.Category.Create(fakeCategories[0]);

            // Assert
            var category = this.businessLogic.Category.GetById(id);
            Assert.AreEqual(id, category.Id);
            Assert.AreEqual(2, category.CustomFields.Count());
        }

        [TestMethod]
        public void TestCategoryUpdate()
        {
            // Arrange
            var id = this.businessLogic.Category.Create(fakeCategories[0]);

            // Act
            this.businessLogic.Category.Update(new Category()
            {
                Id = id,
                Name = "Category#1.1",
                CustomFields = fakeCategories[0].CustomFields,
                Description = fakeCategories[0].Description
            });

            // Assert
            var category = this.businessLogic.Category.GetById(id);
            Assert.IsNotNull(category);
            Assert.AreEqual(id, category.Id);
            Assert.AreEqual("Category#1.1", category.Name);
            Assert.AreEqual(2, category.CustomFields.Count());
        }

        [TestMethod]
        public void TestCategoryUpdateAddCustomFields()
        {
            // Arrange
            var id = this.businessLogic.Category.Create(fakeCategories[0]);

            // Act
            this.businessLogic.Category.Update(new Category()
            {
                Id = id,
                Name = fakeCategories[0].Name,
                CustomFields = new List<CustomField>()
                {
                    new CustomFieldNumber
                    {
                        Name = "CustomField#1",
                        Description = "test"
                    },
                    new CustomFieldNumber
                    {
                        Name = "CustomField#2",
                        Description = "test"
                    },
                    new CustomFieldNumber
                    {
                        Name = "CustomField#3",
                        Description = "test"
                    }
                },
                Description = fakeCategories[0].Description
            });

            // Assert
            var category = this.businessLogic.Category.GetById(id);
            Assert.IsNotNull(category);
            Assert.AreEqual(id, category.Id);
            Assert.AreEqual(3, category.CustomFields.Count());
            Assert.AreEqual("CustomField#1", category.CustomFields[0].Name);
            Assert.AreEqual("CustomField#2", category.CustomFields[1].Name);
            Assert.AreEqual("CustomField#3", category.CustomFields[2].Name);
        }

        [TestMethod]
        public void TestCategoryUpdateRemoveCustomFields()
        {
            // Arrange
            var id = this.businessLogic.Category.Create(fakeCategories[0]);

            // Act
            this.businessLogic.Category.Update(new Category()
            {
                Id = id,
                Name = fakeCategories[0].Name,
                CustomFields = new List<CustomField>()
                {
                    new CustomFieldNumber
                    {
                        Name = "CustomField#1",
                        Description = "test"
                    },
                },
                Description = fakeCategories[0].Description
            });

            // Assert
            var category = this.businessLogic.Category.GetById(id);
            Assert.IsNotNull(category);
            Assert.AreEqual(id, category.Id);
            Assert.AreEqual(1, category.CustomFields.Count());
            Assert.AreEqual("CustomField#1", category.CustomFields[0].Name);
        }

        [TestMethod]
        public void TestCategoryDelete()
        {
            // Arrange
            var id = this.businessLogic.Category.Create(fakeCategories[0]);

            // Act
            this.businessLogic.Category.DeleteById(id);

            // Assert
            Assert.IsNull(this.businessLogic.Category.GetById(id));
        }

        [TestMethod]
        public void TestCategoryList()
        {
            // Arrange
            this.businessLogic.Category.Create(fakeCategories[0]);

            // Assert
            Assert.AreEqual(2, this.businessLogic.Category.All().Count());
        }
    }
}
