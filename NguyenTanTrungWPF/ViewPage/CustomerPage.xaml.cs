using BusinessObjects.Models;
using NguyenTanTrungWPF.PopUp;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace NguyenTanTrungWPF.ViewPage
{
    /// <summary>
    /// Interaction logic for CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : Page
    {
        private Customer cus;
        private ICustomerRepo _repo;
        private ICarRentalRepo _carRentalRepo;

        public CustomerPage(Customer customer)
        {
            InitializeComponent();
            cus = customer;
            _repo = new CustomerRepo();
            _carRentalRepo = new CarRentalRepo();

            loadInfor();
            loadHistoryRental();
            enableInput(false);
            btnSave.IsEnabled = false;


        }

        private void loadHistoryRental()
        {
            List<CarRental> carRental = _carRentalRepo.getListByCustomerId(cus.CustomerId);
            lvCarRental.ItemsSource = carRental;
        }

        private void loadInfor()
        {
            Customer flag = _repo.GetCustomerById(cus.CustomerId);
            txtCustomerID.Text = flag.CustomerId;
            txtCustomerName.Text = flag.CustomerName;
            txtMobile.Text = flag.Mobile;
            txtEmail.Text = flag.CustomerEmail;
            txtPassword.Password = flag.CustomerPassword;
            txtCard.Text = flag.IdentityCard;
            txtLNumber.Text = flag.LicenceNumber;

            txtDate.Text = flag.LicenceDate.ToString();
        }

        

        private void enableInput(bool flag)
        {
            txtCustomerID.IsEnabled = flag;
            txtCustomerName.IsEnabled = flag;
            txtMobile.IsEnabled = flag;
            txtEmail.IsEnabled = flag;
            txtPassword.IsEnabled = flag;
            txtCard.IsEnabled = flag;
            txtLNumber.IsEnabled = flag;
            txtDate.IsEnabled = flag;
        }


        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            //Window popup = new CustomerPopUp(cus);
            //popup.ShowDialog();
            enableInput(true);
            txtEmail.IsEnabled = false;
            txtCustomerID.IsEnabled = false;
            btnSave.IsEnabled = true;
            btnUpdate.IsEnabled = false;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringInput"></param>
        /// <returns>remove space in string</returns>
        private string removeWhitespace(string stringInput)
        {
            Regex trimmer = new Regex(@"\s\s+");
            stringInput = trimmer.Replace(stringInput.Trim(), " ");
            return stringInput;
        }

        private bool checkInput()
        {
            bool result = false;
            try
            {
                if (txtCustomerID.Text.Trim().Equals("")) throw new Exception("Please input Staff ID");
                if (checkIdFormat(txtCustomerID.Text) == false) throw new Exception("Please input Staff ID correct format [NVXXXX] with X is the number ");
                if (txtCustomerName.Text.Trim().Equals("")) throw new Exception("Please input Full name");
                if (txtMobile.Text.Trim().Equals("")) throw new Exception("Please input mobile");
                if (txtEmail.Text.Trim().Equals("")) throw new Exception("Please input Email");
                if (txtPassword.Password.Trim().Equals("")) throw new Exception("Please input Password");
                if (txtCard.Text.Trim().Equals("")) throw new Exception("Please input Identify card");
                if (txtLNumber.Text.Trim().Equals("")) throw new Exception("Please input Licence Number");

                
                result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
            return result;
        }

        private bool checkIdFormat(string staffID)
        {

            string format = "^[C|d]\\d{5}$";
            Regex rgx = new Regex(format);
            staffID = removeWhitespace(staffID);
            bool result = rgx.IsMatch(staffID);
            return result;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (checkInput())
            {
                string id = removeWhitespace(txtCustomerID.Text);
                string name = removeWhitespace(txtCustomerName.Text);
                string mobile = removeWhitespace(txtMobile.Text);
                string email = removeWhitespace(txtEmail.Text);
                string password = removeWhitespace(txtPassword.Password);
                string identifyCard = removeWhitespace(txtCard.Text);
                DateTime date = DateTime.Parse(txtDate.Text);
                string licenceNumber = removeWhitespace(txtLNumber.Text);
                

                Customer updateCustomer = new Customer();
                updateCustomer.CustomerId = id;
                updateCustomer.CustomerName = name;
                updateCustomer.Mobile = mobile;
                updateCustomer.CustomerEmail = email;
                updateCustomer.CustomerPassword = password;
                updateCustomer.IdentityCard = identifyCard;
                updateCustomer.LicenceNumber = licenceNumber;
                updateCustomer.LicenceDate = date;

                _repo.Update(updateCustomer);
                loadInfor();
                enableInput(false);
                btnUpdate.IsEnabled = true;
                btnSave.IsEnabled = false;

            }
        }
    }
}
