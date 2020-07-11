using System.Collections.Generic;
using HotelManagementSystem.BLL.Data;

namespace HotelManagementSystem.BLL.Interfaces.ServiceInterfaces
{
    public interface IBookingService
    {
        /// <summary>
        ///     Getting all booking
        /// </summary>
        /// <returns>Booking collection</returns>
        IEnumerable<Booking> GetAll();

        /// <summary>
        ///     Getting booking by id
        /// </summary>
        /// <param name="id">Booking id</param>
        /// <returns>Booking object</returns>
        Booking GetById(int id);

        /// <summary>
        ///     Add a booking using a Booking Object
        /// </summary>
        /// <param name="room">Booking object</param>
        bool Create(Booking room);

        /// <summary>
        ///     Update a booking using a Booking Object
        /// </summary>
        /// <param name="room">Booking object</param>
        bool Update(Booking room);

        /// <summary>
        ///     Delete a booking by id
        /// </summary>
        /// <param name="id">Booking id</param>
        bool Delete(int id);
    }
}