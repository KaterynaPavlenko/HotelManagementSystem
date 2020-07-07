using System.Collections.Generic;

namespace HotelManagementSystem.DAL.Entity
{
    public class RoomStatusEntity
    {
        public RoomStatusEntity()
        {
            Rooms = new List<RoomEntity>();
        }

        /// <summary>
        ///     Id of the room status
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Room status name
        /// </summary>
        public string Name { get; set; }

        public ICollection<RoomEntity> Rooms { get; set; }
    }
}