using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using HotelManagementSystem.BLL.Data;
using HotelManagementSystem.BLL.Interfaces.ServiceInterfaces;
using HotelManagementSystem.Models;
using HotelManagementSystem.Utilities;
using PagedList;

namespace HotelManagementSystem.Controllers
{
    [Authorize(Roles = "administrator")]
    public class AdminController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IRoomStatusService _roomStatusServices;
        private readonly IRoomTypeServices _roomType;

        public AdminController(IRoomTypeServices roomType, IRoomStatusService roomStatusServices,
            IRoomService roomService)
        {
            _roomType = roomType;
            _roomStatusServices = roomStatusServices;
            _roomService = roomService;
        }

        // GET: Admin
        public ActionResult AdminManage()
        {
            return View();
        }

        // GET: Admin/RoomList
        public ActionResult RoomList(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "Room_Number_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Price" ? "Price_desc" : "Price";
            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            var roomList = _roomService.GetAll();
            if (!string.IsNullOrEmpty(searchString))
                roomList = roomList.Where(s => s.RoomNumber.ToUpper().Contains(searchString.ToUpper())
                                               || s.RoomStatus.ToUpper().Contains(searchString.ToUpper()));
            switch (sortOrder)
            {
                case "Room_Number_desc":
                    roomList = roomList.OrderByDescending(s => s.RoomNumber);
                    break;
                case "Price":
                    roomList = roomList.OrderBy(s => s.RoomPriceForOneNight);
                    break;
                case "Price_desc":
                    roomList = roomList.OrderByDescending(s => s.RoomPriceForOneNight);
                    break;
                default: // RoomNumber ascending 
                    roomList = roomList.OrderBy(s => s.RoomNumber);
                    break;
            }

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Room, RoomViewModel>()).CreateMapper();
            var rooms = mapper.Map<IEnumerable<Room>, List<RoomViewModel>>(roomList);
            var pageSize = 3;
            var pageNumber = page ?? 1;
            return View(rooms.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/CreateRoom
        [HttpGet]
        public ActionResult CreateRoom()
        {
            var roomTypesList = _roomType.GetAll();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RoomType, RoomTypeViewModel>()).CreateMapper();
            var roomTypes = mapper.Map<IEnumerable<RoomType>, List<RoomTypeViewModel>>(roomTypesList);
            var type = new SelectList(roomTypes, "Id", "Name");
            ViewBag.RoomTypes = type;

            var roomStatusList = _roomStatusServices.GetAll();
            var mapperRoomStatus = new MapperConfiguration(cfg => cfg.CreateMap<RoomStatus, RoomStatusViewModel>())
                .CreateMapper();
            var roomStatuses = mapperRoomStatus.Map<IEnumerable<RoomStatus>, List<RoomStatusViewModel>>(roomStatusList);
            var roomStatus = new SelectList(roomStatuses, "Id", "Name");
            ViewBag.RoomStatuses = roomStatus;
            return View();
        }

