using BusinessObjects.Models;
using NguyenTanTrungWPF.PopUp;
using Repository;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace NguyenTanTrungWPF.ViewPage
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        private IStaffAccountRepo _repo;
        private StaffAccount update_account;
        string function = null;
        public AdminPage()
        {
            InitializeComponent();
            _repo = new StaffAccountRepo();
            update_account = new StaffAccount();
            //
            loadRoleToComboBox();
            loadStaffToDG();
            enableInput(false);
            enableButton(false);
            btnCreate.IsEnabled = true;

        }
        private void loadRoleToComboBox()
        {
            List<string> roles = new List<string>();
            roles.Add("Staff");
            roles.Add("Manage");
            cbRole.ItemsSource = roles;
        }
        private void loadStaffToDG()
        {
            var listStaff = _repo.GetAll();
            lvStaff.ItemsSource = listStaff;
            //dgStaff.ItemsSource = 
        }

        private void enableInput(bool flag)
        {
            txtStaffID.IsEnabled = flag;
            txtFullName.IsEnabled = flag;
            txtEmail.IsEnabled = flag;
            txtPassword.IsEnabled = flag;
            cbRole.IsEnabled = flag;
        }
        private void enableButton(bool flag)
        {
            btnCreate.IsEnabled = flag;
            btnUpdate.IsEnabled = flag;
            btnDelete.IsEnabled = flag;
            btnSave.IsEnabled = flag;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            clearInput();
            enableInput(true);
            enableButton(false);
            btnSave.IsEnabled = true;
            lvStaff.IsEnabled = false;
            function = "create";

            //StaffAccount create_account = null;
            //Window popup = new StaffPopUp(create_account);
            //popup.ShowDialog();
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            
            enableInput(true);
            txtStaffID.IsEnabled = false;
            enableButton(false);

            btnSave.IsEnabled = true;
            lvStaff.IsEnabled = false;
            function = "update";
            //StaffAccount account = update_account;
            // Window popup = new StaffPopUp(account);
            //popup.ShowDialog();
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

        

        

       
        private void lvStaff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lvStaff.SelectedIndex == -1)
            {
                return;
            }
            StaffAccount staff = lvStaff.SelectedItem as StaffAccount;
            //update_account.StaffId = staff.StaffId;
            //update_account.FullName = staff.FullName;
            //update_account.Email = staff.Email;
            //update_account.Password = staff.Password;
            //update_account.Role = staff.Role;
            txtStaffID.Text = staff.StaffId;
            txtFullName.Text = staff.FullName;
            txtEmail.Text = staff.Email;
            txtPassword.Password = staff.Password;

            btnUpdate.IsEnabled = true;
            btnDelete.IsEnabled = true;
        }

        

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var response = MessageBox.Show("Do you want delete staff?", "Thông báo"
                            ,MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(response == MessageBoxResult.Yes)
            {
                _repo.Delete(txtStaffID.Text);
                clearInput();
                loadStaffToDG();
            }
            else if(response == MessageBoxResult.No)
            {
                e.Handled = true;
            }
        }

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            lvStaff.ItemsSource = _repo.GetAll();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (checkInput())
            {

                Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                try
                {
                    string id = removeWhitespace(txtStaffID.Text);
                    string name = removeWhitespace(txtFullName.Text);
                    if (!emailRegex.IsMatch(removeWhitespace(txtEmail.Text))) throw new Exception("Email phải khớp với mẫu example@FURentalSystem.com");
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
                        case "create":
                            if (checkId(staffAccount.StaffId))
                            {
                                _repo.Add(staffAccount);
                                MessageBox.Show("Thêm thành công", "Thông báo");
                                clearInput();
                                enableInput(false);
                                enableButton(false);
                                btnCreate.IsEnabled = true;
                                loadStaffToDG();
                                lvStaff.IsEnabled = true;
                            }
                            break;
                        case "update":
                            _repo.Update(staffAccount);
                            MessageBox.Show("Update thành công", "Thông báo");
                            clearInput();
                            enableInput(false);
                            enableButton(false);
                            btnCreate.IsEnabled = true;
                            loadStaffToDG();
                            lvStaff.IsEnabled = true;
                            break;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Thông báo");
                }
                
                
            }

        }

        private void clearInput()
        {
            txtStaffID.Clear();
            txtFullName.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string search = txtSearch.Text.Trim();
            if (search.Equals(""))
            {
                lvStaff.ItemsSource = _repo.GetAll();
            }
            else
            {
                lvStaff.ItemsSource = _repo.GetListStaffByName(search);
            }
        }
    }
}
