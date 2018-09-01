using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Model
{
    public abstract class CustomField
    {
        [Key]
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public abstract void ValidateValue(string value);
    }
}
