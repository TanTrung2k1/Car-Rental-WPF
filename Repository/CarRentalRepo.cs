using BusinessObjects.Models;
using DataAccessObjects;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CarRentalRepo : ICarRentalRepo
    {
        private CarRentalDAO _dao;
        public CarRentalRepo()
        {
            _dao = new CarRentalDAO();
        }

        public void Add(CarRental carRental) => _dao.Add(carRental);
        

        public IEnumerable<CarRental> GetAll() => _dao.GetAll();
        

        public List<CarRental> getListByCustomerId(string customerId) => _dao.getListByCustomerId(customerId);

        public List<CarRental> GetListCarRentalByCarId(string cardId) => _dao.GetListCarRentalByCarId(cardId);

        public List<CarRental> Report(DateTime startDate, DateTime endDate) => _dao.Report(startDate, endDate);
        
    }
}
