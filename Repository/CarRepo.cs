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
    public class CarRepo : ICarRepo
    {
        private CarDAO _dao;
        public CarRepo()
        {
            _dao = new CarDAO();
        }
        public void Add(Car car) => _dao.Add(car);
        

        public void Delete(string carID) => _dao.Delete(carID);
        

        public IEnumerable<Car> GetAll() => _dao.GetAll();


        public Car GetCarById(string carID) => _dao.GetCarById(carID);

        public List<Car> GetListCarByName(string name) => _dao.GetListCarByName(name);
        

        public void Update(Car car) => _dao.Update(car);
        
    }
}
