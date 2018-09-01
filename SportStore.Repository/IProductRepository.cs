using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SportStore.Model;

namespace SportStore.Repository
{
    public interface IProductRepository : ICRUDRepository<Product, Guid>
    {
    }
}
