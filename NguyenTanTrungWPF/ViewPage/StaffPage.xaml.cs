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

namespace NguyenTanTrungWPF.ViewPage
{
    /// <summary>
    /// Interaction logic for StaffPage.xaml
    /// </summary>
    public partial class StaffPage : Page
    {
        private StaffAccount staff;
        private IStaffAccountRepo _repo;
        public StaffPage(StaffAccount account)
        {
            InitializeComponent();
            staff = account;
            _repo = new StaffAccountRepo();

            loadStaff();
            enableInput(false);
            btnSave.IsEnabled = false;
        }

        private void loadStaff()
        {
            StaffAccount loadAccount = _repo.Get(staff.StaffId);
            txtStaffID.Text = loadAccount.StaffId;
            txtFullName.Text = loadAccount.FullName;
            txtEmail.Text = loadAccount.Email;
            txtPassword.Password = loadAccount.Password;
        }

        private void enableInput(bool flag)
        {
            txtStaffID.IsEnabled = flag;
            txtFullName.IsEnabled = flag;
            txtEmail.IsEnabled = flag;
            txtPassword.IsEnabled = flag;
        }

        private void btnCarManage_Click(object sender, RoutedEventArgs e)
        {
            Window carWindow = new ManageCarWindow();
            carWindow.ShowDialog();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            btnSave.IsEnabled = true;
            btnUpdate.IsEnabled = false;
            enableInput(true);
            txtStaffID.IsReadOnly = true;
            txtEmail.IsReadOnly = true;
        }

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
                if (txtFullName.Text.Trim().Equals("")) throw new Exception("Please input name");                               
                if (txtPassword.Password.Trim().Equals("")) throw new Exception("Please input Password");
                result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
            return result;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (checkInput())
            {
                Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                try
                {
                    string id = txtStaffID.Text;
                    string name = removeWhitespace(txtFullName.Text);
                    if (!emailRegex.IsMatch(removeWhitespace(txtEmail.Text))) throw new Exception("Email phải khớp với mẫu example@FURentalSystem.com");
                    string email = txtEmail.Text;
                    string password = removeWhitespace(txtPassword.Password);

                    StaffAccount updateStaff = new StaffAccount();
                    updateStaff.StaffId = id;
                    updateStaff.FullName = name;
                    updateStaff.Email = email;
                    updateStaff.Password = password;
                    updateStaff.Role = staff.Role;

                    _repo.Update(updateStaff);
                    MessageBox.Show("Save successfull", "Thông báo");
                    loadStaff();



                    enableInput(false);
                    btnUpdate.IsEnabled = true;
                    btnSave.IsEnabled = false;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Thông báo");
                }
                

            }
        }

        private void btnCreateCustomer_Click(object sender, RoutedEventArgs e)
        {
            Window popup = new CustomerPopUp();
            popup.ShowDialog();
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            Window popup = new CarReportPopup();
            popup.ShowDialog();
        }
    }
}
