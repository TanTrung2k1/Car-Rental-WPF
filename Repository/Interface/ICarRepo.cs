using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface ICarRepo
    {
        Car GetCarById(string carID);
        void Add(Car car);
        void Update(Car car);
        void Delete(string carID);
        IEnumerable<Car> GetAll();

        List<Car> GetListCarByName(string name);
    }
}
