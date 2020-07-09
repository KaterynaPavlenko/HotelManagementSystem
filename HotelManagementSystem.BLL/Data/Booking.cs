using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.BLL.Data
{
    public class Booking
    {
        public Booking()
        {
        }

        public Booking(int bookingId, DateTime dateFrom, DateTime dateTo, decimal totalPrice, int roomId,
            string hotelUserId, bool payment)
        {
            Id = bookingId;
            DateFrom = dateFrom;
            DateTo = dateTo;
            RoomId = roomId;
            HotelUserId = hotelUserId;
            TotalPrice = totalPrice;
            Payment = payment;
        }

        public int Id { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public decimal TotalPrice { get; set; }
        public string HotelUserId { get; set; }
        public int RoomId { get; set; }
        public bool Payment { get; set; }
    }
}
