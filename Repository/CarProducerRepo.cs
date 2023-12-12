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
    public class CarProducerRepo : ICarProducerRepo
    {
        private CarProducerDAO _dao;
        public CarProducerRepo()
        {
            _dao = new CarProducerDAO();
        }
        public IEnumerable<CarProducer> GetAll() => _dao.GetAll();
        
    }
}
