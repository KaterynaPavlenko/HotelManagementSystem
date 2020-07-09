namespace HotelManagementSystem.DAL.Entity
{
    public class ConfirmationEntity
    {
        public int Id { get; set; }
        public int? RoomId { get; set; }
        public int CustomerRequestId { get; set; }

        /// <summary>
        ///     Delete flag
        /// </summary>
        public bool IsDeleted { get; set; }

        public virtual RoomEntity Room { get; set; }
        public virtual CustomerRequestEntity CustomerRequest { get; set; }
    }
}