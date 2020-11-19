using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeManagement.ServiceLayer;
using EmployeeManagement.ViewModels;
using System.Web.SessionState;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        IEmployeeService ie = new EmployeeService();
        EmployeeViewModel e;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddEmployee()
        {
            AddEmployeeViewModel evm = new AddEmployeeViewModel();
            return View(evm);
        }
        [HttpPost]
        public ActionResult AddEmployee(AddEmployeeViewModel evm)
        {
            ie.InsertEmployee(evm);
            return View();

        }
        public ActionResult FindEmployee()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FindEmployee(int EmpID)
        {
            e = ie.GetEmployeesByEmployeeID(EmpID);
            if (e != null)
            {
                Session["EmpID"] = EmpID;
                return RedirectToAction("UpdateEmployee");
            }
            ModelState.AddModelError("EmpID", "There is no Employee with the specified Employee ID");
            return View();
        }
        public ActionResult UpdateEmployee()
        {
            EmployeeViewModel e = ie.GetEmployeesByEmployeeID(Convert.ToInt32(Session["EmpID"]));
            return View(e);
        }
        [HttpPost]
        public ActionResult UpdateEmployee(EmployeeViewModel ev)
        {
            ev.EmployeeID = Convert.ToInt32(Session["EmpID"]);
            ie.UpdateEmployeeDetails(ev);
            return RedirectToAction("Index","Home");
        }
    }

}
