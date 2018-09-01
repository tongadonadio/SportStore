using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Model
{
    public class ProductInCart
    {
        public Guid Id { get; set; }
        [ForeignKey("Product")]
        public Guid ProductCode { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public ProductInCart()
        {
            this.Id = Guid.NewGuid();
        }

        public ProductInCart(Product product, int quantity)
            : this()
        {
            this.Product = product;
            this.Quantity = quantity;
        }

        public ProductInCart(PurchasedProduct purchasedProduct)
            : this(purchasedProduct.Product, purchasedProduct.Quantity) { }
    }
}
