using BusinessObjects.Models;
using NguyenTanTrungWPF.ViewPage;
using Repository;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
    /// Interaction logic for StaffPopUp.xaml
    /// </summary>
    public partial class StaffPopUp : Window
    {
        private StaffAccount staff;
        private IStaffAccountRepo _repo;
        string function = null;
        
        public StaffPopUp(StaffAccount account)
        {
            InitializeComponent();
            staff = account;
            _repo = new StaffAccountRepo();

            if (staff != null)
            {
                loadInput();
                function = "Update";
            }
            else
            {
                function = "Create";
            }
            
            loadRoleToComboBox();
        }
        private void loadInput(){
            
            txtStaffID.Text = staff.StaffId.ToString();
            txtFullName.Text = staff.FullName.ToString();
            txtEmail.Text = staff.Email.ToString();
            txtPassword.Password = staff.Password.ToString();
        }
        private void loadRoleToComboBox()
        {
            List<string> roles = new List<string>();
            roles.Add("Staff");
            roles.Add("Manage");
            cbRole.ItemsSource = roles;
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
                if (txtStaffID.Text.Trim().Equals("")) throw new Exception("Please input Staff ID");
                if (checkIdFormat(txtStaffID.Text) == false) throw new Exception("Please input Staff ID correct format [NVXXXX] with X is the number ");
                if (txtFullName.Text.Trim().Equals("")) throw new Exception("Please input Full name");
                if (txtEmail.Text.Trim().Equals("")) throw new Exception("Please input Email");
                if (txtPassword.Password.Trim().Equals("")) throw new Exception("Please input Password");
                if (txtComfirmPassword.Password.Trim().Equals("")) throw new Exception("Please input Confirm Password");
                if (!txtComfirmPassword.Password.Trim().Equals(txtPassword.Password.Trim())) throw new Exception("Confirm password does not match the password");
                result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
            return result;
        }

        private bool checkId(string Id)
        {
            bool result = false;
            try
            {
                if (_repo.Get(Id) != null) throw new Exception("Staff id đã tồn tại");
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
            
            string format = "^N[V|d]\\d{4}$";
            Regex rgx = new Regex(format);
            staffID = removeWhitespace(staffID);
            bool result = rgx.IsMatch(staffID);
            return result;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (checkInput())
            {
                string id = removeWhitespace(txtStaffID.Text);
                string name = removeWhitespace(txtFullName.Text);
                string email = removeWhitespace(txtEmail.Text);
                string password = removeWhitespace(txtPassword.Password);
                
                string role = cbRole.Text;

                
                StaffAccount staffAccount = new StaffAccount();
                staffAccount.StaffId = id;
                staffAccount.FullName = name;
                staffAccount.Email = email;
                staffAccount.Password = password;
                if (role.Equals("Manage"))
                {
                    staffAccount.Role = 1;
                }
                else if (role.Equals("Staff"))
                {
                    staffAccount.Role = 2;
                }

                switch (function)
                {
                    case "Create":
                        if (checkId(staffAccount.StaffId))
                        {
                            _repo.Add(staffAccount);
                            this.Close();
                        }
                        break;
                    case "Update":
                        _repo.Update(staffAccount);
                        this.Close();
                        break;
                }
                
                
                
                
            }

            
            
        }
    }
}
