using System;

namespace HotelManagementSystem.DAL.Entity
{
    public class BookingEntity
    {
        public int Id { get; set; }

        /// <summary>
        ///     Start date of booking
        /// </summary>
        public DateTime DateFrom { get; set; }

        /// <summary>
        ///     End date of booking
        /// </summary>
        public DateTime DateTo { get; set; }

        /// <summary>
        ///     The total price of the reservation according to
        ///     the selected number of days and the price of the room
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        ///     Id of the customer who made the reservation
        /// </summary>
        public string HotelUserId { get; set; }

        /// <summary>
        ///     Booked room id
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        ///     Delete flag
        /// </summary>
        public bool IsDeleted { get; set; }

        public bool Payment { get; set; }
        public virtual HotelUserEntity HotelUser { get; set; }
        public virtual RoomEntity Room { get; set; }
    }
}