using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SportStore.Model;

namespace SportStore.BusinessLogic
{
    public interface IProductBusinessLogic : ICRUDBusinessLogic<Product, Guid>
    {
        void CreateRangeWithoutThrowingException(IEnumerable<Product> products, out int createdProducts);

        IEnumerable<Review> AllReviews(Product product);
    }
}
