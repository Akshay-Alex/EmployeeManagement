using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.DomainModels;

namespace EmployeeManagement.Repositories
{
    public interface IUsersRepository
    {
        void InsertUser(User u);
        List<User> GetUsersByEmailAndPassword(string Email, string Password);
        int GetLatestUserID();



    }
    public class UsersRepository : IUsersRepository
    {
        EmpDbcontext db;
        public UsersRepository()
        {
            db = new EmpDbcontext();
        }
        public void InsertUser(User u)
        {
            db.Users.Add(u);
            db.SaveChanges();
        }
        
        public List<User> GetUsersByEmailAndPassword(string Email, string PasswordHash)
        {
            List<User> us = db.Users.Where(temp => string.Compare(temp.Email, Email) == 0 && string.Compare(temp.PasswordHash, PasswordHash) == 0).ToList();
            return us;

        }
        public int GetLatestUserID()
        {
            int uid = db.Users.Select(temp => temp.UserID).Max();
            return uid;

        }


    }
}
