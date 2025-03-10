﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Room
    {

        public int Id { get; set; }

        public string RoomNumber { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }

        public int HospitalId { get; set; }
        public int HospitalInfoId { get; set; }

        public HospitalInfo HospitalInfo { get; set; }


    }
}
