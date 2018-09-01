using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

using SportStore.BusinessLogic.V1.Tests;

namespace SportStore.Plugin.ProductImport.Json.Tests
{
    [TestClass]
    public class ProductImportPluginTest : SportStoreIntegrationTest
    {
        protected override bool LoggedUserShouldBeAdministrator => true;

        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();
        }

        [TestMethod]
        public void ImportProductsTest()
        {
            var plugin = new ProductImportPlugin();
            var products = plugin.ImportProductsFromFile(@"Data\products.json", this.businessLogic.Category.All(), this.businessLogic.Manufacturer.All());

            Assert.AreEqual(1, products.Count());
        }
    }
}
