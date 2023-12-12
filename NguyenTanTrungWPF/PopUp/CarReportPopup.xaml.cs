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
    /// Interaction logic for CarReportPopup.xaml
    /// </summary>
    public partial class CarReportPopup : Window
    {
        private ICarRentalRepo _carRentalRepo;
        public CarReportPopup()
        {
            InitializeComponent();
            _carRentalRepo = new CarRentalRepo();
            txtTotalPrice.IsEnabled = false;
            txtTotalRent.IsEnabled = false;

            loadAllCarRental();
        }

        private void loadAllCarRental()
        {
            lvCarRental.ItemsSource = _carRentalRepo.GetAll();
        }

        private void loadTotalRent(List<CarRental> list)
        {
            int count = 0;
            decimal price = 0;
            foreach (CarRental carRental in list)
            {
                price += (decimal)carRental.RentPrice;
                count++;

            }

            
            txtTotalRent.Text = count.ToString();
            txtTotalPrice.Text = price.ToString();
            
        }

        private bool checkInput()
        {
            bool result = false;
            try
            {
                if (txtStartDate.Text.Trim().Equals("")) throw new Exception("Please input start date");
                if (txtEndDate.Text.Trim().Equals("")) throw new Exception("Please input end date");
                if (DateTime.Compare(DateTime.Parse(txtEndDate.Text), DateTime.Parse(txtStartDate.Text)) < 0) throw new Exception("Please input again Start date and End date");
                result = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }

            return result;
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            if (checkInput())
            {
                
                DateTime startDate= DateTime.Parse(txtStartDate.Text);
                DateTime endDate = DateTime.Parse(txtEndDate.Text);
                List<CarRental> list = _carRentalRepo.Report(startDate, endDate);
                loadTotalRent(list);
                lvCarRental.ItemsSource = _carRentalRepo.Report(startDate, endDate);
            }
        }
    }
}
