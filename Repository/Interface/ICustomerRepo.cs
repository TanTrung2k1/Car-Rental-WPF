using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface ICustomerRepo
    {
        Customer GetCustomerById(string id);
        Customer GetCustomerByEmail(string email);
        void Update(Customer customer);
        void Add(Customer customer);

        IEnumerable<Customer> GetAll();
    }
}
