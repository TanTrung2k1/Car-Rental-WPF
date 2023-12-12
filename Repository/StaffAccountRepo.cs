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
    public class StaffAccountRepo : IStaffAccountRepo
    {
        private StaffAccountDAO _dao;

        public StaffAccountRepo()
        {
            _dao = new StaffAccountDAO();
        }
        public void Add(StaffAccount account) => _dao.Add(account);
        public void Delete(string StaffID) => _dao.Delete(StaffID);
        public StaffAccount Get(string StaffID) => _dao.getStaffAccountById(StaffID);
        public IEnumerable<StaffAccount> GetAll() => _dao.GetAll();

        public StaffAccount GetByEmail(string email) => _dao.getStaffAccountByEmail(email);

        public List<StaffAccount> GetListStaffByName(string name) => _dao.GetListStaffByName(name);
        

        public void Update(StaffAccount account) => _dao.Update(account);
        
    }
}
