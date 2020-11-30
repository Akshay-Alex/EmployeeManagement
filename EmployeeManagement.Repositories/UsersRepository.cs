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
        List<User> GetUserByUserID(int UserID);
        void UpdateUserDetails(User u);
        void DeleteUserDetails(User u);
        string[] GetAllRoles();
        string[] GetAllNames();
        List<User> GetUsersByRole(string role);
        List<User> GetUsersByName(string name);



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
        public List<User> GetUserByUserID(int UserID)
        {
            List<User> u = db.Users.Where(temp => temp.UserID == UserID).ToList();
            return u;
        }
        public void UpdateUserDetails(User u)
        {
            User es = db.Users.Where(temp => temp.UserID == u.UserID).FirstOrDefault();
            if (es != null)
            {
                es.Name = u.Name;
                es.Email = u.Email;
                es.Mobile = u.Mobile;
                if(u.ImageUrl != null)
                {
                    es.ImageUrl = u.ImageUrl;
                }
                db.SaveChanges();
            }
        }
        public void DeleteUserDetails(User u)
        {
            User es = db.Users.Where(temp => temp.UserID == u.UserID).FirstOrDefault();
            if (es != null)
            {
                db.Users.Remove(es);
                db.SaveChanges();
            }
        }
        public string[] GetAllRoles()
        {
            string[] roles = db.Employees.Select(temp => temp.Role).Distinct().ToArray();
            return roles;
        }
        public string[] GetAllNames()
        {
            string[] names = db.Users.Select(temp => temp.Name).Distinct().ToArray();
            return names;
        }
        public List<User> GetUsersByRole(string role)
        {
            List<User> e = db.Users.Where(temp => temp.Role.Contains(role)).ToList();
            return e;
        }
        public List<User> GetUsersByName(string name)
        {
            List<User> e = db.Users.Where(temp => temp.Name.Contains(name)).ToList();
            return e;
        }
    }
}
