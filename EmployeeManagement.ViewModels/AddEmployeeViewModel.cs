using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.ViewModels
{
    public class AddEmployeeViewModel
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z]*$")]
        public string EmployeeName { get; set; }
        [Required]
        [RegularExpression(@"(\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,6})")]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
