using System.Collections.Generic;
using HotelManagementSystem.BLL.Data;

namespace HotelManagementSystem.BLL.Interfaces.ServiceInterfaces
{
    public interface IRoomStatusService
    {
        /// <summary>
        ///     Get all room statuses from the DB
        /// </summary>
        /// <returns>Room Statuses Collection</returns>
        IEnumerable<RoomStatus> GetAll();

        /// <summary>
        ///     Get room status by id from the DB
        /// </summary>
        /// <param name="id">Room status id</param>
        /// <returns>Room object</returns>
        RoomStatus GetById(int id);

        /// <summary>
        ///     Adding a room status to the database
        /// </summary>
        /// <param name="roomStatus">RoomStatus entity</param>
        bool Create(RoomStatus roomStatus);

        /// <summary>
        ///     Updating a room status in the database
        /// </summary>
        /// <param name="roomStatus">RoomStatus entity</param>
        bool Update(RoomStatus roomStatus);

        /// <summary>
        ///     Removing a room status by id in the database
        /// </summary>
        /// <param name="id">Room Status id</param>
        bool Delete(int id);
    }
}