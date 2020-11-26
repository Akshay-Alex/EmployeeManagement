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
        void RejectLeave(Leave l);
        List<Leave> GetLeaveRequests();
        List<Leave> GetLeaveRequestsByEmployeeID(int eid);
    }
    public class LeaveRepository : ILeaveRepository
    {
        EmpDbcontext db;
        public LeaveRepository()
        {
            db = new EmpDbcontext();
        }
        public void RequestLeave(Leave l)
        {
            db.LeaveRequests.Add(l);
            db.SaveChanges();
        }

        public void ApproveLeave(Leave l)
        {
            Leave ls = db.LeaveRequests.Where(temp => temp.LeaveID == l.LeaveID).FirstOrDefault();
            if(ls != null)
            {
                ls.IsApproved = "Approved";
                db.SaveChanges();
            }
        }
        public void RejectLeave(Leave l)
        {
            Leave ls = db.LeaveRequests.Where(temp => temp.LeaveID == l.LeaveID).FirstOrDefault();
            if (ls != null)
            {
                ls.IsApproved = "Rejected";
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
