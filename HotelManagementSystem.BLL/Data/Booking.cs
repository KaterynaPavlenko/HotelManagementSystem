using System;

namespace HotelManagementSystem.BLL.Data
{
    public class Booking
    {
      
        public int Id { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public decimal TotalPrice { get; set; }
        public string HotelUserId { get; set; }
        public int RoomId { get; set; }
        public bool Payment { get; set; }
        public bool IsDeleted { get; set; }
    }
}