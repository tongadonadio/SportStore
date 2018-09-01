using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SportStore.Plugin.ProductImport.Xml.Model
{
    [XmlRoot("Products")]
    public class ProductListXML
    {
        [XmlElement("Product")]
        public List<ProductXML> Items { get; set; }

        public ProductListXML()
        {
            this.Items = new List<ProductXML>();
        }
    }
}
