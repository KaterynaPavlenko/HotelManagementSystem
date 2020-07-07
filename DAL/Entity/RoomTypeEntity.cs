using System.Collections.Generic;

namespace HotelManagementSystem.DAL.Entity
{
    public class RoomTypeEntity
    {
        public RoomTypeEntity()
        {
            Rooms = new List<RoomEntity>();
        }

        /// <summary>
        ///     Id of the room type
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Room type name
        /// </summary>
        public string Name { get; set; }

        public ICollection<RoomEntity> Rooms { get; set; }
    }
}