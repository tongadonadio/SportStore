using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window, INotifyPropertyChanged
    {
        private ISportStoreBusinessLogic businessLogic;

        public Product Product { get; set; }
        public bool SaveChanges { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ProductWindow(ISportStoreBusinessLogic businessLogic, Product product)
        {
            this.businessLogic = businessLogic;

            InitializeComponent();
            InitializeComboBoxes();
            InitializeListViews();

            this.Product = product != null ? CloneProduct(product) : new Product();
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Product"));

            this.SaveChanges = false;
        }

        private Product CloneProduct(Product product)
        {
            Category productCategory = null;
            Manufacturer productManufacturer = null;

            foreach (Category category in CategoryComboBox.Items)
            {
                if (category.Id == product.Category.Id)
                {
                    productCategory = category;
                    break;
                }
            }

            foreach (Manufacturer manufacturer in ManufacturerComboBox.Items)
            {
                if (manufacturer.Id == product.Manufacturer.Id)
                {
                    productManufacturer = manufacturer;
                    break;
                }
            }

            return new Product()
            {
                Code = product.Code,
                Name = product.Name,
                Description = product.Description,
                Manufacturer = productManufacturer,
                Price = product.Price,
                Category = productCategory,
                Photos = product.Photos,
                CustomFields = product.CustomFields,
                Stock = product.Stock
            };
        }

        private void InitializeComboBoxes()
        {
            this.CategoryComboBox.ItemsSource = this.businessLogic.Category.All();
            this.ManufacturerComboBox.ItemsSource = this.businessLogic.Manufacturer.All();
        }

        private void InitializeListViews()
        {
            this.PropertyChanged += (sender, e) =>
            {
                this.CustomFieldListView.ItemsSource = null;
                this.CustomFieldListView.ItemsSource = this.Product.CustomFields;

                CategoryComboBox_SelectionChanged(sender, null); // To refresh the custom field's list add button
            };
        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.CustomFieldsAddButton.IsEnabled = this.CategoryComboBox.SelectedValue != null && (this.CategoryComboBox.SelectedValue as Category).CustomFields.Count > 0;
            this.CustomFieldsRemoveButton.IsEnabled = this.CustomFieldListView.SelectedItems.Count > 0;

            if (e != null)
            {
                if (this.CustomFieldListView.Items.Count > 0)
                {
                    var firstCustomField = this.CustomFieldListView.Items[0] as CustomFieldValue;
                    var selectedCateory = this.CategoryComboBox.SelectedValue as Category;

                    if (!selectedCateory.CustomFields.Any(cfv => cfv.Name == firstCustomField.CustomField.Name))
                    {
                        this.Product.CustomFields.Clear();
                    }
                }

                // To refresh the custom field's list
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Product"));
            }
        }

        private void CustomFieldListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.CustomFieldsRemoveButton.IsEnabled = this.CustomFieldListView.SelectedItems.Count > 0;
        }

        private void CustomFieldsAddButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new CustomFieldValueWindow(this.Product.Category);
            window.ShowDialog();

            if (window.SaveChanges)
            {
                var existingCustomField = this.Product.CustomFields.Find(cfv => cfv.CustomField.Name == window.CustomFieldValue.CustomField.Name);
                
                if (existingCustomField == null)
                {
                    this.Product.CustomFields.Add(window.CustomFieldValue);
                }
                else
                {
                    existingCustomField.Value = window.CustomFieldValue.Value;
                }
                
                // To refresh the custom field's list
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Product"));
            }
        }

        private void CustomFieldsRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.CustomFieldListView.SelectedItems.Count > 0)
            {
                foreach (var selectedItem in this.CustomFieldListView.SelectedItems)
                {
                    var selectedCustomFieldValue = selectedItem as CustomFieldValue;
                    var productCustomFieldValue = this.Product.CustomFields.Find(cfv => cfv.CustomField.Name == selectedCustomFieldValue.CustomField.Name);

                    this.Product.CustomFields.Remove(productCustomFieldValue);
                }

                // To refresh the custom field's list
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Product"));
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.SaveChanges = true;
            this.Close();
        }
    }
}
