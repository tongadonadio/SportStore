using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using SportStore.App.Management.Components;
using SportStore.BusinessLogic;
using SportStore.Plugin;

namespace SportStore.App.Management
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ISportStoreBusinessLogic businessLogic;

        public MainWindow(ISportStoreBusinessLogic businessLogic)
        {
            InitializeComponent();

            this.businessLogic = businessLogic;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadProductImportPluginMenu();
        }

        private void ProductsButton_Click(object sender, RoutedEventArgs e)
        {
            ProductsUserControl userControl = new ProductsUserControl(this.businessLogic);
            MainContainer.Children.Clear();
            MainContainer.Children.Add(userControl);
        }

        private void DotsButton_Click(object sender, RoutedEventArgs e)
        {
            DotsUserControl userControl = new DotsUserControl(this.businessLogic);
            MainContainer.Children.Clear();
            MainContainer.Children.Add(userControl);
        }

        private void LogsButton_Click(object sender, RoutedEventArgs e)
        {
            LogsUserControl userControl = new LogsUserControl(this.businessLogic);
            MainContainer.Children.Clear();
            MainContainer.Children.Add(userControl);
        }

        private void LoadProductImportPluginMenu()
        {
            if (this.businessLogic.Config.GetPluginsEnabled() && this.businessLogic.Plugin != null)
            {
                var productImportMenuItem = new MenuItem() { Header = "Import _Products" };

                foreach (var plugin in this.businessLogic.Plugin.AllOfType<IProductImportPlugin>())
                {
                    var pluginMenuItem = new MenuItem() { Header = plugin.Name };

                    pluginMenuItem.Click += (sender, e) =>
                    {
                        var products = plugin.ImportProducts(this.businessLogic.Category.All(), this.businessLogic.Manufacturer.All());
                        var importedProducts = 0;

                        this.businessLogic.Product.CreateRangeWithoutThrowingException(products, out importedProducts);

                        MessageBox.Show(string.Format("{0} product(s) has been imported ({1} errors)", importedProducts, products.Count() - importedProducts));
                    };

                    productImportMenuItem.Items.Add(pluginMenuItem);
                }

                MainMenu.Items.Add(productImportMenuItem);
            }
        }
    }
}
