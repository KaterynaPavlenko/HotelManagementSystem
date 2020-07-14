using System.Collections.Generic;
using System.Web.Mvc;

namespace HotelManagementSystem.Models
{
    public class RoomsListViewModel
    {
        public IEnumerable<RoomViewModel> Rooms { get; set; }
        public SelectList SleepsPricesSort { get; set; }
        public SelectList RoomTypes { get; set; }
        public SelectList Statuses { get; set; }
    }
}