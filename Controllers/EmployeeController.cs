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
    
    [Authorize(Roles = "HR")]
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
            if(Request.Files.Count >= 1)
            {
                var file = Request.Files[0];
                var imgBytes = new Byte[0];

                try

                {

                    imgBytes = new Byte[file.ContentLength];

                    file.InputStream.Read(imgBytes, 0, file.ContentLength);

                }

                catch (Exception)

                {

                    imgBytes = new Byte[file.ContentLength - 1];

                    file.InputStream.Read(imgBytes, 0, file.ContentLength);

                }
                var base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                evm.ImageUrl = base64String;
            }
            ie.InsertEmployee(evm);
            Session["CurrentImageUrl"] = evm.ImageUrl;
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
