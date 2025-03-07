using Hospital.ViewModels;
using hospitals.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public interface IContactService
    {
        PagedResult<ContactViewModel> GetAll(int pagreNumber , int pageSize);
        ContactViewModel GetContactById(int ContactId);
        void UpdateContact(ContactViewModel Contact);
        void DeleteContact(int id);
        void InsertContact(ContactViewModel Contact);
    }
}
