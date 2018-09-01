using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using SportStore.Model;
using SportStore.Plugin.ProductImport.Xml.Model;

namespace SportStore.Plugin.ProductImport.Xml
{
    public class ProductImportPlugin : IProductImportPlugin
    {
        public string Id => "SportStore.Plugin.ProductImport.Xml";

        public string Name => "XML Product Importer";

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
                var xmlSerializer = new XmlSerializer(typeof(ProductListXML));

                using (var reader = new StringReader(fileContents))
                {
                    var productsXML = xmlSerializer.Deserialize(reader) as ProductListXML;

                    foreach (var productXML in productsXML.Items)
                    {
                        products.Add(new Product()
                        {
                            Name = productXML.Name,
                            Description = productXML.Description,
                            Manufacturer = manufacturers.Single(m => m.Name == productXML.ManufacturerName),
                            Category = categories.Single(c => c.Name == productXML.CategoryName),
                            Price = productXML.Price,
                            Stock = productXML.Stock
                        });
                    }
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
