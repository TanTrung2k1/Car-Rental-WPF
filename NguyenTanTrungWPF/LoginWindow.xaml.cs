using BusinessObjects.Models;
using Repository;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private CarRentalSystemDBContext _context;
        private ICustomerRepo _customerRepo;
        private IStaffAccountRepo _staffAccountRepo;
        public LoginWindow()
        {
            InitializeComponent();
            _customerRepo = new CustomerRepo();
            _staffAccountRepo = new StaffAccountRepo();
        }

        private StaffAccount GetAdmin()
        {
            _context = new CarRentalSystemDBContext();
            string conn = _context.GetConnectionString();
            return _context.getDefaultAccount();
        }
        
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (checkInput())
            {
                string email = removeWhitespace(txtEmail.Text);
                string pass = removeWhitespace(txtPassword.Password);

                
                
                StaffAccount admin = GetAdmin();
                StaffAccount staff = _staffAccountRepo.GetByEmail(email);
                Customer customer = _customerRepo.GetCustomerByEmail(email);
                if(admin != null && email.Equals(admin.Email) && pass.Equals(admin.Password)){
                    Window window = new MainWindow(admin);
                    this.Close();
                    window.ShowDialog();
                }else if(staff != null && email.Equals(staff.Email) && pass.Equals(staff.Password))
                {
                    Window window = new MainWindow(staff);
                    this.Close();
                    window.ShowDialog();
                }
                else if(customer != null && email.Equals(customer.CustomerEmail) && pass.Equals(customer.CustomerPassword))
                {
                    Window window = new CustomerWindow(customer);
                    this.Close();
                    window.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Wrong Email or Password", "Thông báo");
                }
                
                
            }
            

            //string username = trimmer.Replace(txtUsername.Text.Trim(), " ");
            //string password = trimmer.Replace(txtPassword.Password.Trim(), "");
            //MessageBox.Show("Welcome " + username + " + " + password, "Thông báo");
            //checkInput(username, password);
        }

        
        private string removeWhitespace(string stringInput) {
            Regex trimmer = new Regex(@"\s\s+");
            stringInput = trimmer.Replace(stringInput.Trim(), " ");
            return stringInput;
        }


        private bool checkInput()
        {
            bool result = false;
            try
            {
                if (txtEmail.Text.Trim().Equals("")) throw new Exception("Please input username");
                if (txtPassword.Password.Trim().Equals("")) throw new Exception("Please input password");

                result = true;
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
                
            }
            return result;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtEmail.Clear();
            txtPassword.Clear();    
        }
    }
}
