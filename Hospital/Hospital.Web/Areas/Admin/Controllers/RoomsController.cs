﻿using Hospital.Services;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class RoomsController : Controller
    {
        private IRoomService _room;
        private IHospitalInfo _hospitalInfo;

        public RoomsController(IRoomService room, IHospitalInfo hospitalInfo)
        {
            _room = room;
            _hospitalInfo = hospitalInfo;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_room.GetAll(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.hospital = new SelectList(_hospitalInfo.GetAll(1, 10).Data, "Id", "Name");
            var viewModel = _room.GetRoomById(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(RoomViewModels vm)
        {
            _room.UpdateRoom(vm);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.hospital = new SelectList(_hospitalInfo.GetAll(1, 10).Data, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(RoomViewModels vm)
        {
            _room.InsertRoom(vm);
            return RedirectToAction("Index");
        }




        public IActionResult Delete(int id)
        {
            _room.DeleteRoom(id);
            return RedirectToAction("Index");
        }


    }
}

