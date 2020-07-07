using System.Collections.Generic;

namespace HotelManagementSystem.DAL.Entity
{
    public class RoomEntity
    {
        public int Id { get; set; }

        /// <summary>
        ///     Hotel room description
        /// </summary>
        public string RoomDescription { get; set; }

        /// <summary>
        ///     Hotel room number
        /// </summary>
        public string RoomNumber { get; set; }

        /// <summary>
        ///     Room price per night
        /// </summary>
        public decimal RoomPriceForOneNight { get; set; }

        /// <summary>
        ///     Maximum number of people who can stay in a room
        /// </summary>
        public int Sleeps { get; set; }

        /// <summary>
        ///     URL hotel room image
        /// </summary>
        public string RoomImage { get; set; }

        public int RoomTypeId { get; set; }
        public int RoomStatusId { get; set; }
        public virtual RoomTypeEntity RoomType { get; set; }
        public virtual RoomStatusEntity RoomStatus { get; set; }
        public virtual ICollection<ConfirmationEntity> Confirmation { get; set; }
        public virtual ICollection<BookingEntity> Booking { get; set; }
    }
}