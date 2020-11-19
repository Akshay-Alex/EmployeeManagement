using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.DomainModels;

namespace EmployeeManagement.Repositories
{
    public interface ILeaveRepository
    {
        void RequestLeave(Leave l);
        void ApproveLeave(Leave l);
        List<Leave> GetLeaveRequests();
        List<Leave> GetLeaveRequestsByEmployeeID(int eid);
    }
    public class LeaveRepository : ILeaveRepository
    {
        empDbcontext db;
        public LeaveRepository()
        {
            db = new empDbcontext();
        }
        public void RequestLeave(Leave l)
        {
            db.LeaveRequests.Add(l);
            db.SaveChanges();
        }

        public void ApproveLeave(Leave l)
        {
            Leave ls = db.LeaveRequests.Where(temp => temp.EmployeeID == l.EmployeeID).FirstOrDefault();
            if(ls != null)
            {
                ls.IsApproved = true;
                db.SaveChanges();
            }
        }
        public List<Leave> GetLeaveRequests()
        {
            List<Leave> lt = db.LeaveRequests.OrderByDescending(temp => temp.StartDate).ToList();
            return lt;
        }
        public List<Leave> GetLeaveRequestsByEmployeeID(int eid)
        {
            List<Leave> lt = db.LeaveRequests.Where(temp => temp.EmployeeID == eid).ToList();
            return lt;
        }
    }
}
