using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using HotelManagementSystem.BLL.Data;
using HotelManagementSystem.BLL.Interfaces.ServiceInterfaces;
using HotelManagementSystem.Jobs;
using HotelManagementSystem.Models;
using HotelManagementSystem.Utilities;
using log4net;
using Microsoft.AspNet.Identity;

namespace HotelManagementSystem.Controllers
{
    [Authorize(Roles = "client")]
    public class BookingController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger("LOGGER");
        private readonly IBookingService _bookingService;
        private readonly IRoomService _roomService;
        private readonly IRoomStatusService _roomStatusService;


        public BookingController(IBookingService bookingService, IRoomService roomService,
            IRoomStatusService roomStatusService)
        {
            _bookingService = bookingService;
            _roomService = roomService;
            _roomStatusService = roomStatusService;
        }

        // GET: Booking
        public ActionResult Booking(BookingViewModel bookingViewModel, string errorMessage)
        {
            if (bookingViewModel == null)
            {
                Logger.Log.Debug("Invalid value of Model");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var room = _roomService.GetById(bookingViewModel.RoomId);
            if (room == null)
            {
                Logger.Log.Debug("Invalid value of room id");
                return HttpNotFound();
            }

            bookingViewModel.RoomNumber = room.RoomNumber;
            ViewBag.ErrorMessage = errorMessage;
            return View(bookingViewModel);
        }

        // POST: Booking
        [HttpPost]
        public ActionResult Booking(BookingViewModel bookingViewModel)
        {
            if (bookingViewModel == null)
            {
                Logger.Log.Debug("Invalid value of Model");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Validation of entered dates.
            if (bookingViewModel.DateFrom > bookingViewModel.DateTo)
                return RedirectToAction("Booking", "Booking",
                    new
                    {
                        bookingViewModel.RoomNumber,
                        bookingViewModel.RoomId,
                        errorMessage =
                            "The date of arrival cannot be later than the date of departure. Enter the dates correctly"
                    });
            if (bookingViewModel.DateFrom == DateTime.MinValue && bookingViewModel.DateTo == DateTime.MinValue)
                return RedirectToAction("Booking", "Booking",
                    new
                    {
                        bookingViewModel.RoomNumber,
                        bookingViewModel.RoomId,
                        errorMessage = "Please enter a dates"
                    });
            bookingViewModel.HotelUserId = User.Identity.GetUserId();
            bookingViewModel.TotalPrice = GetTotalRoomPrice(bookingViewModel.RoomId, bookingViewModel.DateTo,
                bookingViewModel.DateFrom);
            try
            {
                if (ModelState.IsValid)
                {
                    var booking = new Booking
                    {
                        DateFrom = bookingViewModel.DateFrom,
                        DateTo = bookingViewModel.DateTo,
                        TotalPrice = bookingViewModel.TotalPrice,
                        HotelUserId = bookingViewModel.HotelUserId,
                        RoomId = bookingViewModel.RoomId,
                        Payment = bookingViewModel.Payment
                    };
                    Logger.Log.Debug("Add new booking");
                    _bookingService.Create(booking);
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Error adding new booking", ex);
                ModelState.AddModelError("Error adding new booking", ex.Message);
            }

            bookingViewModel.Id = _bookingService.GetAll()
                .First(bookingEntity => bookingEntity.DateFrom == bookingViewModel.DateFrom &&
                                        bookingEntity.DateTo == bookingViewModel.DateTo &&
                                        bookingEntity.HotelUserId == bookingViewModel.HotelUserId &&
                                        bookingEntity.RoomId == bookingViewModel.RoomId).Id;
            var room = _roomService.GetById(bookingViewModel.RoomId);
            if (room == null)
            {
                Logger.Log.Debug("Invalid value of room id");
                return HttpNotFound();
            }

            bookingViewModel.RoomNumber = room.RoomNumber;
            room.RoomStatusId = _roomStatusService.GetAll().First(rs => rs.Name == "Booked").Id;
            try
            {
                log.Debug("Updating room status");
                _roomService.Update(room);
            }
            catch (Exception ex)
            {
                log.Error("Error updating room status", ex);
                return Content(ex.Message);
            }

            return RedirectToAction("BookingDeleteWithoutPayment", "Booking",
                new
                {
                    bookingViewModel.Id,
                    bookingViewModel.DateFrom,
                    bookingViewModel.DateTo,
                    bookingViewModel.Payment,
                    bookingViewModel.TotalPrice,
                    bookingViewModel.RoomNumber
                });
        }

        private decimal GetTotalRoomPrice(int roomId, DateTime dateTo, DateTime dateFrom)
        {
            return _roomService.GetById(roomId).RoomPriceForOneNight * (dateTo - dateFrom).Days;
        }

        public async Task<ActionResult> BookingDeleteWithoutPayment(BookingViewModel bookingViewModel)
        {
            if (!ModelState.IsValid)
            {
                Logger.Log.Debug("Invalid value of Model");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            await Task.Run(() => BookingScheduler.Start(DateTime.Now, bookingViewModel.Id));
            return RedirectToAction("BookingInformation", "Booking",
                new
                {
                    bookingViewModel.DateFrom,
                    bookingViewModel.DateTo,
                    bookingViewModel.Payment,
                    bookingViewModel.TotalPrice,
                    bookingViewModel.RoomNumber
                });
        }

        // GET: Booking/BookingInformation
        [HttpGet]
        public ActionResult BookingInformation(BookingViewModel booking)
        {
            if (!ModelState.IsValid)
            {
                Logger.Log.Debug("Invalid value of Model");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.AllBookingDays = booking.DateTo.AddDays(1).Subtract(booking.DateFrom).Days;
            return View(booking);
        }
    }
}