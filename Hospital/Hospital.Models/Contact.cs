﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Models

{
    public class Contact
    {
         public int Id { get; set; }
         public int HospitalId { get; set; }
         public int HospitalInfoId { get; set; } 
         public HospitalInfo HospitalInfo { get; set; }
         public string Email { get; set; }
         public string Phone { get; set; }
    }
}
