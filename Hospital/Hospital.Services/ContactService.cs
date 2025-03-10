﻿using Hospital.Models;
using Hospital.Repositories.Interfaces;
using Hospital.ViewModels;
using hospitals.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class ContactService : IContactService
    {
        private IUnitOfWork _unitOfWork;

        public ContactService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteContact(int id)
        {
            var model = _unitOfWork.GenericRepository<Contact>().GetById(id);
            _unitOfWork.GenericRepository<Contact>().Delete(model);
            _unitOfWork.Save();
        }

        public PagedResult<ContactViewModel> GetAll(int pagreNumber, int pageSize)
        {
            var vm = new ContactViewModel();
            int totalCount;
            List<ContactViewModel> vmList = new List<ContactViewModel>();
            try
            {
                int ExcludeRecords = (pageSize * pagreNumber) - pageSize;

                var modelList = _unitOfWork.GenericRepository<Contact>().GetAll(includeProperties: "HospitalInfo").Skip(ExcludeRecords).Take(pageSize).ToList();

                totalCount = _unitOfWork.GenericRepository<Contact>().GetAll().ToList().Count;

                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }
            var result = new PagedResult<ContactViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pagreNumber,
                PageSize = pageSize,
                
            };
            return result;
        }

        public ContactViewModel GetContactById(int ContactId)
        {
            var model = _unitOfWork.GenericRepository<Contact>().GetById(ContactId);
            var vm = new ContactViewModel(model);
            return vm;
        }


        public void InsertContact(ContactViewModel model)
        {
            Console.WriteLine($"HospitalId: {model.HospitalInfoId}");

            var hospitalExists = _unitOfWork.GenericRepository<HospitalInfo>().GetById(model.HospitalInfoId);
            if (hospitalExists == null)
            {
                throw new Exception($"Hospital with ID {model.HospitalInfoId} does not exist in the database.");
            }

            var contact = new Contact
            {
                Email = model.Email,
                Phone = model.Phone,
                HospitalId = model.HospitalInfoId,  
                HospitalInfoId = model.HospitalInfoId
            };

            _unitOfWork.GenericRepository<Contact>().Add(contact);
            _unitOfWork.Save();
        }


       


        public void UpdateContact(ContactViewModel Contact)
        {
            var model = new ContactViewModel().ConvertViewModel(Contact);
            var ModelById = _unitOfWork.GenericRepository<Contact>().GetById(model.Id);

            ModelById.Phone = Contact.Phone;
            ModelById.Email = Contact.Email;
            ModelById.HospitalId = Contact.HospitalInfoId;  
            ModelById.HospitalInfoId = Contact.HospitalInfoId; 

            _unitOfWork.GenericRepository<Contact>().Update(ModelById);
            _unitOfWork.Save();
        }




        private List<ContactViewModel> ConvertModelToViewModelList(List<Contact> modelList)
        {
            return modelList.Select(x => new ContactViewModel(x)).ToList();
        }
    }
}


