using BusinessObjects.Models;
using NguyenTanTrungWPF.ViewPage;
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

namespace NguyenTanTrungWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(StaffAccount account)
        {
            InitializeComponent();
            if(account.Role == 0)
            {
                frMain.Content = new AdminPage();
                
            } else if (account.Role == 1 || account.Role ==2)
            {
                frMain.Content = new StaffPage(account);
            }
            
            tbWelcome.Text = "Welcome: " + account.FullName;

        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Window loginWindow = new LoginWindow();
            this.Close();
            loginWindow.ShowDialog();
            
        }
    }
}
