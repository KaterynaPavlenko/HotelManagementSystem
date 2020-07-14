using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using HotelManagementSystem.BLL.Data;
using HotelManagementSystem.BLL.Interfaces.ServiceInterfaces;
using HotelManagementSystem.Models;

namespace HotelManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IRoomStatusService _roomStatusServices;
        private readonly IRoomTypeServices _roomTypeServices;

        public HomeController(IRoomService roomService, IRoomTypeServices roomTypeServices,
            IRoomStatusService roomStatusServices)
        {
            _roomService = roomService;
            _roomTypeServices = roomTypeServices;
            _roomStatusServices = roomStatusServices;
        }

        public ActionResult Index(string sortOrder, int? roomType, int? status)
        {
            var roomsEntity = _roomService.GetAll();
            var roomTypes = _roomTypeServices.GetAll().ToList();
            var roomStatuses = _roomStatusServices.GetAll().ToList();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Room, RoomViewModel>())
                .CreateMapper();
            var rooms =
                mapper.Map<IEnumerable<Room>, IEnumerable<RoomViewModel>>(roomsEntity);
            rooms = SortRoom(sortOrder, rooms);

            //Filter by room type
            rooms = FilterRoomsByType(roomType, rooms);
            //Filter by room status
            rooms = FilterRoomsByStatus(status, rooms);


            var roomsListViewModel = new RoomsListViewModel
            {
                Rooms = rooms,
                RoomTypes = new SelectList(roomTypes, "Id", "Name"),
                Statuses = new SelectList(roomStatuses, "Id", "Name"),
                SleepsPricesSort = new SelectList(new List<string>
                {
                    "All",
                    "Price: High to Low",
                    "Price: Low to High",
                    "Sleeps: High to Low",
                    "Sleeps: Low to High"
                })
            };
            return View(roomsListViewModel);
        }

        /// <summary>
        ///     Filter by room type
        /// </summary>
        /// <param name="roomType"></param>
        /// <param name="rooms"></param>
        /// <returns></returns>
        private IEnumerable<RoomViewModel> FilterRoomsByType(int? roomType, IEnumerable<RoomViewModel> rooms)
        {
            if (roomType != null && roomType != 1)
                rooms = rooms.Where(p => p.RoomTypeId == roomType);
            return rooms;
        }

        /// <summary>
        ///     Filter by room status
        /// </summary>
        /// <param name="status"></param>
        /// <param name="rooms"></param>
        /// <returns></returns>
        private IEnumerable<RoomViewModel> FilterRoomsByStatus(int? status, IEnumerable<RoomViewModel> rooms)
        {
            if (status != null && status != 1)
                rooms = rooms.Where(x => x.RoomStatusId == status);
            return rooms;
        }

        private IEnumerable<RoomViewModel> SortRoom(string sortOrder, IEnumerable<RoomViewModel> rooms)
        {
            //Sort by selected parameter, if it is selected
            if (!string.IsNullOrEmpty(sortOrder) && !sortOrder.Equals("All"))
            {
                if (sortOrder == "Price: High to Low")
                    rooms = rooms.OrderByDescending(p => p.RoomPriceForOneNight);
                else if (sortOrder == "Price: Low to High")
                    rooms = rooms.OrderBy(p => p.RoomPriceForOneNight);
                else if (sortOrder == "Sleeps: Low to High")
                    rooms = rooms.OrderBy(p => p.Sleeps);
                else if (sortOrder == "Sleeps: High to Low")
                    rooms = rooms.OrderByDescending(p => p.Sleeps);
            }

            return rooms;
        }

        [HttpPost]
        public ActionResult Index(int? id)
        {
            return RedirectToAction("Booking", "Booking",
                new {RoomId = id});
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}