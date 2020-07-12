using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using HotelManagementSystem.BLL.Data;
using HotelManagementSystem.BLL.Interfaces;
using HotelManagementSystem.DAL.Context;
using HotelManagementSystem.DAL.Entity;

namespace HotelManagementSystem.DAL.Repository
{
    public class RoomStatusRepository : IRepository<RoomStatus>
    {
        private readonly HotelDbContext _hotelDbContext;

        /// <summary>
        ///     Initializes a new instance of the RoomStatusRepository
        /// </summary>
        /// <param name="context">The context</param>
        public RoomStatusRepository(HotelDbContext context)
        {
            _hotelDbContext = context;
        }

        /// <summary>
        ///     Get all room statuses from the DB
        /// </summary>
        /// <returns>Room Statuses Collection</returns>
        public IEnumerable<RoomStatus> GetAll()
        {
            var entities = _hotelDbContext.RoomStatuses.ToList();
            var roomStatuses = new List<RoomStatus>();
            foreach (var roomStatusEntity in entities)
            {
                var roomStatus = new RoomStatus(roomStatusEntity.Id, roomStatusEntity.Name);

                roomStatuses.Add(roomStatus);
            }

            return roomStatuses;
        }

        /// <summary>
        ///     Get Customer Request by id from the DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RoomStatus GetById(int id)
        {
            var roomStatusEntity = _hotelDbContext.RoomStatuses.Find(id);
            var roomStatus = new RoomStatus(roomStatusEntity.Id, roomStatusEntity.Name);
            return roomStatus;
        }

        /// <summary>
        ///     Adding a room status to the database
        /// </summary>
        /// <param name="roomStatus">RoomStatus entity</param>
        public bool Create(RoomStatus roomStatus)
        {
            try
            {
                var entity = new RoomStatusEntity
                {
                    Name = roomStatus.Name
                };
                _hotelDbContext.RoomStatuses.Add(entity);
                return true;
            }
            catch
            {
                return false;
            }
           
        }

        /// <summary>
        ///     Updating a room status in the database
        /// </summary>
        /// <param name="roomStatus">RoomStatus entity</param>
        public bool Update(RoomStatus roomStatus)
        {
            try
            {
                var entity = new RoomStatusEntity
                {
                    Id = roomStatus.Id,
                    Name = roomStatus.Name
                };
                _hotelDbContext.RoomStatuses.AddOrUpdate(entity);
                return true;
            }
            catch
            {
                return false;
            }
         
        }

        /// <summary>
        ///     Removing a room status by id in the database
        /// </summary>
        /// <param name="id">Room Status id</param>
        public bool Delete(int id)
        {
            try
            {
                var roomStatus = _hotelDbContext.RoomStatuses.Find(id);
                if (roomStatus != null)
                    _hotelDbContext.RoomStatuses.Remove(roomStatus);
                return true;
            }
            catch
            {
                return false;
            }
          
        }
    }
}