using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeManagement.ServiceLayer;
using EmployeeManagement.ViewModels;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        IEmployeeService ie = new EmployeeService();
        
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult fu()
        {

            EmployeeViewModel e = ie.GetEmployeesByEmployeeID(1);
            return View(e);
        }
    }
}