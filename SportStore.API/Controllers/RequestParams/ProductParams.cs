using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SportStore.Model;

namespace SportStore.API.Controllers.RequestParams
{
    public class ProductParams : Product, IRequestParams<Product>
    {
        public float MinPrice { get; set; }
        public float MaxPrice { get; set; }

        public Predicate<Product> GetPredicate()
        {
            return p =>
            {
                return
                (
                    (this.Code != null && p.Code == this.Code) ||
                    (this.Name != null && p.Name.IndexOf(this.Name) >= 0) ||
                    (this.Description != null && p.Description.IndexOf(this.Description) >= 0) ||
                    (this.ManufacturerId != null && p.Manufacturer.Id == this.ManufacturerId) ||
                    (this.Price != 0 && p.Price == this.Price) ||
                    (this.CategoryId != null && p.Category.Id == this.CategoryId) ||
                    (this.MinPrice != 0 && p.Price > this.MinPrice) ||
                    (this.MaxPrice != 0 && p.Price > this.MaxPrice)
                );
            };
        }
    }
}