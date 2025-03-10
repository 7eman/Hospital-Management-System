﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public string Nationality { get; set; }
        public string Address { get; set; }
        public DateTime DOP { get; set; }
        public string Specialist { get; set; }
        public bool IsDoctor { get; set; }
        public int? DepartmentId { get; set; } 
        public Department? Department { get; set; }
        //public Department Department { get; set; }
        [NotMapped]
        public ICollection<Appoinment> Appoinments { get; set; }
        public ICollection<Payroll> Payrolls { get; set; }


    }

}

namespace Hospital.Models
{
    public enum Gender
    {
        Male,Female,Other
    }
}