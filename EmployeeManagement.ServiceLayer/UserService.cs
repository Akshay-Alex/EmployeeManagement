using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.DomainModels;
using EmployeeManagement.ViewModels;
using EmployeeManagement.Repositories;
using AutoMapper;
using AutoMapper.Configuration;

namespace EmployeeManagement.ServiceLayer
{
    public interface IUsersService
    {
        int InsertUser(RegisterViewModel uvm);
        
        UserViewModel GetUsersByEmailAndPassword(string Email, string Password);
        int GetLatestUserID();

        UserViewModel GetUserByUserID(int UserID);
        void UpdateUserDetails(UserViewModel uvm);
        void DeleteUserDetails(UserViewModel uvm);
        string[] GetAllRoles();
        List<UserViewModel> GetUsersByRole(string role);


    }
    public class UsersService : IUsersService
    {
        IUsersRepository ur;
        public UsersService()
        {
            ur = new UsersRepository();
        }


        public int InsertUser(RegisterViewModel uvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<RegisterViewModel, User>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            User u = mapper.Map<RegisterViewModel, User>(uvm);
            u.PasswordHash = SHA256HashGenerator.GenerateHash(uvm.Password);
            ur.InsertUser(u);
            int uid = ur.GetLatestUserID();
            return uid;
        }
        public int GetLatestUserID()
        {
            int uid = ur.GetLatestUserID();
            return uid;
        }
        public void UpdateUserDetails(UserViewModel uvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<UserViewModel, User>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            User u = mapper.Map<UserViewModel, User>(uvm);
            ur.UpdateUserDetails(u);  
        }
        public void DeleteUserDetails(UserViewModel uvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<UserViewModel, User>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            User u = mapper.Map<UserViewModel, User>(uvm);
            ur.DeleteUserDetails(u);
        }


        public UserViewModel GetUsersByEmailAndPassword(string Email, string Password)
        {
            User u = ur.GetUsersByEmailAndPassword(Email, SHA256HashGenerator.GenerateHash(Password)).FirstOrDefault();
            UserViewModel uvm = null;
            if (u != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                uvm = mapper.Map<User, UserViewModel>(u);
            }
            return uvm;
        }
        public UserViewModel GetUserByUserID(int UserID)
        {
            User u = ur.GetUserByUserID(UserID).FirstOrDefault();
            UserViewModel uvm = null;
            if (u != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                uvm = mapper.Map<User, UserViewModel>(u);
            }
            return uvm;
        }
        public string[] GetAllRoles()
        {
            string[] roles = ur.GetAllRoles();
            return roles;
        }
        public List<UserViewModel> GetUsersByRole(string role)
        {
            List<User> e = ur.GetUsersByRole(role);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<UserViewModel> evm = mapper.Map<List<User>, List<UserViewModel>>(e);
            return evm;
        }




    }
}
