using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models
{
    public class RoomTypeViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Room type")] public string Name { get; set; }
    }
}