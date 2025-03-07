using Hospital.Services;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ContactController : Controller
    {
        private IContactService _contact;
        private IHospitalInfo _hospitalInfo;

        public ContactController(IContactService contact, IHospitalInfo hospitalInfo)
        {
            _contact = contact;
            _hospitalInfo = hospitalInfo;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_contact.GetAll(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _contact.GetContactById(id);
            if (viewModel == null)
            {
                return NotFound();
            }

            ViewBag.hospital = new SelectList(_hospitalInfo.GetAll(1, 10)?.Data ?? new List<HospitalInfoViewModel>(), "Id", "Name");
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(ContactViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.hospital = new SelectList(_hospitalInfo.GetAll(1, 10)?.Data ?? new List<HospitalInfoViewModel>(), "Id", "Name");
                return View(vm);
            }

            var existingContact = _contact.GetContactById(vm.Id);
            if (existingContact == null)
            {
                return NotFound();
            }

            _contact.UpdateContact(vm);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            var hospitals = _hospitalInfo.GetAll(1, 10)?.Data ?? new List<HospitalInfoViewModel>();
            ViewBag.Hospitals = hospitals;
            return View();
        }

        [HttpPost]
        public IActionResult Create(ContactViewModel vm)
        {
            Console.WriteLine($"Received HospitalInfoId: {vm.HospitalInfoId}");

            if (!ModelState.IsValid)
            {
                var hospitals = _hospitalInfo.GetAll(1, 100)?.Data ?? new List<HospitalInfoViewModel>();
                ViewBag.Hospitals = hospitals;
                return View(vm);
            }

            var hospitalExists = _hospitalInfo.GetAll(1, 100)?.Data.Any(h => h.Id == vm.HospitalInfoId) ?? false;
            if (!hospitalExists)
            {
                ModelState.AddModelError("HospitalInfoId", "Selected hospital does not exist.");
                var hospitals = _hospitalInfo.GetAll(1, 100)?.Data ?? new List<HospitalInfoViewModel>();
                ViewBag.Hospitals = hospitals;
                return View(vm);
            }
           
            _contact.InsertContact(vm);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var existingContact = _contact.GetContactById(id);
            if (existingContact == null)
            {
                return NotFound();
            }

            _contact.DeleteContact(id);
            return RedirectToAction("Index");
        }
    }
}

