using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagement.Filters
{
    public class ManagerOrSpecialHR : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if(!(Convert.ToString(HttpContext.Current.Session["CurrentUserRole"]) == "Project Manager" || (Convert.ToString(HttpContext.Current.Session["CurrentUserRole"]) == "HR" && Convert.ToString(HttpContext.Current.Session["IsSpecialPermission"]) == "True")))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}