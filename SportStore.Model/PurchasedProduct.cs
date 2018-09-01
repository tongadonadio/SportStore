using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Model
{
    public class PurchasedProduct
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }

        public PurchasedProduct()
        { }

        public PurchasedProduct(ProductInCart productInCart)
        {
            this.Id = Guid.NewGuid();
            this.Product = productInCart.Product;
            this.Quantity = productInCart.Quantity;
            this.Price = productInCart.Product.Price;
        }
    }
}
