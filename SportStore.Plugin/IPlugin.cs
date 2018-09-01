using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Plugin
{
    public interface IPlugin
    {
        string Id { get; }
        string Name { get; }
        string Version { get; }

        void Initialize();
    }
}
