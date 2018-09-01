using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

using SportStore.BusinessLogic.V1;
using SportStore.Model;

namespace SportStore.BusinessLogic.V1.Tests
{
    [TestClass]
    public class RoleIntegrationTest : SportStoreIntegrationTest
    {
        private Role[] fakeRoles;

        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();

            SetUpRoleData();
        }

        private void SetUpRoleData()
        {
            var role = new Role[]
            {
                new Role()
                {
                    Name = "Role#1",
                    Description = "Role#1"
                }
            };

            this.fakeRoles = role;
        }


        [TestMethod]
        public void TestRoleCreate()
        {
            // Act
            var name = this.businessLogic.Role.Create(fakeRoles[0]);

            // Assert
            var role = this.businessLogic.Role.GetById(name);
            Assert.AreEqual(name, role.Name);
            Assert.AreEqual("Role#1", role.Description);
        }

        [TestMethod]
        public void TestRoleUpdate()
        {
            // Arrange
            var name = this.businessLogic.Role.Create(fakeRoles[0]);

            // Act
            this.businessLogic.Role.Update(new Role()
            {
                Name = name,
                Description = "Role#1.1"
            });

            // Assert
            var role = this.businessLogic.Role.GetById(name);
            Assert.AreEqual(name, role.Name);
            Assert.AreEqual("Role#1.1", role.Description);
        }

        [TestMethod]
        public void TestRoleDelete()
        {
            // Arrange
            var name = this.businessLogic.Role.Create(fakeRoles[0]);

            // Act
            this.businessLogic.Role.DeleteById(name);

            // Assert
            Assert.IsNull(this.businessLogic.Role.GetById(name));
        }

        [TestMethod]
        public void TestRoleList()
        {
            // Arrange
            this.businessLogic.Role.Create(fakeRoles[0]);

            // Assert
            Assert.AreEqual(3, this.businessLogic.Role.All().Count());
        }
    }
}
