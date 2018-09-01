using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SportStore.Model;
using SportStore.Model.Reports;

namespace SportStore.BusinessLogic
{
    public interface IManagementBusinessLogic
    {
        PurchaseByCategoryReport PurchaseByCategoryReport(DateTime? startDateTime = null, DateTime? endDateTime = null);
        PurchasedProductRankingReport PurchasedProductRankingReport();
    }
}
