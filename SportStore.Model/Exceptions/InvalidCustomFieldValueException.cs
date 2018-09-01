using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Model.Exceptions
{
    public class InvalidCustomFieldValueException : Exception
    {
        public InvalidCustomFieldValueException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
