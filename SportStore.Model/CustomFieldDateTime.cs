using System;

using SportStore.Model.Exceptions;

namespace SportStore.Model
{
    public class CustomFieldDateTime : CustomField
    {
        public override void ValidateValue(string value)
        {
            try
            {
                DateTime.Parse(value);
            }
            catch (Exception ex)
            {
                throw new InvalidCustomFieldValueException("The CustomFieldValue is not a DateTime: " + value, ex);
            }
        }
    }
}
