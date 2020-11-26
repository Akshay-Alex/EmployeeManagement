using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeManagement.ServiceLayer;
using EmployeeManagement.ViewModels;

namespace EmployeeManagement.Controllers
{
    [Authorize(Roles = "Project Manager")]
    public class LeaveController : Controller
    {
        // GET: Leave
        ILeaveService il;
        public LeaveController(ILeaveService il)
        {
            this.il = il;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetLeaveRequests()
        {
            List<LeaveViewModel> lvm = new List<LeaveViewModel>();
            lvm = il.GetLeaveRequests();
            return View(lvm);
        }
        [HttpPost]
        public ActionResult ApproveorRejectLeave(LeaveViewModel lvm,string submit)
        {
            if (submit == "Approve")
            {
                il.ApproveLeave(lvm);
                return RedirectToAction("GetLeaveRequests");
            }
            else
            {
                il.RejectLeave(lvm);
                return RedirectToAction("GetLeaveRequests");
            }
        }
            
        }
    }
