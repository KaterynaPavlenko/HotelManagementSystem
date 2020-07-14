using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using HotelManagementSystem.BLL.Data;
using HotelManagementSystem.BLL.Interfaces;
using HotelManagementSystem.DAL.Context;
using HotelManagementSystem.DAL.Entity;

namespace HotelManagementSystem.DAL.Repository
{
    public class BookingRepository : IRepository<Booking>
    {
        private readonly HotelDbContext _hotelDbContext;

        /// <summary>
        ///     Initializes a new instance of the BookingRepository
        /// </summary>
        /// <param name="hotelDbContext">The context</param>
        public BookingRepository(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }

        /// <summary>
        ///     Getting all booking from the DB
        /// </summary>
        /// <returns>Booking Collection</returns>
        public IEnumerable<Booking> GetAll()
        {
            var entities = _hotelDbContext.Bookings.Include(x => x.HotelUser).Include(y => y.Room).ToArray();
            var bookings = new List<Booking>();
            foreach (var bookingEntity in entities)
            {
                var booking = new Booking
                {
                    Id = bookingEntity.Id,
                    DateFrom = bookingEntity.DateFrom,
                    DateTo = bookingEntity.DateTo,
                    TotalPrice = bookingEntity.TotalPrice,
                    RoomId = bookingEntity.RoomId,
                    HotelUserId = bookingEntity.HotelUserId,
                    Payment = bookingEntity.Payment,
                    IsDeleted = bookingEntity.IsDeleted
                };


                bookings.Add(booking);
            }

            return bookings;
        }

        /// <summary>
        ///     Getting booking by id from the DB
        /// </summary>
        /// <param name="id">Booking id</param>
        /// <returns>Booking entity</returns>
        public Booking GetById(int id)
        {

            var bookingEntity = _hotelDbContext.Bookings.Find(id);

            var booking = new Booking
            {
               Id = bookingEntity.Id,
               DateFrom = bookingEntity.DateFrom, 
               DateTo = bookingEntity.DateTo,
              TotalPrice  = bookingEntity.TotalPrice,
                RoomId = bookingEntity.RoomId,
               HotelUserId = bookingEntity.HotelUserId,
               Payment = bookingEntity.Payment,
               IsDeleted = bookingEntity.IsDeleted
            };
            return booking;
        }

        /// <summary>
        ///     Adding a booking to the database
        /// </summary>
        /// <param name="booking">Booking Entity</param>
        public bool Create(Booking booking)
        {
            try
            {
                var entity = new BookingEntity
                {
                    DateTo = booking.DateTo,
                    DateFrom = booking.DateFrom,
                    TotalPrice = booking.TotalPrice,
                    HotelUserId = booking.HotelUserId,
                    RoomId = booking.RoomId,
                    Payment = booking.Payment,
                    IsDeleted = booking.IsDeleted
                };
                _hotelDbContext.Bookings.Add(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        ///     Updating a booking in the database
        /// </summary>
        /// <param name="booking">Booking Entity</param>
        public bool Update(Booking booking)
        {
            try
            {
                var entity = new BookingEntity
                {
                    Id = booking.Id,
                    DateTo = booking.DateTo,
                    DateFrom = booking.DateFrom,
                    TotalPrice = booking.TotalPrice,
                    HotelUserId = booking.HotelUserId,
                    RoomId = booking.RoomId,
                    Payment = booking.Payment,
                    IsDeleted = booking.IsDeleted
                };
                _hotelDbContext.Bookings.AddOrUpdate(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Removing a booking by id in the database
        /// </summary>
        /// <param name="id">Booking id</param>
        public bool Delete(int id)
        {
            try
            {
                var booking = _hotelDbContext.Bookings.Find(id);
                if (booking != null)
                    _hotelDbContext.Bookings.Remove(booking);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}