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
    /// Interaction logic for DotsUserControl.xaml
    /// </summary>
    public partial class DotsUserControl : UserControl
    {
        private ISportStoreBusinessLogic businessLogic;

        public DotsUserControl(ISportStoreBusinessLogic businessLogic)
        {
            InitializeComponent();

            this.businessLogic = businessLogic;

            this.Loaded += DotsUserControl_Loaded;
        }

        private void DotsUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DotPriceTextBox.Text = this.businessLogic.Config.GetDotPrice().ToString();
            BlackListListView.ItemsSource = this.businessLogic.Config.GetDotBlackList().Select(guid => this.businessLogic.Product.GetById(guid));
        }

        private void DotPriceTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var dotPrice = float.Parse(DotPriceTextBox.Text);

                this.businessLogic.Config.SetDotPrice(dotPrice);
            }
            catch (Exception ex)
            {
                // TODO: Show error
                DotPriceTextBox.Text = this.businessLogic.Config.GetDotPrice().ToString();
            }
        }

        private void BlackListListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BlackListRemoveProductButton.IsEnabled = BlackListListView.SelectedItems.Count > 0;
        }

        private void BlackListAddProductButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var window = new ProductSelectWindow(this.businessLogic);
                window.ShowDialog();

                if (window.Product != null)
                {
                    this.businessLogic.Config.AddProductToDotBlackList(window.Product.Code);

                    BlackListListView.ItemsSource = this.businessLogic.Config.GetDotBlackList().Select(guid => this.businessLogic.Product.GetById(guid)); // To reload black list list view
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BlackListRemoveProductButton_Click(object sender, RoutedEventArgs e)
        {
            var product = BlackListListView.SelectedItem as Product;

            try
            {
                this.businessLogic.Config.RemoveProductFromDotBlackList(product.Code);

                BlackListListView.ItemsSource = this.businessLogic.Config.GetDotBlackList().Select(guid => this.businessLogic.Product.GetById(guid)); // To reload black list list view
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
