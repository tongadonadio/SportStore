using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Model
{
    public class CustomFieldString : CustomField
    {
        public override void ValidateValue(string value)
        {
            // every value will be fine
        }
    }
}
