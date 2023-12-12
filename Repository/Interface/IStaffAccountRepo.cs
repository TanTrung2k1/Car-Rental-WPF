using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IStaffAccountRepo
    {
        IEnumerable<StaffAccount> GetAll();
        StaffAccount GetByEmail(string email);
        StaffAccount Get(string StaffID);
        void Add(StaffAccount account);
        void Update(StaffAccount account);
        void Delete(string StaffID);
        List<StaffAccount> GetListStaffByName(string name);
    }
}
