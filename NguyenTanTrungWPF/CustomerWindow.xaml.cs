using BusinessObjects.Models;
using NguyenTanTrungWPF.ViewPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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

namespace NguyenTanTrungWPF
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        public CustomerWindow(Customer customer)
        {
            InitializeComponent();
            frMain.Content = new CustomerPage(customer);
            tbWelcome.Text = "Welcome: " + customer.CustomerName;
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Window loginWindow = new LoginWindow();
            this.Close();
            loginWindow.ShowDialog();
        }
    }
}
