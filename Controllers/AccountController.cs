using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeManagement.ViewModels;
using EmployeeManagement.ServiceLayer;
using EmployeeManagement.CustomFilters;
using System.Web.Security;

namespace EmployeeManagement.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        IUsersService us;
        public AccountController(IUsersService us)
        {
            this.us = us;
        }

        public ActionResult Register()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Register(RegisterViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                FormsAuthentication.SetAuthCookie(rvm.Name, false);
                int uid = this.us.InsertUser(rvm);
                Session["CurrentUserID"] = uid;
                Session["CurrentUserName"] = rvm.Name;
                Session["CurrentUserEmail"] = rvm.Email;
                Session["CurrentUserPassword"] = rvm.Password;
                Session["CurrentRole"] = rvm.Role;
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View();
            }

        }
        public ActionResult Login()
        {
            LoginViewModel lvm = new LoginViewModel();
            return View(lvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                UserViewModel uvm = this.us.GetUsersByEmailAndPassword(lvm.Email, lvm.Password);
                if (uvm != null)
                {
                    FormsAuthentication.SetAuthCookie(uvm.Name, false);
                    Session["CurrentUserID"] = uvm.UserID;
                    Session["CurrentUserName"] = uvm.Name;
                    Session["CurrentUserEmail"] = uvm.Email;
                    Session["CurrentUserPassword"] = uvm.Password;
                    Session["CurrentUserRole"] = uvm.Role;
                }
                /*if (uvm.Role)
                {
                    return RedirectToRoute(new { area = "admin", controller = "AdminHome", action = "Index" });

                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

                   */
                else
                {
                    ModelState.AddModelError("x", "Invalid Email / Password");
                }
            }
            return RedirectToAction("Index", "Home");

        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}