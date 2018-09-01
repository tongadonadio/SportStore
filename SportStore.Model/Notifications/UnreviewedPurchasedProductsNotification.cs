using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Model.Notifications
{
    public class UnreviewedPurchasedProductsNotification : Notification
    {
        public IEnumerable<PurchasedProduct> PurchasedProducts;

        public UnreviewedPurchasedProductsNotification(IEnumerable<PurchasedProduct> purchasedProducts)
        {
            this.PurchasedProducts = purchasedProducts;
        }
    }
}
