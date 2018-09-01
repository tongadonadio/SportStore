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
using System.Windows.Shapes;

using SportStore.BusinessLogic;
using SportStore.Model;

namespace SportStore.App.Management.Windows
{
    /// <summary>
    /// Lógica de interacción para ProductSelectWindow.xaml
    /// </summary>
    public partial class ProductSelectWindow : Window
    {
        private ISportStoreBusinessLogic businessLogic;

        public Product Product { get; set; }

        public ProductSelectWindow(ISportStoreBusinessLogic businessLogic)
        {
            InitializeComponent();

            this.businessLogic = businessLogic;

            ProductsListView.ItemsSource = this.businessLogic.Product.All();
        }

        private void ProductsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OkButton.IsEnabled = ProductsListView.SelectedItems.Count > 0;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Product = ProductsListView.SelectedItems[0] as Product;

            this.Close();
        }
    }
}
