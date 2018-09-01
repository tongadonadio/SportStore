using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Model.Exceptions
{
    public class InvalidCustomFieldException : Exception
    {
        public InvalidCustomFieldException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
