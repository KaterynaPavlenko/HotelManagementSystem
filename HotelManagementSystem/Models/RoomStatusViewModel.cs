using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models
{
    public class RoomStatusViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Room status")] public string Name { get; set; }
    }
}