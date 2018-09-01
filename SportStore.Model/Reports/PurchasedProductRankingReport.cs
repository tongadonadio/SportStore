using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SportStore.Model.Reports.ResultRows;

namespace SportStore.Model.Reports
{
    public class PurchasedProductRankingReport : IReport<PurchasedProductRankingReportResultRow>
    {
        private IEnumerable<PurchasedProductRankingReportResultRow> results;

        public IEnumerable<PurchasedProductRankingReportResultRow> Results => results;

        public PurchasedProductRankingReport(IDictionary<Product, int> intermediateResults)
        {
            this.results = GenerateResults(intermediateResults);
        }

        private IEnumerable<PurchasedProductRankingReportResultRow> GenerateResults(IDictionary<Product, int> intermediateResults)
        {
            var results = new List<PurchasedProductRankingReportResultRow>();

            foreach (var intermediateResult in intermediateResults)
            {
                results.Add(new PurchasedProductRankingReportResultRow()
                {
                    Product = intermediateResult.Key,
                    Quantity = intermediateResult.Value,
                });
            }

            return results;
        }
    }
}
