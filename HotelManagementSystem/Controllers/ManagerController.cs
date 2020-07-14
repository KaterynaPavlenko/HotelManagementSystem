using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using HotelManagementSystem.BLL.Data;
using HotelManagementSystem.BLL.Interfaces.ServiceInterfaces;
using HotelManagementSystem.Models;
using HotelManagementSystem.Utilities;
using Microsoft.AspNet.Identity.Owin;

namespace HotelManagementSystem.Controllers
{
    [Authorize(Roles = "manager")]
    public class ManagerController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IConfirmationService _confirmationService;
        private readonly ICustomerRequestServices _customerRequestServices;
        private readonly ICustomerRequestStatusService _customerRequestStatusService;
        private readonly IRoomService _roomService;
        private readonly IRoomTypeServices _roomTypeServices;


        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ManagerController(ICustomerRequestServices customerRequestServices, IRoomService roomService,
            IRoomTypeServices roomTypeServices,
            IConfirmationService confirmationService, IBookingService bookingService,
            ICustomerRequestStatusService customerRequestStatusService)
        {
            _customerRequestServices = customerRequestServices;
            _roomService = roomService;
            _roomTypeServices = roomTypeServices;
            _customerRequestServices = customerRequestServices;
            _confirmationService = confirmationService;
            _bookingService = bookingService;
            _customerRequestStatusService = customerRequestStatusService;
        }

        public ManagerController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get => _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            private set => _signInManager = value;
        }

        public ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set => _userManager = value;
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult CustomerRequest()
        {
            var users = UserManager.Users.ToList();
            var roomTypes = _roomTypeServices.GetAll().ToList();
            var customerRequest = _customerRequestServices.GetAll();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CustomerRequest, CustomerRequestViewModel>())
                .CreateMapper();
            var customerRequests =
                mapper.Map<IEnumerable<CustomerRequest>, List<CustomerRequestViewModel>>(customerRequest);

            ViewBag.Users = users;
            ViewBag.RoomTypes = roomTypes;
            return View(customerRequests);
        }

        public ActionResult ChooseHotelRoom(int? requestId)
        {
            if (requestId == null)
            {
                Logger.Log.Debug("Invalid value of room id");
                return HttpNotFound();
            }

            ViewBag.RequestId = requestId;
            var rooms = _roomService.GetAll();
            return View(rooms);
        }

        [HttpPost]
        public ActionResult AskedToConfirm(string roomId, int? requestId)
        {
            if (requestId == null)
            {
                Logger.Log.Debug("Invalid value of request id");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (roomId == null)
            {
                Logger.Log.Debug("Invalid value of room id");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var customerRequest = _customerRequestServices.GetById((int) requestId);
            if (customerRequest == null)
            {
                Logger.Log.Debug("Invalid value of customerRequest id");
                return HttpNotFound();
            }

            customerRequest.CustomerRequestStatusId =
                _customerRequestStatusService.GetAll().First(crs => crs.Name == "Answer received").Id;
            try
            {
                Logger.Log.Debug("Add new confirmation");
                _customerRequestServices.Update(customerRequest);
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Error updating request", ex);
                ModelState.AddModelError("", ex.Message);
            }

            var confirmation = new ConfirmationViewModel
            {
                CustomerRequestId = (int) requestId,
                RoomId = int.Parse(roomId)
            };
            try
            {
                if (ModelState.IsValid)
                {
                    var confirmationEntity = new Confirmation
                    {
                        Id = confirmation.Id,
                        RoomId = confirmation.RoomId,
                        CustomerRequestId = confirmation.CustomerRequestId
                    };
                    Logger.Log.Debug("Add new confirmation");
                    _confirmationService.Create(confirmationEntity);
                    return RedirectToAction("Dashboard");
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Error adding new confirmation", ex);
                ModelState.AddModelError("", ex.Message);
            }

            return RedirectToAction("CustomerRequest", "Manager");
        }

        public ActionResult ReservationList()
        {
            var bookingEntity = _bookingService.GetAll();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Booking, BookingViewModel>()).CreateMapper();
            var bookingViewModel = mapper.Map<IEnumerable<Booking>, List<BookingViewModel>>(bookingEntity);
            ViewBag.Users = UserManager.Users;

            foreach (var booking in bookingViewModel)
                booking.RoomNumber = _roomService.GetById(booking.RoomId).RoomNumber;
            return View(bookingViewModel);
        }

        [HttpPost]
        public ActionResult ChangeReservationStatus(int? id)
        {
            if (id == null)
            {
                Logger.Log.Debug("Invalid value of reservation status id");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var booking = _bookingService.GetById((int) id);
            if (booking == null)
            {
                Logger.Log.Debug("No found booking in DB");
                return HttpNotFound();
            }

            booking.Payment = true;
            try
            {
                Logger.Log.Debug("Update confirmation");
                _bookingService.Update(booking);
                return RedirectToAction("Dashboard");
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Error changing  confirmation", ex);
                ModelState.AddModelError("", ex.Message);
            }

            return RedirectToAction("ReservationList", "Manager");
        }
    }
}