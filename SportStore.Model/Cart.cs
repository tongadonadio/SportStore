using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Model
{
    public class Cart
    {
        public Guid Id { get; set; }
        public List<ProductInCart> Products { get; set; }
        [NotMapped]
        public float Total
        {
            get
            {
                var totalPrice = 0f;

                foreach (var item in this.Products)
                {
                    totalPrice += item.Quantity * item.Product.Price;
                }

                return totalPrice;
            }
        }

        public Cart()
        {
            this.Products = new List<ProductInCart>();
        }
    }
}
