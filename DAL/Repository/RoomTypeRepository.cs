using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using HotelManagementSystem.BLL.Data;
using HotelManagementSystem.BLL.Interfaces;
using HotelManagementSystem.DAL.Context;
using HotelManagementSystem.DAL.Entity;

namespace HotelManagementSystem.DAL.Repository
{
    public class RoomTypeRepository : IRepository<RoomType>
    {
        private readonly HotelDbContext _hotelDbContext;

        /// <summary>
        ///     Initializes a new instance of the RoomTypeRepository
        /// </summary>
        /// <param name="context">The context</param>
        public RoomTypeRepository(HotelDbContext context)
        {
            _hotelDbContext = context;
        }

        /// <summary>
        ///     Get all room types from the DB
        /// </summary>
        /// <returns>Room type collection</returns>
        public IEnumerable<RoomType> GetAll()
        {
            var entities = _hotelDbContext.RoomTypes.ToList();
            var roomTypes = new List<RoomType>();
            foreach (var roomTypeEntity in entities)
            {
                var roomType = new RoomType
                {
                    Id = roomTypeEntity.Id,
                    Name = roomTypeEntity.Name,
                    IsDeleted = roomTypeEntity.IsDeleted
                };

                roomTypes.Add(roomType);
            }

            return roomTypes;
        }

        /// <summary>
        ///     Get room type by id from the DB
        /// </summary>
        /// <param name="id">Room type id</param>
        /// <returns>Room type object</returns>
        public RoomType GetById(int id)
        {
            var roomType = _hotelDbContext.RoomTypes.Find(id);
            var room = new RoomType
            {
                Id = roomType.Id,
                Name = roomType.Name,
                IsDeleted = roomType.IsDeleted

            };
            return room;
        }

        /// <summary>
        ///     Adding a room type to the database
        /// </summary>
        /// <param name="roomType">RoomType object</param>
        public bool Create(RoomType roomType)
        {
            try
            {
                var entity = new RoomTypeEntity
                {
                    Name = roomType.Name,
                    IsDeleted = roomType.IsDeleted

                };
                _hotelDbContext.RoomTypes.Add(entity);
                return true;
            }
            catch
            {
                return false;
            }
          
        }

        /// <summary>
        ///     Updating a room type in the database
        /// </summary>
        /// <param name="roomType">RoomType object</param>
        public bool Update(RoomType roomType)
        {
            try
            {
                var entity = new RoomTypeEntity
                {
                    Id = roomType.Id,
                    Name = roomType.Name,
                    IsDeleted = roomType.IsDeleted

                };
                _hotelDbContext.RoomTypes.AddOrUpdate(entity);
                return true;
            }
            catch
            {
                return false;
            }
        
        }

        /// <summary>
        ///     Removing a room type by id in the database
        /// </summary>
        /// <param name="id">Room Type id</param>
        public bool Delete(int id)
        {
            try
            {
                var roomType = _hotelDbContext.RoomTypes.Find(id);
                if (roomType != null)
                    _hotelDbContext.RoomTypes.Remove(roomType);
                return true;
            }
            catch
            {
                return false;
            }
        
        }
    }
}