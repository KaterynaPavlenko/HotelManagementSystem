using System;
using System.Collections.Generic;

namespace HotelManagementSystem.DAL.Entity
{
    public class CustomerRequestEntity
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
        ///     Maximum number of people who can stay in a room
        /// </summary>

        public int Sleeps { get; set; }

        /// <summary>
        ///     Room type id
        /// </summary>
        public int RoomTypeId { get; set; }

        /// <summary>
        ///     Customer request status
        /// </summary>
        public int CustomerRequestStatusId { get; set; }

        /// <summary>
        ///     Id of the client who made the request
        /// </summary>
        public string HotelUserId { get; set; }

        /// <summary>
        ///     Delete flag
        /// </summary>
        public bool IsDeleted { get; set; }


        public virtual HotelUserEntity HotelUser { get; set; }
        public virtual CustomerRequestStatusEntity CustomerRequestStatus { get; set; }
        public virtual RoomTypeEntity RoomType { get; set; }
        public virtual ICollection<ConfirmationEntity> Confirmation { get; set; }
    }
}