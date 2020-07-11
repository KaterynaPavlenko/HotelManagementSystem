using System.Collections.Generic;
using HotelManagementSystem.BLL.Data;

namespace HotelManagementSystem.BLL.Interfaces.ServiceInterfaces
{
    public interface IRoomTypeServices
    {
        /// <summary>
        ///     Get all room types from the DB
        /// </summary>
        /// <returns>Room type collection</returns>
        IEnumerable<RoomType> GetAll();

        /// <summary>
        ///     Get room type by id from the DB
        /// </summary>
        /// <param name="id">Room type id</param>
        /// <returns>Room type object</returns>
        RoomType GetById(int id);

        /// <summary>
        ///     Adding a room type to the database
        /// </summary>
        /// <param name="roomType">RoomType object</param>
        bool Create(RoomType roomType);

        /// <summary>
        ///     Updating a room type in the database
        /// </summary>
        /// <param name="roomType">RoomType object</param>
        bool Update(RoomType roomType);

        /// <summary>
        ///     Removing a room type by id in the database
        /// </summary>
        /// <param name="id">Room Type id</param>
        bool Delete(int id);
    }
}