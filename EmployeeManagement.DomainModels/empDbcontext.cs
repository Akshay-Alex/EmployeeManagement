using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace EmployeeManagement.DomainModels
{
    public class empDbcontext :DbContext
    {
        public empDbcontext() :base("EmployeeDatabaseDbContext")
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Leave> LeaveRequests { get; set; }
    }
}
