using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using SportStore.Plugin;

namespace SportStore.BusinessLogic.V1
{
    public class PluginBusinessLogic : IPluginBusinessLogic
    {
        private Dictionary<string, IPlugin> plugins;
        private string pluginsFolderRelativePath;

        public PluginBusinessLogic(string pluginsFolder)
        {
            this.plugins = new Dictionary<string, IPlugin>();
            this.pluginsFolderRelativePath = pluginsFolder;

            LoadPlugins();
        }

        private void LoadPlugins()
        {
            var pluginFolderPath = Directory.GetCurrentDirectory() + "\\" + this.pluginsFolderRelativePath;

            if (Directory.Exists(pluginFolderPath))
            {
                var pluginsFolderFiles = Directory.GetFiles(pluginFolderPath);

                foreach (var pluginFolderFile in pluginsFolderFiles)
                {
                    var pluginFolderAssembly = Assembly.LoadFile(pluginFolderFile);
                    var pluginFolderAssemblyTypes = pluginFolderAssembly.GetExportedTypes();

                    foreach (var pluginFolderAssemblyType in pluginFolderAssemblyTypes)
                    {
                        if (typeof(IPlugin).IsAssignableFrom(pluginFolderAssemblyType))
                        {
                            LoadPluginOfType(pluginFolderAssemblyType);
                        }
                    }
                }
            }
        }

        private void LoadPluginOfType(Type type)
        {
            var instance = Activator.CreateInstance(type) as IPlugin;

            instance.Initialize();

            this.plugins.Add(instance.Id, instance);
        }

        public IEnumerable<IPlugin> All()
        {
            return this.plugins.Values;
        }

        public IEnumerable<T> AllOfType<T>() where T : IPlugin
        {
            var pluginsOfTypeT = this.plugins.Values.Where(p => typeof(T).IsAssignableFrom(p.GetType()));

            return pluginsOfTypeT.Cast<T>();
        }
    }
}
