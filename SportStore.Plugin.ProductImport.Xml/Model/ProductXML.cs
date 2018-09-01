using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SportStore.Plugin.ProductImport.Xml.Model
{
    public class ProductXML
    {
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "Description")]
        public string Description { get; set; }
        [XmlElement(ElementName = "Manufacturer")]
        public string ManufacturerName { get; set; }
        [XmlElement(ElementName = "Price")]
        public float Price { get; set; }
        [XmlElement(ElementName = "Category")]
        public string CategoryName { get; set; }
        //[XmlElement(ElementName = "CustomFields")]
        //public List<CustomFieldValue> CustomFields { get; set; }
        [XmlElement(ElementName = "Stock")]
        public int Stock { get; set; }
    }
}
