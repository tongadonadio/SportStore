using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SportStore.Model;

namespace SportStore.Plugin
{
    public interface IProductImportPlugin : IPlugin
    {
        IEnumerable<Product> ImportProducts(IEnumerable<Category> categories, IEnumerable<Manufacturer> manufacturers);
    }
}
