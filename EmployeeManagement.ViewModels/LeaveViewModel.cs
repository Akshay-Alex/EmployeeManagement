using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class LeaveViewModel
    {
        public int LeaveID;
        public int EmployeeID;
        public DateTime StartDate;
        public DateTime EndDate;
        public bool IsApproved;
    }
}
