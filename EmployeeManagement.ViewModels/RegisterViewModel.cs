﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;

namespace EmployeeManagement.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]*$")]
        public string Name { get; set; }
        [Required]
        public string Mobile { get; set; }
        public string Role { get; set; }
        public string ImageUrl { get; set; }


    }
}
