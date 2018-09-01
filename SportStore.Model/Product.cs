using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Model
{
    public class Product
    {
        [Key]
        public Guid Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public float Price { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Photo> Photos { get; set; }
        public List<CustomFieldValue> CustomFields { get; set; }
        public int Stock { get; set; }

        public Product()
        {
            this.Photos = new List<Photo>();
            this.CustomFields = new List<CustomFieldValue>();
        }
    }
}