        // POST: Admin/CreateRoom
        [HttpPost]
        public ActionResult CreateRoom(RoomViewModel roomViewModel)
        {
            var fileName = Path.GetFileNameWithoutExtension(roomViewModel.ImageFile.FileName);
            var extension = Path.GetExtension(roomViewModel.ImageFile.FileName);
            fileName += extension;
            roomViewModel.RoomImage = "~/Content/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Content/Image/"), fileName);
            try
            {
                roomViewModel.ImageFile.SaveAs(fileName);
            }
            catch (DirectoryNotFoundException dirEx)
            {
                Logger.Log.Error("Error adding image.", dirEx);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var room = new Room
                    {
                        RoomDescription = roomViewModel.RoomDescription,
                        RoomNumber = roomViewModel.RoomNumber,
                        RoomPriceForOneNight = roomViewModel.RoomPriceForOneNight,
                        Sleeps = roomViewModel.Sleeps,
                        RoomImage = roomViewModel.RoomImage,
                        RoomTypeId = roomViewModel.RoomTypeId,
                        RoomType = roomViewModel.RoomType,
                        RoomStatus = roomViewModel.RoomStatus,
                        RoomStatusId = roomViewModel.RoomStatusId
                    };
                    Logger.Log.Debug("Add new room");
                    _roomService.Create(room);
                    return RedirectToAction("RoomList");
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Error adding new room", ex);
                ModelState.AddModelError("", ex.Message);
            }

            return RedirectToAction("CreateRoom");
        }

        // GET: /Admin/Edit/5
        public ActionResult EditRoom(int? id)
        {
            if (id == null)
            {
                Logger.Log.Debug("Invalid value of room id");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Get room types
            var roomTypesList = _roomType.GetAll();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RoomType, RoomTypeViewModel>()).CreateMapper();
            var roomTypes = mapper.Map<IEnumerable<RoomType>, List<RoomTypeViewModel>>(roomTypesList);
            var type = new SelectList(roomTypes, "Id", "Name");
            ViewBag.RoomTypes = type;

            //Get room statuses
            var roomStatusList = _roomStatusServices.GetAll();
            var mapperRoomStatus = new MapperConfiguration(cfg => cfg.CreateMap<RoomStatus, RoomStatusViewModel>())
                .CreateMapper();
            var roomStatuses = mapperRoomStatus.Map<IEnumerable<RoomStatus>, List<RoomStatusViewModel>>(roomStatusList);
            var roomStatus = new SelectList(roomStatuses, "Id", "Name");
            ViewBag.RoomStatuses = roomStatus;

            var room = _roomService.GetById((int) id);
            var mapperRoom = new MapperConfiguration(cfg => cfg.CreateMap<Room, RoomViewModel>()).CreateMapper();
            var roomViewModel = mapperRoom.Map<Room, RoomViewModel>(room);
            if (room == null) return HttpNotFound();
            return View(roomViewModel);
        }

        // POST: /Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRoom(RoomViewModel roomViewModel)
        {
            if (!ModelState.IsValid)
            {
                Logger.Log.Debug("Invalid value of Model");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var room = new Room
                {
                    Id = roomViewModel.Id,
                    RoomDescription = roomViewModel.RoomDescription,
                    RoomNumber = roomViewModel.RoomNumber,
                    RoomPriceForOneNight = roomViewModel.RoomPriceForOneNight,
                    Sleeps = roomViewModel.Sleeps,
                    RoomImage = roomViewModel.RoomImage,
                    RoomTypeId = roomViewModel.RoomTypeId,
                    RoomType = roomViewModel.RoomType,
                    RoomStatus = roomViewModel.RoomStatus,
                    RoomStatusId = roomViewModel.RoomStatusId
                };
                Logger.Log.Debug($"Update room. Room Id{roomViewModel.Id}");
                _roomService.Update(room);
                return RedirectToAction("AdminManage");
            }
            catch (Exception ex)
            {
                Logger.Log.Error($"Error updating new room. Room Id{roomViewModel.Id}", ex);
                ModelState.AddModelError("", ex.Message);
            }

            return View(roomViewModel);
        }

        // GET: Admin/RoomTypeList
        public ActionResult RoomTypeList(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "Room_Type_desc" : "";
            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            var roomTypes = _roomType.GetAll();
            if (!string.IsNullOrEmpty(searchString))
                roomTypes = roomTypes.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper()));
            switch (sortOrder)
            {
                case "Room_Type_desc":
                    roomTypes = roomTypes.OrderByDescending(s => s.Name);
                    break;
                default: // RoomType ascending 
                    roomTypes = roomTypes.OrderBy(s => s.Name);
                    break;
            }

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RoomType, RoomTypeViewModel>()).CreateMapper();
            var roomTypeViewModels = mapper.Map<IEnumerable<RoomType>, List<RoomTypeViewModel>>(roomTypes);
            var pageSize = 3;
            var pageNumber = page ?? 1;
            return View(roomTypeViewModels.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/CreateRoomType
        [HttpGet]
        public ActionResult CreateRoomType()
        {
            return View();
        }

        // POST: Admin/CreateRoomType
        [HttpPost]
        public ActionResult CreateRoomType(RoomTypeViewModel roomTypeViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var roomType = new RoomType
                    {
                        Name = roomTypeViewModel.Name
                    };
                    Logger.Log.Debug("Add new room type");
                    _roomType.Create(roomType);
                    return RedirectToAction("RoomTypeList");
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Error adding new room type", ex);
                ModelState.AddModelError("", ex.Message);
            }

            return RedirectToAction("RoomTypeList");
        }
    }
}