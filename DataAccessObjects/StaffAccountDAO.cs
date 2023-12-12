using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class StaffAccountDAO
    {
        private CarRentalSystemDBContext _dbContext;

        public StaffAccountDAO()
        {
            _dbContext = new CarRentalSystemDBContext();
        }

        public StaffAccount getStaffAccountById(string StaffID)
        {
            return _dbContext.StaffAccounts.FirstOrDefault(s => s.StaffId == StaffID);
        }

        public StaffAccount getStaffAccountByEmail(string email)
        {
            return _dbContext.StaffAccounts.FirstOrDefault(s => s.Email == email);
        }

        public List<StaffAccount> GetListStaffByName(string name)
        {
            return _dbContext.StaffAccounts.Where(s => s.FullName.Equals(name)).ToList();
        }

        public void Add(StaffAccount account)
        {
            _dbContext.Add(account);
            _dbContext.SaveChanges();
        }
        public void Update(StaffAccount account)
        {
            string Id = account.StaffId;
            var getStaff = getStaffAccountById(Id);
            if (getStaff != null)
            {
                getStaff.StaffId = account.StaffId;
                getStaff.FullName = account.FullName;
                getStaff.Email = account.Email;
                getStaff.Password = account.Password;
                getStaff.Role = account.Role;
                
                _dbContext.SaveChanges();
            }
        }
        public void Delete(string StaffID)
        {
            var getStaff = getStaffAccountById(StaffID);
            if (getStaff != null)
            {
                _dbContext.StaffAccounts.Remove(getStaff);
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<StaffAccount> GetAll()
        {
            return _dbContext.StaffAccounts.ToList();
        }
    }
}
