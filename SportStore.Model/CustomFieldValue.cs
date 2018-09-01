using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Model
{
    public class CustomFieldValue
    {
        private string value;

        [Key]
        [ForeignKey("CustomField")]
        public string CustomFieldName { get; set; }
        public CustomField CustomField { get; set; }
        public string Value
        {
            get;set;
        }
    }
}
