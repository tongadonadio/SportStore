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
    public class CategoryControllerUnitTest
    {
        [TestMethod]
        public void TestCategoryAll()
        {
            // Arrange
            var expectedCategories = FakeCategories();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Category.All())
                .Returns(expectedCategories);

            var controller = new CategoryController(mockBusinessLogic.Object);

            // Act
            var obtainedCategories = controller.Get();

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(obtainedCategories);
            Assert.AreEqual(expectedCategories.Count(), obtainedCategories.Count());
        }

        [TestMethod]
        public void TestCategoryGetById()
        {
            // Arrange
            var expectedCategory = FakeCategories().First();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Category.GetById(expectedCategory.Id))
                .Returns(expectedCategory);

            var controller = new CategoryController(mockBusinessLogic.Object);

            // Act
            var result = controller.Get(expectedCategory.Id);
            var obtainedCategory = (result as OkNegotiatedContentResult<Category>).Content;

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(obtainedCategory);
            Assert.AreEqual(expectedCategory.Id, obtainedCategory.Id);
        }

        [TestMethod]
        public void TestCategoryGetByIdNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Category.GetById(id))
                .Returns(null as Category);

            var controller = new CategoryController(mockBusinessLogic.Object);

            // Act
            var result = controller.Get(id);

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void TestCategoryCreate()
        {
            // Arrange
            var fakeCategory = FakeCategories().First();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Category.Create(fakeCategory))
                .Returns(fakeCategory.Id);

            var controller = new CategoryController(mockBusinessLogic.Object);

            // Act
            var result = controller.Post(fakeCategory);
            var createdCategoryCode = (result as OkNegotiatedContentResult<Guid>).Content;

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(createdCategoryCode);
            Assert.AreEqual(fakeCategory.Id, createdCategoryCode);
        }

        [TestMethod]
        public void TestCategoryUpdate()
        {
            // Arrange
            var fakeCategory = FakeCategories().First();

            fakeCategory.Description = "Description#2";

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Category.Update(fakeCategory));

            var controller = new CategoryController(mockBusinessLogic.Object);

            // Act
            var result = controller.Put(fakeCategory.Id, fakeCategory);
            var updatedCategoryId = (result as OkNegotiatedContentResult<Guid>).Content;

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsNotNull(updatedCategoryId);
            Assert.AreEqual(fakeCategory.Id, updatedCategoryId);
        }

        [TestMethod]
        public void TestCategoryDelete()
        {
            // Arrange
            var fakeCategory = FakeCategories().First();

            var mockBusinessLogic = new Mock<ISportStoreBusinessLogic>();
            mockBusinessLogic
                .Setup(bl => bl.Category.DeleteById(fakeCategory.Id));

            var controller = new CategoryController(mockBusinessLogic.Object);

            // Act
            var result = controller.Delete(fakeCategory.Id);

            // Assert
            mockBusinessLogic.VerifyAll();

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private IEnumerable<Category> FakeCategories()
        {
            return new List<Category>()
            {
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Category#1",
                    Description = "Remera roja, marca Nike",
                },
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Category#2",
                    Description = "Campera Dry-Fit negra, marca Nike",
                }
            };
        }
    }
}
