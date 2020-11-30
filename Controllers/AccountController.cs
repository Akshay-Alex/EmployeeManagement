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
            RegisterViewModel rvm = new RegisterViewModel();
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Register(RegisterViewModel rvm)
        {
            if (Request.Files.Count >= 1)
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
                rvm.ImageUrl = base64String;
            }
            if (ModelState.IsValid)
            {
                FormsAuthentication.SetAuthCookie(rvm.Name, false);
                int uid = this.us.InsertUser(rvm);
                Session["CurrentUserID"] = us.GetLatestUserID();
                Session["CurrentUserName"] = rvm.Name;
                Session["CurrentUserEmail"] = rvm.Email;
                Session["CurrentUserPassword"] = rvm.Password;
                Session["CurrentUserRole"] = rvm.Role;
                Session["CurrentImageUrl"] = rvm.ImageUrl;
                Session["IsSpecialPermission"] = rvm.IsSpecialPermission;
                Session["CurrentUserMobile"] = rvm.Mobile;
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
                    Session["CurrentImageUrl"] = uvm.ImageUrl;
                    Session["IsSpecialPermission"] = uvm.IsSpecialPermission;
                    Session["CurrentUserMobile"] = uvm.Mobile;
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
                    return View(lvm);
                }
            }
            return RedirectToAction("Index", "Home");

        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        [Authorize(Roles = "HR")]
        public ActionResult FindEmployee()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "HR")]
        public ActionResult FindEmployee(int UserID)
        {
            UserViewModel u = us.GetUserByUserID(UserID);
            if (u != null)
            {
                Session["UserID"] = UserID;
                return RedirectToAction("UpdateEmployee");
            }
            ModelState.AddModelError("EmpID", "There is no Employee with the specified Employee ID");
            return View();
        }
        [Authorize(Roles = "Employee")]
        public ActionResult UpdateEmployeeProfile()
        {

            UserViewModel u = us.GetUserByUserID(Convert.ToInt32(Session["CurrentUserID"]));
            return View(u);
        }
        [Authorize(Roles = "Employee")]
        [HttpPost]
        public ActionResult UpdateEmployeeProfile(UserViewModel ev)
        {
            if (Request.Files.Count >= 1)
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
                ev.ImageUrl = base64String;
            }
            Session["CurrentUserName"] = ev.Name;
            Session["CurrentImageUrl"] = ev.ImageUrl;
            Session["CurrentUserMobile"] = ev.Mobile;

            ev.UserID = Convert.ToInt32(Session["CurrentUserID"]);
                us.UpdateUserDetails(ev);
                return RedirectToAction("Index", "Home");
            
        }

            [Authorize(Roles = "HR")]
        public ActionResult UpdateEmployee()
        {

            UserViewModel u = us.GetUserByUserID(Convert.ToInt32(Session["UserID"]));
            return View(u);
        }
        
        [HttpPost]
        [Authorize(Roles = "HR")]
        public ActionResult UpdateEmployee(UserViewModel ev, string submit)
        {
            if (submit == "Update")
            {
                ev.UserID = Convert.ToInt32(Session["UserID"]);
                us.UpdateUserDetails(ev);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ev.UserID = Convert.ToInt32(Session["UserID"]);
                us.DeleteUserDetails(ev);
                if (Convert.ToInt32(Session["CurrentUserID"]) == Convert.ToInt32(Session["UserID"]))
                {
                    return RedirectToAction("Logout", "Account");
                }
                return RedirectToAction("Index", "Home");
            }
        }
        [Authorize(Roles = "HR")]
        public ActionResult SearchEmployee()
        {
            List<UserViewModel> evm = new List<UserViewModel>();
            return View(evm);
        }
        [HttpPost]
        [Authorize(Roles = "HR")]
        public ActionResult SearchEmployee(UserViewModel evm)
        {
            List<UserViewModel> e = us.GetUsersByRole(evm.Role);
            return View(e);
        }
        [Authorize(Roles = "HR")]
        public ActionResult SearchEmployeeByName()
        {
            DataViewModel dvm = new DataViewModel();
            dvm.namelist = us.GetAllNames();
            dvm.Userlist = new List<UserViewModel>();
            return View(dvm);
        }
        [HttpPost]
        [Authorize(Roles = "HR")]
        public ActionResult SearchEmployeeByName(DataViewModel dvm, string name)
        {
            
            dvm.Userlist = us.GetUsersByName(name);
            dvm.namelist = us.GetAllNames();
            return View(dvm);
        }
    }
}