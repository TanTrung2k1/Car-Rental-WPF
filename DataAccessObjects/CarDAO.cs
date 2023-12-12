using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class CarDAO
    {
        private CarRentalSystemDBContext _context;
        public CarDAO()
        {
            _context = new CarRentalSystemDBContext();
        }

        public Car GetCarById(string carID)
        {
            return _context.Cars.FirstOrDefault(c => c.CarId == carID);
        }

        public List<Car> GetListCarByName(string name)
        {
            return _context.Cars.Where(c => c.CarName.Equals(name)).ToList();
        }


        public void Add(Car car)
        {
            _context.Add(car);
            _context.SaveChanges();
        }

        public void Update(Car car)
        {
            var flag = GetCarById(car.CarId);
            if(flag != null)
            {
                flag.CarId = car.CarId;
                flag.CarName = car.CarName;
                flag.CarModelYear = car.CarModelYear;
                flag.Color = car.Color;
                flag.Capacity = car.Capacity;
                flag.Description = car.Description;
                flag.ImportDate = car.ImportDate;
                flag.RentPrice = car.RentPrice;
                flag.Status = car.Status;
                flag.ProducerId = car.ProducerId;

                _context.SaveChanges();
            }
        }

        public void Delete(string carID)
        {
            var flag = GetCarById(carID);
            if (flag != null)
            {
                _context.Cars.Remove(flag);
                _context.SaveChanges();
            }
        }
        public IEnumerable<Car> GetAll()
        {
            return _context.Cars.ToList();
        }


    }
}
