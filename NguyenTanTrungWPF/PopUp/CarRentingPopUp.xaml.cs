using BusinessObjects.Models;
using Repository;
using Repository.Interface;
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

namespace NguyenTanTrungWPF.PopUp
{
    /// <summary>
    /// Interaction logic for CarRentingPopUp.xaml
    /// </summary>
    public partial class CarRentingPopUp : Window
    {
        private ICarRentalRepo _carRentalRepo;
        private ICustomerRepo _customerRepo;
        private Car currentCar;
        public CarRentingPopUp(Car car)
        {
            InitializeComponent();
            _carRentalRepo = new CarRentalRepo();
            _customerRepo = new CustomerRepo();
            currentCar = car;

            loadInfor();
            loadCustomer();
        }

        private void loadInfor()
        {
            txtCarName.Text = currentCar.CarName;
            txtPrice.Text = currentCar.RentPrice.ToString();
        }

        private bool checkInput()
        {
            bool result = false;
            try
            {
                if (DateTime.Compare(DateTime.Parse(txtReturnDate.Text), DateTime.Parse(txtPUDate.Text)) < 0) throw new Exception("Please input again Pick up date and Return date");

                result = true;
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
            return result;
        }

        private void loadCustomer()
        {
            cbCustomer.ItemsSource = _customerRepo.GetAll();
            cbCustomer.DisplayMemberPath = "CustomerName";
            cbCustomer.SelectedValuePath = "CustomerId";
            cbCustomer.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (checkInput())
            {
                try
                {
                    List<CarRental> listCarRental = _carRentalRepo.GetListCarRentalByCarId(currentCar.CarId);
                    foreach (CarRental carRental in listCarRental)
                    {
                        if (carRental.ReturnDate > DateTime.Parse(txtPUDate.Text)) throw new Exception("Thời gian chọn trên đã có người thuê");
                        
                    }

                    DateTime d1 = DateTime.Parse(txtPUDate.Text);
                    DateTime d2 = DateTime.Parse(txtReturnDate.Text);
                    TimeSpan time = d2 - d1;
                    int days = time.Days;

                    CarRental carRent = new CarRental();
                    carRent.CarId = currentCar.CarId;
                    carRent.CustomerId = cbCustomer.SelectedValue.ToString();
                    carRent.RentPrice = currentCar.RentPrice * days;
                    carRent.PickupDate = DateTime.Parse(txtPUDate.Text);
                    carRent.ReturnDate = DateTime.Parse(txtReturnDate.Text);

                    _carRentalRepo.Add(carRent);

                    MessageBox.Show("Rental successful!!\n Total Price: " + carRent.RentPrice, "Thông báo");

                    this.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Thông báo");
                }
                
            }
        }
    }
}
