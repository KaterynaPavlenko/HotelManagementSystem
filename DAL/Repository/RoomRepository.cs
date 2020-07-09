using System;
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
    public class RoomRepository : IRepository<Room>
    {
        private readonly HotelDbContext _hotelDbContext;

        /// <summary>
        ///     Initializes a new instance of the RoomRepository
        /// </summary>
        /// <param name="context">The context</param>
        public RoomRepository(HotelDbContext context)
        {
            _hotelDbContext = context;
        }

        /// <summary>
        ///     Get rooms from the DB
        /// </summary>
        /// <returns>RoomsCollection</returns>
        public IEnumerable<Room> GetAll()
        {
            var entities = _hotelDbContext.Rooms.Include(x => x.RoomStatus).Include(x => x.RoomType).ToList();

            var rooms = new List<Room>();
            foreach (var roomEntity in entities)
            {
                var room = new Room
                {
                    Id = roomEntity.Id,
                    RoomDescription = roomEntity.RoomDescription,
                    RoomNumber = roomEntity.RoomNumber,
                    RoomPriceForOneNight = roomEntity.RoomPriceForOneNight,
                    Sleeps = roomEntity.Sleeps,
                    RoomImage = roomEntity.RoomImage,
                    RoomTypeId = roomEntity.RoomTypeId,
                    RoomStatusId = roomEntity.RoomStatusId,
                    RoomType = roomEntity.RoomType.Name,
                    RoomStatus = roomEntity.RoomStatus.Name
                };
                rooms.Add(room);
            }

            return rooms;
        }

        /// <summary>
        ///     Get rooms by id from the DB
        /// </summary>
        /// <param name="id">Room id</param>
        /// <returns>Room entity</returns>
        public Room GetById(int id)
        {
            var roomEntity = _hotelDbContext.Rooms.Find(id);
            if (roomEntity == null) return null;
            var room = new Room
            {
                Id = roomEntity.Id,
                RoomDescription = roomEntity.RoomDescription,
                RoomImage = roomEntity.RoomImage,
                RoomNumber = roomEntity.RoomNumber,
                RoomPriceForOneNight = roomEntity.RoomPriceForOneNight,
                Sleeps = roomEntity.Sleeps,
                RoomTypeId = roomEntity.RoomTypeId,
                RoomStatusId = roomEntity.RoomStatusId,
                RoomStatus = roomEntity.RoomStatus.Name,
                RoomType = roomEntity.RoomType.Name
            };
            return room;
        }

        /// <summary>
        ///     Adding a room to the database
        /// </summary>
        /// <param name="room">Room entity</param>
        public void Create(Room room)
        {
            if (room == null) throw new NullReferenceException();
            var entity = new RoomEntity
            {
                Id = room.Id,
                RoomDescription = room.RoomDescription,
                RoomImage = room.RoomImage,
                RoomNumber = room.RoomNumber,
                RoomPriceForOneNight = room.RoomPriceForOneNight,
                Sleeps = room.Sleeps,
                RoomTypeId = room.RoomTypeId,
                RoomStatusId = room.RoomStatusId
            };
            _hotelDbContext.Rooms.Add(entity);
            _hotelDbContext.SaveChanges();
        }

        /// <summary>
        ///     Updating a room in the database
        /// </summary>
        /// <param name="room">Room entity</param>
        public void Update(Room room)
        {
            var roomEntity = new RoomEntity
            {
                Id = room.Id,
                RoomDescription = room.RoomDescription,
                RoomImage = room.RoomImage,
                RoomNumber = room.RoomNumber,
                RoomPriceForOneNight = room.RoomPriceForOneNight,
                Sleeps = room.Sleeps,
                RoomTypeId = room.RoomTypeId,
                RoomStatusId = room.RoomStatusId
            };
            _hotelDbContext.Rooms.AddOrUpdate(roomEntity);
            _hotelDbContext.SaveChanges();
        }

        /// <summary>
        ///     Removing a room by id in the database
        /// </summary>
        /// <param name="id">Room id</param>
        public void Delete(int id)
        {
            var roomEntity = _hotelDbContext.Rooms.Find(id);
            if (roomEntity != null)
            {
                _hotelDbContext.Rooms.Remove(roomEntity);
                _hotelDbContext.SaveChanges();
            }
        }
    }
}