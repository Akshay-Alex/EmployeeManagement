using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeManagement.ViewModels;
using EmployeeManagement.ServiceLayer;

namespace EmployeeManagement.Controllers
{
    [Authorize(Roles = "Employee")]
    public class LeaveRequestController : Controller
    {
        // GET: LeaveRequest
        ILeaveService il;
        
        public LeaveRequestController(ILeaveService il)
        {
            this.il = il;
        }
        public ActionResult NewRequest()
        {
            LeaveViewModel lvm = new LeaveViewModel();
            return View(lvm);
        }
        [HttpPost]
        public ActionResult NewRequest(LeaveViewModel lvm)
        {
            il.RequestLeave(lvm);
            return View(lvm);
        }
        public ActionResult LeaveStatus()
        {
            List<LeaveViewModel> lvm = new List<LeaveViewModel>();
            int userid = Convert.ToInt32(Session["CurrentUserID"]);
            lvm = il.GetLeaveRequestsByEmployeeID(userid);
            return View(lvm);
        }
    }
}