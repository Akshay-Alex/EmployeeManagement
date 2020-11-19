using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.DomainModels;

namespace EmployeeManagement.Repositories
{
    public interface IEmployeesRepository
    {
        void InsertEmployee(Employee e);
        void UpdateEmployeeDetails(Employee e);
        List<Employee> GetEmployeesByName(string name);
        List<Employee> GetEmployeesByEmployeeID(int id);
        int GetLatestEmployeeID();
    }
    public class EmployeesRepository : IEmployeesRepository
    {
        EmpDbcontext db;
        public EmployeesRepository()
        {
            db = new EmpDbcontext();
        }
        public void InsertEmployee(Employee e)
        {
            db.Employees.Add(e);
            db.SaveChanges();
        }
        public void UpdateEmployeeDetails(Employee e)
        {
            Employee es = db.Employees.Where(temp => temp.EmployeeID == e.EmployeeID).FirstOrDefault();
            if(es != null)
            {
                es.EmployeeName = e.EmployeeName;
                es.Email = e.Email;
                es.PhoneNumber = e.PhoneNumber;
                db.SaveChanges();
            }
        }
        public List<Employee> GetEmployeesByName(string name)
        {
            List<Employee> e = db.Employees.Where(temp => temp.EmployeeName.Contains(name)).ToList();
            return e;
        }
        public List<Employee> GetEmployeesByEmployeeID(int id)
        {
            List<Employee> e = db.Employees.Where(temp => temp.EmployeeID == id).ToList();
            return e;
        }
        public int GetLatestEmployeeID()
        {
            int eid = db.Employees.Select(temp => temp.EmployeeID).Max();
            return eid;
        }
    }
  
}
