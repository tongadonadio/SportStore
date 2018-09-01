using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SportStore.Model;

namespace SportStore.BusinessLogic
{
    public interface IPaymentMethodBusinessLogic: ICRUDBusinessLogic<PaymentMethod, Guid>
    {
    }
}
