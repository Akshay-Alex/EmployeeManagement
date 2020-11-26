using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagement.Controllers
{
    public class LeaveRequestController : Controller
    {
        // GET: LeaveRequest
        public ActionResult NewRequest()
        {
            return View();
        }
    }
}