using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface ICarRentalRepo
    {
        IEnumerable<CarRental> GetAll();
        List<CarRental> getListByCustomerId(string customerId);
        List<CarRental> GetListCarRentalByCarId(string cardId);
        void Add(CarRental carRental);
        List<CarRental> Report(DateTime startDate, DateTime endDate);
    }
}
