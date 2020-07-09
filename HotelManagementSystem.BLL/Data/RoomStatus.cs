using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.BLL.Data
{
    public class RoomStatus
    {
        public RoomStatus()
        {
        }

        public RoomStatus(int roomStatusId, string name)
        {
            Id = roomStatusId;
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
