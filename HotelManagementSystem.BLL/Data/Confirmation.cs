namespace HotelManagementSystem.BLL.Data
{
    public class Confirmation
    {
    
       
        public int Id { get; set; }
        public int? RoomId { get; set; }
        public int CustomerRequestId { get; set; }
        public bool IsDeleted { get; set; }

    }
}