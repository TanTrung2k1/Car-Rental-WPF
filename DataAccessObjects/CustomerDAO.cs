using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class CustomerDAO
    {
        private CarRentalSystemDBContext _dbContext;
        public CustomerDAO()
        {
            _dbContext = new CarRentalSystemDBContext();
        }

        public Customer getCustomerByEmail(string email)
        {
            return _dbContext.Customers.FirstOrDefault(c => c.CustomerEmail == email);
        }

        public Customer GetCustomerById(string Id)
        {
            return _dbContext.Customers.FirstOrDefault(c => c.CustomerId == Id);
        }

        public void Add(Customer customer)
        {
            _dbContext.Add(customer);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Customer> GetAll()
        {
            return _dbContext.Customers.ToList();
        }

        public void Update(Customer customer)
        {
            var flag = GetCustomerById(customer.CustomerId);
            if(flag != null)
            {
                flag.CustomerId = customer.CustomerId;
                flag.CustomerName = customer.CustomerName;
                flag.Mobile= customer.Mobile;
                flag.CustomerEmail= customer.CustomerEmail;
                flag.CustomerPassword= customer.CustomerPassword;
                flag.IdentityCard= customer.IdentityCard;
                flag.LicenceNumber= customer.LicenceNumber;
                flag.LicenceDate= customer.LicenceDate;

               
                _dbContext.SaveChanges();
            }
        }
    }
}
