using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.BLL.Data
{
    public class Confirmation
    {
        public Confirmation()
        {
        }

        public Confirmation(int confirmationId, int? roomId, int customerRequestId)
        {
            Id = confirmationId;
            RoomId = roomId;
            CustomerRequestId = customerRequestId;
        }

        public int Id { get; set; }
        public int? RoomId { get; set; }
        public int CustomerRequestId { get; set; }
    }
}
