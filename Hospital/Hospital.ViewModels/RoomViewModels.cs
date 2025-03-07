using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.ViewModels
{
    public class RoomViewModels
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public int HospitalInfoId { get; set; }
        public HospitalInfo HospitalInfo { get; set; }

        public RoomViewModels()
        {

        }
        public RoomViewModels(Room model)
        {
            Id = model.Id;
            RoomNumber = model.RoomNumber;
            Type = model.Type;
            Status = model.Status;
            HospitalInfoId = model.HospitalId;
        }

        public Room ConvertViewMosel(RoomViewModels model)
        {
            return new Room
            {
                Id = model.Id,
                RoomNumber = model.RoomNumber,
                Type = model.Type,
                Status = model.Status,
                HospitalId = model.HospitalInfoId,
                HospitalInfo = model.HospitalInfo
            };
        }


    }
}
