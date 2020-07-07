namespace HotelManagementSystem.DAL.Entity
{
    public class ConfirmationEntity
    {
        public int Id { get; set; }
        public int? RoomId { get; set; }
        public int CustomerRequestId { get; set; }
        public string ManagerComment { get; set; }
        public virtual RoomEntity Room { get; set; }
        public virtual CustomerRequestEntity CustomerRequest { get; set; }
    }
}