using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Model
{
    public class Purchase
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public User User { get; set; }
        public List<PurchasedProduct> Products { get; set; }
        public float Total
        {
            get
            {
                var totalPrice = 0f;

                foreach (var item in this.Products)
                {
                    totalPrice += item.Quantity * item.Price;
                }

                return totalPrice;
            }
        }
        public PaymentMethod PaymentMethod { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
    }
}
