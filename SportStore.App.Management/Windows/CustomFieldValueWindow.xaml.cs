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
    /// Interaction logic for CustomFieldValueWindow.xaml
    /// </summary>
    public partial class CustomFieldValueWindow : Window, INotifyPropertyChanged
    {
        public CustomFieldValue CustomFieldValue { get; set; }
        public bool SaveChanges { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public CustomFieldValueWindow(Category category)
        {
            InitializeComponent();
            InitializeComboBoxes(category);

            this.CustomFieldValue = new CustomFieldValue();
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CustomFieldValue"));

            this.SaveChanges = false;
        }

        private void InitializeComboBoxes(Category category)
        {
            this.CustomFieldComboBox.ItemsSource = category.CustomFields;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.SaveChanges = true;
            this.Close();
        }
    }
}
