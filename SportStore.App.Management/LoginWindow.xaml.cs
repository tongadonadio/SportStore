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
using SportStore.BusinessLogic.V1;
using SportStore.Model;
using SportStore.Repository.Entity;

namespace SportStore.App.Management
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        ISportStoreBusinessLogic businessLogic;

        public LoginWindow()
        {
            InitializeComponent();

            this.businessLogic = new SportStoreBusinessLogic<SportStoreRepository>();
            this.businessLogic.SetUpInitialData();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text;
            var password = PasswordTextBox.Text;

            try
            {
                this.businessLogic.Auth.Login(username, password);

                if (this.businessLogic.Auth.CurrentSession.User.Role.Name == RoleName.Administrator)
                {
                    MainWindow window = new MainWindow(this.businessLogic);
                    window.Show();
                    window.Closed += (s, ev) => this.Close();

                    this.Hide();
                }
                else
                {
                    this.businessLogic.Auth.Logout();

                    ShowErrorMessage("The app is intended for admin users only");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
