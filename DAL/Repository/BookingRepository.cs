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
                var booking = new Booking(bookingEntity.Id, bookingEntity.DateFrom, bookingEntity.DateTo,
                    bookingEntity.TotalPrice, bookingEntity.RoomId,
                    bookingEntity.HotelUserId, bookingEntity.Payment);


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

            var booking = new Booking(bookingEntity.Id, bookingEntity.DateFrom, bookingEntity.DateTo,
                bookingEntity.TotalPrice,
                bookingEntity.RoomId,
                bookingEntity.HotelUserId, bookingEntity.Payment);
            return booking;
        }

        /// <summary>
        ///     Adding a booking to the database
        /// </summary>
        /// <param name="booking">Booking Entity</param>
        public void Create(Booking booking)
        {
            var entity = new BookingEntity
            {
                DateTo = booking.DateTo,
                DateFrom = booking.DateFrom,
                TotalPrice = booking.TotalPrice,
                HotelUserId = booking.HotelUserId,
                RoomId = booking.RoomId,
                Payment = booking.Payment
            };

            _hotelDbContext.Bookings.Add(entity);
        }


        /// <summary>
        ///     Updating a booking in the database
        /// </summary>
        /// <param name="booking">Booking Entity</param>
        public void Update(Booking booking)
        {
            var entity = new BookingEntity
            {
                DateTo = booking.DateTo,
                DateFrom = booking.DateFrom,
                TotalPrice = booking.TotalPrice,
                HotelUserId = booking.HotelUserId,
                RoomId = booking.RoomId,
                Payment = booking.Payment
            };

            _hotelDbContext.Bookings.AddOrUpdate(entity);
            _hotelDbContext.SaveChanges();
        }

        /// <summary>
        ///     Removing a booking by id in the database
        /// </summary>
        /// <param name="id">Booking id</param>
        public void Delete(int id)
        {
            var booking = _hotelDbContext.Bookings.Find(id);
            if (booking != null)
                _hotelDbContext.Bookings.Remove(booking);
            _hotelDbContext.SaveChanges();
        }
    }
}