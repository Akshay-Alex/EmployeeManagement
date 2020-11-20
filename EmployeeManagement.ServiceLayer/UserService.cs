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
        
        


    }
}
