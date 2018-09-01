using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SportStore.Plugin;

namespace SportStore.BusinessLogic
{
    public interface IPluginBusinessLogic
    {
        IEnumerable<IPlugin> All();
        IEnumerable<T> AllOfType<T>() where T : IPlugin;
    }
}
