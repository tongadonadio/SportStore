using SportStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.BusinessLogic
{
    public interface ICartBusinessLogic
    {
        Cart GetForCurrentSession();
        Cart AddProduct(Product product, int quantity);
        Cart RemoveProduct(Product product, int quantity);
        Purchase CheckOut(PaymentMethod payment, ShippingAddress shippingAddress);
    }
}
