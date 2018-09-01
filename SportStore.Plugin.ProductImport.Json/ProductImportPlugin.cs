using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SportStore.Model;
using SportStore.Plugin.ProductImport.Json.Model;

namespace SportStore.Plugin.ProductImport.Json
{
    public class ProductImportPlugin : IProductImportPlugin
    {
        public string Id => "SportStore.Plugin.ProductImport.Json";

        public string Name => "JSON Product Importer";

        public string Version => "1.0";

        public void Initialize()
        {
        }

        public IEnumerable<Product> ImportProducts(IEnumerable<Category> categories, IEnumerable<Manufacturer> manufacturers)
        {
            var openFileDialog = new OpenFileDialog();
            var openFileDialogResult = openFileDialog.ShowDialog();

            if (openFileDialogResult.HasValue && openFileDialogResult.Value)
            {
                return ImportProductsFromFile(openFileDialog.FileName, categories, manufacturers);
            }
            else
            {
                return new List<Product>();
            }
        }

        public IEnumerable<Product> ImportProductsFromFile(string filePath, IEnumerable<Category> categories, IEnumerable<Manufacturer> manufacturers)
        {
            var products = new List<Product>();

            try
            {
                var fileContents = File.ReadAllText(filePath);
                var productsJSON = JsonConvert.DeserializeObject<List<ProductJSON>>(fileContents);

                foreach (var productJSON in productsJSON)
                {
                    products.Add(new Product()
                    {
                        Name = productJSON.Name,
                        Description = productJSON.Description,
                        Manufacturer = manufacturers.Single(m => m.Name == productJSON.ManufacturerName),
                        Category = categories.Single(c => c.Name == productJSON.CategoryName),
                        Price = productJSON.Price,
                        Stock = productJSON.Stock
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return products;
        }
    }
}
