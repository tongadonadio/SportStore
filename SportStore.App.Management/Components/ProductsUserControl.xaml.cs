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

using SportStore.App.Management.Windows;
using SportStore.BusinessLogic;
using SportStore.Model;

namespace SportStore.App.Management.Components
{
    /// <summary>
    /// Interaction logic for ProductsUserControl.xaml
    /// </summary>
    public partial class ProductsUserControl : UserControl
    {
        private ISportStoreBusinessLogic businessLogic;

        public ProductsUserControl(ISportStoreBusinessLogic businessLogic)
        {
            InitializeComponent();

            this.businessLogic = businessLogic;

            this.Loaded += ProductsUserControl_Loaded;
        }

        private void ProductsUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ProductsListView.ItemsSource = this.businessLogic.Product.All();
        }

        private void ProductCreateButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new ProductWindow(this.businessLogic, null);
            window.ShowDialog();

            if (window.SaveChanges)
            {
                this.businessLogic.Product.Create(window.Product);
                ProductsUserControl_Loaded(this, null); // To refresh the product's list
            }
        }

        private void ProductUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new ProductWindow(this.businessLogic, ProductsListView.SelectedItem as Product);
            window.ShowDialog();

            if (window.SaveChanges)
            {
                this.businessLogic.Product.Update(window.Product);
                ProductsUserControl_Loaded(this, null); // To refresh the product's list
            }
        }

        private void ProductDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var count = ProductsListView.SelectedItems.Count;

            if (MessageBox.Show(string.Format("¿Está seguro que desea eliminar {0} productos?", count), "SportStore", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            {
                foreach (var selectedItem in ProductsListView.SelectedItems)
                {
                    this.businessLogic.Product.Delete(selectedItem as Product);
                }

                ProductsUserControl_Loaded(this, null); // To refresh the product's list
            }
        }

        private void ProductsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductUpdateButton.IsEnabled = ProductsListView.SelectedItems.Count == 1;
            ProductDeleteButton.IsEnabled = ProductsListView.SelectedItems.Count >= 1;
        }
    }
}
