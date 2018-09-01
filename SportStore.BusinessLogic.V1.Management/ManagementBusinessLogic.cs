using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SportStore.BusinessLogic.V1;
using SportStore.Model;
using SportStore.Model.Reports;
using SportStore.Repository;

namespace SportStore.BusinessLogic.V1.Management
{
    public class ManagementBusinessLogic : IManagementBusinessLogic
    {
        private ISportStoreRepository repository;

        public ManagementBusinessLogic(ISportStoreRepository repository)
        {
            this.repository = repository;
        }

        // TODO: Clean code en este método
        public PurchaseByCategoryReport PurchaseByCategoryReport(DateTime? startDateTime = null, DateTime? endDateTime = null)
        {
            var now = DateTime.Now;
            startDateTime = startDateTime ?? new DateTime(now.Year, now.Month, 1, 0, 0, 0, now.Kind);
            endDateTime = endDateTime ?? new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month), 0, 0, 0, now.Kind);

            var purchases = this.repository.PurchaseRepository.Find(p => p.DateTime >= startDateTime && p.DateTime <= endDateTime);
            var intermediateResults = new SortedDictionary<Category, float>(Comparer<Category>.Create((c1, c2) => c1.Name.CompareTo(c2.Name)));

            foreach (var purchase in purchases)
            {
                foreach (var purchasedProduct in purchase.Products)
                {
                    var c = purchasedProduct.Product.Category;
                    var p = purchasedProduct.Price * purchasedProduct.Quantity;

                    if (intermediateResults.ContainsKey(c))
                    {
                        intermediateResults[c] += p;
                    }
                    else
                    {
                        intermediateResults.Add(c, p);
                    }
                }
            }

            return new PurchaseByCategoryReport(intermediateResults);
        }

        public PurchasedProductRankingReport PurchasedProductRankingReport()
        {
            var purchases = this.repository.PurchaseRepository.All();
            var intermediateResults = new Dictionary<Product, int>();

            foreach (var purchase in purchases)
            {
                foreach (var purchasedProduct in purchase.Products)
                {
                    var p = purchasedProduct.Product;
                    var q = purchasedProduct.Quantity;

                    if (intermediateResults.ContainsKey(p))
                    {
                        intermediateResults[p] += q;
                    }
                    else
                    {
                        intermediateResults.Add(p, q);
                    }
                }
            }

            var sortedIntermediateResults = intermediateResults
                                                .OrderBy(kv => kv.Key.Name)
                                                .OrderBy(kv => kv.Value, Comparer<int>.Create((v1, v2) => v1.CompareTo(v2) * -1))
                                                .ToDictionary(pair => pair.Key, pair => pair.Value);

            return new PurchasedProductRankingReport(sortedIntermediateResults);
        }
    }
}
