using BusinessObjects.Models;
using NguyenTanTrungWPF.PopUp;
using Repository;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
    /// Interaction logic for ManageCarWindow.xaml
    /// </summary>
    public partial class ManageCarWindow : Window
    {
        private ICarRepo _carRepo;
        private ICarProducerRepo _carProducerRepo;
        private string function = null;
        public ManageCarWindow()
        {
            InitializeComponent();
            _carRepo = new CarRepo();
            _carProducerRepo = new CarProducerRepo();
            loadCar();
            loadProducer();
            enableButton(false);
            enableInput(false);
            btnCreate.IsEnabled = true;
        }
        private void loadProducer()
        {
            cbProducerID.ItemsSource = _carProducerRepo.GetAll();
            cbProducerID.DisplayMemberPath = "ProcuderName";
            cbProducerID.SelectedValuePath = "ProducerId";
            cbProducerID.SelectedIndex = 0;
        }
        private void loadCar()
        {
            var listCar = _carRepo.GetAll();
            lvCar.ItemsSource = listCar;
        }

        private void enableButton(bool flag)
        {
            btnCreate.IsEnabled = flag;
            btnUpdate.IsEnabled = flag;
            btnDelete.IsEnabled = flag;
            btnSave.IsEnabled = flag;
            btnCreateRenting.IsEnabled = flag;
        }

        private void clearInput()
        {
            txtID.Clear();
            txtName.Clear();
            txtCMYear.Clear();
            txtColor.Clear();
            txtCapacity.Clear();
            txtDesc.Clear();
            txtPrice.Clear();
            txtStatus.Clear();
            
        }
        private void enableInput(bool flag)
        {
            txtID.IsEnabled = flag;
            txtName.IsEnabled = flag;
            txtCMYear.IsEnabled = flag;
            txtColor.IsEnabled = flag;
            txtCapacity.IsEnabled = flag;
            txtDesc.IsEnabled = flag;
            txtImportDate.IsEnabled = flag;
            txtPrice.IsEnabled = flag;
            txtStatus.IsEnabled = flag;
            cbProducerID.IsEnabled = flag;
        }

        private string removeWhitespace(string stringInput)
        {
            Regex trimmer = new Regex(@"\s\s+");
            stringInput = trimmer.Replace(stringInput.Trim(), " ");
            return stringInput;
        }
        private bool checkIdFormat(string staffID)
        {

            string format = "^C[A|d]\\d{4}$";
            Regex rgx = new Regex(format);
            staffID = removeWhitespace(staffID);
            bool result = rgx.IsMatch(staffID);
            return result;
        }

        private bool checkInput()
        {
            bool result = false;
            try
            {
                if (txtID.Text.Trim().Equals("")) throw new Exception("Please input Car ID");
                if (checkIdFormat(txtID.Text) == false) throw new Exception("Please input Car ID correct format [CAXXXX] with X is the number ");
                if (txtName.Text.Trim().Equals("")) throw new Exception("Please input Full name");
                if (txtCMYear.Text.Trim().Equals("")) throw new Exception("Please input Car Model Year");
                
                if (txtColor.Text.Trim().Equals("")) throw new Exception("Please input Color");
                if (txtCapacity.Text.Trim().Equals("")) throw new Exception("Please input Capacity");
                if (txtDesc.Text.Trim().Equals("")) throw new Exception("Please input Description");
                if (txtImportDate.Text.Trim().Equals("")) throw new Exception("Please input Import date");
                if (txtPrice.Text.Trim().Equals("")) throw new Exception("Please input Price");
                if (txtStatus.Text.Trim().Equals("")) throw new Exception("Please input Status");
                
                result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
            return result;
        }

        private void lvCar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lvCar.SelectedIndex == -1)
            {
                return;
            }
            Car car = lvCar.SelectedItem as Car;

            txtID.Text = car.CarId;
            txtName.Text = car.CarName;
            txtCMYear.Text = car.CarModelYear.ToString();
            txtColor.Text = car.Color;
            txtCapacity.Text = car.Capacity.ToString();
            txtDesc.Text = car.Description;
            txtImportDate.Text = car.ImportDate.ToString();
            txtPrice.Text = car.RentPrice.ToString();
            txtStatus.Text = car.Status.ToString();
           

            
            btnUpdate.IsEnabled = true;
            btnDelete.IsEnabled = true;
            btnCreateRenting.IsEnabled = true;
            
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            lvCar.IsEnabled = false;
            enableInput(true);
            clearInput();
            enableButton(false);
            btnSave.IsEnabled = true;
            function = "create";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (checkInput())
            {
                try
                {
                    Car getCar = new Car();
                    getCar.CarId = removeWhitespace(txtID.Text);
                    getCar.CarName = removeWhitespace(txtName.Text);
                    try
                    {

                        getCar.CarModelYear = int.Parse(removeWhitespace(txtCMYear.Text));
                        getCar.Capacity = int.Parse(removeWhitespace(txtCapacity.Text));
                        getCar.RentPrice = int.Parse(removeWhitespace(txtPrice.Text));
                        getCar.Status = int.Parse(removeWhitespace(txtStatus.Text));
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Car model year, Capacity, Rent price and Status is integer number");
                    }
                    getCar.Color = removeWhitespace(txtColor.Text);
                    getCar.Description = removeWhitespace(txtDesc.Text);
                    getCar.ImportDate = DateTime.Parse(txtImportDate.Text);
                    getCar.ProducerId = cbProducerID.SelectedValue.ToString();

                    switch (function)
                    {
                        case "create":
                            var flag = _carRepo.GetCarById(getCar.CarId);
                            if (flag == null)
                            {
                                _carRepo.Add(getCar);
                                MessageBox.Show("Thêm thành công", "Thông báo");
                                loadCar();
                                clearInput();
                                enableInput(false);
                                enableButton(false);
                                btnCreate.IsEnabled = true;
                                lvCar.IsEnabled = true;
                            }
                            else
                            {
                                MessageBox.Show("Car ID đã tồn tại vui lòng nhập ID khác", "Thông báo");
                            }
                            break;
                        case "update":
                            _carRepo.Update(getCar);
                            MessageBox.Show("Update thành công", "Thông báo");
                            loadCar();
                            clearInput();
                            enableInput(false);
                            enableButton(false);
                            btnCreate.IsEnabled = true;
                            lvCar.IsEnabled = true;

                            break;
                    }

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Thông báo");
                }
                
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            lvCar.IsEnabled = false;
            enableInput(true);
            txtID.IsEnabled = false;
            enableButton(false);
            btnSave.IsEnabled = true;
            function = "update";
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var response = MessageBox.Show("Do you want delete car?", "Thông báo"
                            , MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (response == MessageBoxResult.Yes)
            {
                _carRepo.Delete(txtID.Text);
                loadCar();
                clearInput();
                enableButton(false);
                btnCreate.IsEnabled = true;
            }
            else if (response == MessageBoxResult.No)
            {
                e.Handled = true;
            }
        }

        private void btnCreateRenting_Click(object sender, RoutedEventArgs e)
        {
            Car carRental = new Car();
            carRental.CarId = txtID.Text;
            carRental.CarName = txtName.Text;
            carRental.CarModelYear = int.Parse(txtCMYear.Text);
            carRental.Color = txtColor.Text;
            carRental.Capacity = int.Parse(txtCapacity.Text);
            carRental.Description = txtDesc.Text;
            carRental.ImportDate = DateTime.Parse(txtImportDate.Text);
            carRental.RentPrice= Decimal.Parse(txtPrice.Text);
            carRental.Status = int.Parse(txtStatus.Text);

            
            Window popup = new CarRentingPopUp(carRental);
            popup.ShowDialog();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string search = txtSearch.Text.Trim();
            if (search.Equals(""))
            {
                lvCar.ItemsSource = _carRepo.GetAll();
            }
            else
            {
                lvCar.ItemsSource = _carRepo.GetListCarByName(search);
            }
        }
    }
}
