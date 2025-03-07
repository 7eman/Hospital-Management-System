using Hospital.Models;
using Hospital.Repositories.Interfaces;
using Hospital.ViewModels;
using hospitals.Utilities;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class RoomService : IRoomService
    {
        private IUnitOfWork _unitOfWork;

        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteRoom(int id)
        {
            var model = _unitOfWork.GenericRepository<Room>().GetById(id);
            _unitOfWork.GenericRepository<Room>().Delete(model);
            _unitOfWork.Save();
        }
        int totalCount;
        public PagedResult<RoomViewModels> GetAll(int pageNumber, int pageSize)
        {
            var vm = new RoomViewModels();

            List<RoomViewModels> vmList = new List<RoomViewModels>();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;

                var modelList = _unitOfWork.GenericRepository<Room>().GetAll().Skip(ExcludeRecords).Take(pageSize).ToList();
                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }

            var result = new PagedResult<RoomViewModels>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        public RoomViewModels GetRoomById(int RoomId)
        {
            var model = _unitOfWork.GenericRepository<Room>().GetById(RoomId);
            var vm = new RoomViewModels(model);
            return vm;
        }


        public void InsertRoom(RoomViewModels roomViewModel)
        {
            var model = new Room
            {
                Id = roomViewModel.Id,
                RoomNumber = roomViewModel.RoomNumber,
                Type = roomViewModel.Type,
                Status = roomViewModel.Status,
                HospitalId = roomViewModel.HospitalInfoId, 
                HospitalInfoId = roomViewModel.HospitalInfoId, 
                HospitalInfo = roomViewModel.HospitalInfo
            };

            _unitOfWork.GenericRepository<Room>().Add(model);
            _unitOfWork.Save();
        }





        public void UpdateRoom(RoomViewModels Room)
        {
            var model = new RoomViewModels().ConvertViewMosel(Room);
            var ModelById = _unitOfWork.GenericRepository<Room>().GetById(model.Id);
            ModelById.Type = Room.Type;
            ModelById.RoomNumber = Room.RoomNumber;
            ModelById.Status = Room.Status;
            ModelById.HospitalId = Room.HospitalInfoId;


            _unitOfWork.GenericRepository<Room>().Update(ModelById);
            _unitOfWork.Save();
        }

        public List<RoomViewModels> ConvertModelToViewModelList(List<Room> modelList)
        {
            return modelList.Select(x => new RoomViewModels(x)).ToList();
        }


      
    }
}

