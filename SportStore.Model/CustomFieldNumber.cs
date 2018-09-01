using System;

using SportStore.Model.Exceptions;

namespace SportStore.Model
{
    public class CustomFieldNumber : CustomField
    {
        public override void ValidateValue(string value)
        {
            try
            {
                var v = float.Parse(value);
            }
            catch (Exception ex)
            {
                throw new InvalidCustomFieldValueException("The CustomFieldValue is not a Number: " + value, ex);
            }
        }
    }
}
