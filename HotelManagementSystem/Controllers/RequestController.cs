using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using HotelManagementSystem.BLL.Data;
using HotelManagementSystem.BLL.Interfaces.ServiceInterfaces;
using HotelManagementSystem.Models;
using HotelManagementSystem.Utilities;
using Microsoft.AspNet.Identity;

namespace HotelManagementSystem.Controllers
{
    [Authorize(Roles = "client")]
    public class RequestController : Controller
    {
        private readonly ICustomerRequestServices _customerRequestServices;
        private readonly ICustomerRequestStatusService _customerRequestStatusService;
        private readonly IRoomTypeServices _roomType;

        public RequestController(IRoomTypeServices roomType, ICustomerRequestServices customerRequestServices,
            ICustomerRequestStatusService customerRequestStatusService)
        {
            _roomType = roomType;
            _customerRequestServices = customerRequestServices;
            _customerRequestStatusService = customerRequestStatusService;
        }

        // GET: CustomerRequest
        public ActionResult CustomerRequest(string errorMessage)
        {
            ViewBag.ErrorMessage = errorMessage;
            var roomTypesList = _roomType.GetAll();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RoomType, RoomTypeViewModel>()).CreateMapper();
            var roomTypes = mapper.Map<IEnumerable<RoomType>, List<RoomTypeViewModel>>(roomTypesList);
            var type = new SelectList(roomTypes, "Id", "Name");
            ViewBag.RoomTypes = type;
            return View();
        }

        // POST: CustomerRequest
        [HttpPost]
        public ActionResult CustomerRequest(CustomerRequestViewModel customerRequestViewModel)
        {
            if (customerRequestViewModel == null) return HttpNotFound();
            //Validation of entered dates
            if (customerRequestViewModel.DateFrom > customerRequestViewModel.DateTo)
                return RedirectToAction("CustomerRequest", "Request",
                    new
                    {
                        errorMessage = "Arrival date can not be later than the date of departure. Enter dates correctly"
                    });
            //Validation field sleeps
            if (customerRequestViewModel.Sleeps <= 0)
                return RedirectToAction("CustomerRequest", "Request",
                    new
                    {
                        errorMessage = "Field sleeps cannot be less than zero"
                    });
            var userId = User.Identity.GetUserId();
            customerRequestViewModel.CustomerRequestStatusId =
                _customerRequestStatusService.GetAll().First(x => x.Name == "New request").Id;
            try
            {
                if (ModelState.IsValid)
                {
                    var customerRequest = new CustomerRequest
                    {
                        CustomerRequestStatusId = customerRequestViewModel.CustomerRequestStatusId,
                        DateTo = customerRequestViewModel.DateTo,
                        DateFrom = customerRequestViewModel.DateFrom,
                        Sleeps = customerRequestViewModel.Sleeps,
                        RoomTypeId = customerRequestViewModel.RoomTypeId,
                        HotelUserId = userId
                    };
                    Logger.Log.Debug("Add new customer request");
                    _customerRequestServices.Create(customerRequest);
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Error adding new customer request", ex);
                ModelState.AddModelError(ex.Source, ex.Message);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}