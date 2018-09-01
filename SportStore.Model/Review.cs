using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Model
{
    public class Review
    {
        public Guid Id { get; set; }
        public PurchasedProduct PurchasedProduct { get; set; }
        public string Comment { get; set; }
        public User User { get; set; }
    }
}
