using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    
    public class CarRentalDAO
    {
        private CarRentalSystemDBContext _dbContext;

        public CarRentalDAO()
        {
            _dbContext = new CarRentalSystemDBContext();
        }

        public IEnumerable<CarRental> GetAll()
        {
            return _dbContext.CarRentals.OrderByDescending(c => c.RentPrice).ToList();
        }

        public List<CarRental> GetListCarRentalByCarId(string cardId)
        {
            return _dbContext.CarRentals.Where(c => c.CarId == cardId).ToList();
        }

        public List<CarRental> getListByCustomerId(string customerId)
        {
            return _dbContext.CarRentals.Where(c => c.CustomerId == customerId).ToList();
        }

        public void Add(CarRental carRental)
        {
            _dbContext.Add(carRental);
            _dbContext.SaveChanges();
        }

        public List<CarRental> Report(DateTime startDate, DateTime endDate)
        {
            return _dbContext.CarRentals.Where(c => c.PickupDate >= startDate && c.ReturnDate <= endDate).OrderByDescending(c => c.RentPrice).ToList();
        }
    }
}
