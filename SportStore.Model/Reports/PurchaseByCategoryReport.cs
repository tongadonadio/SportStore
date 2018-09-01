using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SportStore.Model.Reports.ResultRows;

namespace SportStore.Model.Reports
{
    public class PurchaseByCategoryReport : IReport<PurchaseByCategoryReportResultRow>
    {
        private IEnumerable<PurchaseByCategoryReportResultRow> results;

        public IEnumerable<PurchaseByCategoryReportResultRow> Results => results;

        public PurchaseByCategoryReport(IDictionary<Category, float> intermediateResults)
        {
            this.results = GenerateResults(intermediateResults);
        }

        private IEnumerable<PurchaseByCategoryReportResultRow> GenerateResults(IDictionary<Category, float> intermediateResults)
        {
            var results = new List<PurchaseByCategoryReportResultRow>();

            var total = 0f;

            foreach (var intermediateResult in intermediateResults)
            {
                results.Add(new PurchaseByCategoryReportResultRow()
                {
                    Category = intermediateResult.Key,
                    SubTotal = intermediateResult.Value,
                });

                total += intermediateResult.Value;
            }

            foreach (var result in results)
            {
                result.PercentageOfTotal = total / result.SubTotal;
            }

            return results;
        }
    }
}
