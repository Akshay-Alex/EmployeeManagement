using System.Web.Http;
using Unity;
using Unity.WebApi;
using Unity.Mvc5;
using EmployeeManagement.ServiceLayer;
using System.Web.Mvc;
namespace EmployeeManagement
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<IEmployeeService, EmployeeService>();
            container.RegisterType<IUsersService, UsersService>();
            //container.RegisterType<IEmployeeService, EmployeeService>();
            container.RegisterType<ILeaveService, LeaveService>();
            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            
        }
    }
}