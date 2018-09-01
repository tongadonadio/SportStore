using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Plugin.ProductImport.Json.Model
{
    public class ProductJSON
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ManufacturerName { get; set; }
        public float Price { get; set; }
        public string CategoryName { get; set; }
        public int Stock { get; set; }
    }
}
