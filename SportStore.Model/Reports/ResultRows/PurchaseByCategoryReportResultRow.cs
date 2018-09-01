using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Model.Reports.ResultRows
{
    public class PurchaseByCategoryReportResultRow
    {
        public Category Category { get; set; }
        public float SubTotal { get; set; }
        public float PercentageOfTotal { get; set; }
    }
}
