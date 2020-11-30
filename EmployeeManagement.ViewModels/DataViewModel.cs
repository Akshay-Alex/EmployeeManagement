using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class DataViewModel
    {
        public List<UserViewModel> Userlist {get;set;}
        public string [] namelist { get; set; }
        public DataViewModel()
        {
            this.Userlist = new List<UserViewModel>();
            this.namelist = new string[100];
        }
    }
    
}
