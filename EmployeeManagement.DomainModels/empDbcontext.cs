using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
//using EmployeeManagement.Migrations;


namespace EmployeeManagement.DomainModels
{
    public class EmpDbcontext :DbContext
    {
        public EmpDbcontext() :base("EmployeeDatabaseDbContext")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<EmpDbcontext>, Configuration);
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Leave> LeaveRequests { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
