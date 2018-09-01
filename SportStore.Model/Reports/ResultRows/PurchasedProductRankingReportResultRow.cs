using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Model.Reports.ResultRows
{
    public class PurchasedProductRankingReportResultRow
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
