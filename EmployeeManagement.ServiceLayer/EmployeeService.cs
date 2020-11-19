using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.DomainModels;
using EmployeeManagement.Repositories;
using EmployeeManagement.ViewModels;
using AutoMapper;
using AutoMapper.Configuration; 


namespace EmployeeManagement.ServiceLayer
{
    public interface IEmployeeService
    {
        int InsertEmployee(AddEmployeeViewModel aem);
        void UpdateEmployeeDetails(EmployeeViewModel eem);
        List<EmployeeViewModel> GetEmployeesByName(string name);
        EmployeeViewModel GetEmployeesByEmployeeID(int eid);
    }
    public class EmployeeService : IEmployeeService
    {
        IEmployeesRepository er;
        public EmployeeService()
        {
            er = new EmployeesRepository();
        }
        public int InsertEmployee(AddEmployeeViewModel aem)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<AddEmployeeViewModel, Employee>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Employee e = mapper.Map<AddEmployeeViewModel, Employee>(aem);
            er.InsertEmployee(e);
            int uid = er.GetLatestEmployeeID();
            return uid;
        }
        public void UpdateEmployeeDetails(EmployeeViewModel eem)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EmployeeViewModel, Employee>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Employee e = mapper.Map<EmployeeViewModel, Employee>(eem);
            er.UpdateEmployeeDetails(e);
        }
        public List<EmployeeViewModel> GetEmployeesByName(string name)
        {
            List<Employee> e = er.GetEmployeesByName(name);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Employee, EditEmployeeViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<EmployeeViewModel> evm = mapper.Map<List<Employee>,List< EmployeeViewModel >> (e);
            return evm;
        }
        public EmployeeViewModel GetEmployeesByEmployeeID(int eid)
        {
            Employee e = er.GetEmployeesByEmployeeID(eid).FirstOrDefault();
            EmployeeViewModel evm = null;
            if (e != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Employee, EditEmployeeViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                evm = mapper.Map<Employee, EmployeeViewModel>(e);

            }
            return evm;

        }

    }
}
