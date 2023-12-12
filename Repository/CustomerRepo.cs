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
    public class CustomerRepo : ICustomerRepo
    {
        private CustomerDAO _dao;

        public CustomerRepo()
        {
            _dao = new CustomerDAO();
        }

        public void Add(Customer customer) => _dao.Add(customer);

        public IEnumerable<Customer> GetAll() => _dao.GetAll();
        

        public Customer GetCustomerByEmail(string email) => _dao.getCustomerByEmail(email);



        public Customer GetCustomerById(string id) => _dao.GetCustomerById(id);
        

        public void Update(Customer customer) => _dao.Update(customer);
        
    }
}
