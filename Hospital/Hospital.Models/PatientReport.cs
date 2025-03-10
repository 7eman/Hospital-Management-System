﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class PatientReport
    {
        public int Id { get; set; }
        public string Diagonse { get; set; }
        public ApplicationUser Doctor { get; set; }
        public ApplicationUser Patient { get; set; }
        public ICollection<PrescribedMedicine>PrescribedMedicines { get; set; }
    }
}
