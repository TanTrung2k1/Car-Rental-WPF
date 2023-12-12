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

namespace NguyenTanTrungWPF.PopUp
{
    /// <summary>
    /// Interaction logic for CustomerPopUp.xaml
    /// </summary>
    public partial class CustomerPopUp : Window
    {
        
        private ICustomerRepo _repo;
        public CustomerPopUp()
        {
            InitializeComponent();

            _repo = new CustomerRepo();

            

            
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
                if (txtID.Text.Trim().Equals("")) throw new Exception("Please input Staff ID");
                if (checkIdFormat(txtID.Text) == false) throw new Exception("Please input Staff ID correct format [CXXXXX] with X is the number ");
                if (txtName.Text.Trim().Equals("")) throw new Exception("Please input Full name");
                if (txtMobile.Text.Trim().Equals("")) throw new Exception("Please input mobile");
                if (txtEmail.Text.Trim().Equals("")) throw new Exception("Please input Email");
                if (txtPass.Password.Trim().Equals("")) throw new Exception("Please input Password");
                if (txtConfirm.Password.Trim().Equals("")) throw new Exception("Please input Confirm password");
                if (txtCard.Text.Trim().Equals("")) throw new Exception("Please input Identify card");
                if (txtLNumber.Text.Trim().Equals("")) throw new Exception("Please input Licence Number");
                if (txtLDate.Text.Trim().Equals("")) throw new Exception("Please input Licence Date");

                if (!txtConfirm.Password.Trim().Equals(txtPass.Password.Trim())) throw new Exception("Confirm password does not match the password");
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
                Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                try
                {
                    string id = removeWhitespace(txtID.Text);
                    string name = removeWhitespace(txtName.Text);
                    string mobile = removeWhitespace(txtMobile.Text);
                    if (!emailRegex.IsMatch(removeWhitespace(txtEmail.Text))) throw new Exception("Email phải khớp với mẫu example@FURentalSystem.com");
                    string email = removeWhitespace(txtEmail.Text);
                    string password = removeWhitespace(txtPass.Password);
                    string identifyCard = removeWhitespace(txtCard.Text);
                    string licenceNumber = removeWhitespace(txtLNumber.Text);
                    DateTime date = DateTime.Parse(txtLDate.Text);

                    Customer updateCustomer = new Customer();
                    updateCustomer.CustomerId = id;
                    updateCustomer.CustomerName = name;
                    updateCustomer.Mobile = mobile;
                    updateCustomer.CustomerEmail = email;
                    updateCustomer.CustomerPassword = password;
                    updateCustomer.IdentityCard = identifyCard;
                    updateCustomer.LicenceNumber = licenceNumber;
                    updateCustomer.LicenceDate = date;

                    var flag = _repo.GetCustomerById(id);
                    if(flag == null)
                    {
                        _repo.Add(updateCustomer);
                        MessageBox.Show("Thêm customer thành công", "Thông báo");

                        this.Close();

                    }
                    else
                    {
                        throw new Exception("Id đã tồn tại");
                    }
                    
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Thông báo");
                }
                
                

            }
        }
    }
}
