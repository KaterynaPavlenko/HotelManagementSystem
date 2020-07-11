using System.Collections.Generic;
using HotelManagementSystem.BLL.Data;

namespace HotelManagementSystem.BLL.Interfaces.ServiceInterfaces
{
    public interface IRoomService
    {
        /// <summary>
        ///     Get rooms from the DB
        /// </summary>
        /// <returns>Rooms Collection</returns>
        IEnumerable<Room> GetAll();

        /// <summary>
        ///     Get rooms by id from the DB
        /// </summary>
        /// <param name="id">Room id</param>
        /// <returns>Room entity</returns>
        Room GetById(int id);

        /// <summary>
        ///     Adding a room to the database
        /// </summary>
        /// <param name="room">Room entity</param>
        bool Create(Room room);

        /// <summary>
        ///     Updating a room in the database
        /// </summary>
        /// <param name="room">Room entity</param>
        bool Update(Room room);

        /// <summary>
        ///     Removing a room by id in the database
        /// </summary>
        /// <param name="id">Room id</param>
        bool Delete(int id);
    }
}