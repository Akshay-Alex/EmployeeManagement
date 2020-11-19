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
   public interface ILeaveService
    {
        void RequestLeave(LeaveViewModel l);
        void ApproveLeave(LeaveViewModel l);
        List<LeaveViewModel> GetLeaveRequests();
        List<LeaveViewModel> GetLeaveRequestsByEmployeeID(int eid);
    }

    public class LeaveService : ILeaveService
    {
        ILeaveRepository ir;
        public LeaveService()
        {
            ir = new LeaveRepository();
        }
        public void RequestLeave(LeaveViewModel l)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<LeaveViewModel, Leave>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Leave lv = mapper.Map<LeaveViewModel, Leave>(l);
            ir.RequestLeave(lv);
        }
        public void ApproveLeave(LeaveViewModel l)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<LeaveViewModel, Leave>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Leave lv = mapper.Map<LeaveViewModel, Leave>(l);
            ir.ApproveLeave(lv);
        }
        public List<LeaveViewModel> GetLeaveRequests()
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<List<LeaveViewModel>, List<Leave>>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<Leave> lv = ir.GetLeaveRequests();
            List<LeaveViewModel> l = mapper.Map<List<Leave>, List<LeaveViewModel>>(lv);
            return l;
        }
        public List<LeaveViewModel> GetLeaveRequestsByEmployeeID(int eid)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<List<LeaveViewModel>, List<Leave>>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<Leave> lv = ir.GetLeaveRequestsByEmployeeID(eid);
            List<LeaveViewModel> l = mapper.Map<List<Leave>, List<LeaveViewModel>>(lv);
            return l;
        }

    }
}
