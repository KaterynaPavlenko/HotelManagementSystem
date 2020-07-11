using System.Collections.Generic;
using System.Web.ModelBinding;
using HotelManagementSystem.BLL.Data;
using HotelManagementSystem.BLL.Interfaces;
using HotelManagementSystem.BLL.Interfaces.ServiceInterfaces;

namespace HotelManagementSystem.BLL.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private ModelStateDictionary _modelState;

        /// <summary>
        ///     Initializes a new instance of the Unit Of Work
        /// </summary>
        /// <param name="uow"></param>
        public BookingService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }


        /// <summary>
        ///     Get all booking from the DB
        /// </summary>
        /// <returns>Booking Collection</returns>
        public IEnumerable<Booking> GetAll()
        {
            return _unitOfWork.Bookings.GetAll();
        }

        /// <summary>
        ///     Get booking by id from the DB
        /// </summary>
        /// <param name="id">Booking id</param>
        /// <returns>Booking entity</returns>
        public Booking GetById(int id)
        {
            var booking = _unitOfWork.Bookings.GetById(id);
            return booking;
        }

        /// <summary>
        ///     Adding a booking to the database
        /// </summary>
        /// <param name="booking">Booking Entity</param>
        public bool Create(Booking booking)
        {
            // Validation 
            if (!ValidateBooking(booking))
                return false;
            try
            {
                _unitOfWork.Bookings.Create(booking);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Updating a booking in the database
        /// </summary>
        /// <param name="booking">Booking Entity</param>
        public bool Update(Booking booking)
        {
            try
            {
                _unitOfWork.Bookings.Update(booking);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Removing a booking by id in the database
        /// </summary>
        /// <param name="id">Booking id</param>
        public bool Delete(int id)
        {
            try
            {
                _unitOfWork.Bookings.Delete(id);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Validation
        /// </summary>
        /// <param name="bookingToValidate">Booking object</param>
        /// <returns></returns>
        protected bool ValidateBooking(Booking bookingToValidate)
        {
            if (bookingToValidate.TotalPrice < 0)
                _modelState.AddModelError("TotalPrice", "Total price cannot be less than zero.");
            return _modelState.IsValid;
        }
    }
}