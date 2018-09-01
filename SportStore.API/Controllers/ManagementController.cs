using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

using SportStore.BusinessLogic;
using SportStore.Model.Reports.ResultRows;

namespace SportStore.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ManagementController : SportStoreController
    {
        public ManagementController(ISportStoreBusinessLogic businessLogic)
        {
            this.businessLogic = businessLogic;
        }

        // GET: api/Management/Report/PurchaseByCategory
        [HttpGet]
        [Route("api/Management/Report/PurchaseByCategory")]
        public IEnumerable<PurchaseByCategoryReportResultRow> PurchaseByCategory(DateTime? startDateTime = null, DateTime? endDateTime = null)
        {
            AuthenticateWithTokenInHeaders();

            var report = this.businessLogic.Management.PurchaseByCategoryReport(startDateTime, endDateTime);

            return report.Results;
        }

        // GET: api/Management/Report/PurchasedProductRanking
        [HttpGet]
        [Route("api/Management/Report/PurchasedProductRanking")]
        public IEnumerable<PurchasedProductRankingReportResultRow> PurchasedProductRanking()
        {
            AuthenticateWithTokenInHeaders();

            var report = this.businessLogic.Management.PurchasedProductRankingReport();

            return report.Results;
        }
    }
}
