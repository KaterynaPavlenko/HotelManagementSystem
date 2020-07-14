using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace HotelManagementSystem.Models
{
    public class RoomViewModel
    {
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a room description")]
        [Display(Name = "Room Description")]
        public string RoomDescription { get; set; }

        [Required(ErrorMessage = "Please enter a room number")]
        [Display(Name = "Room number")]
        public string RoomNumber { get; set; }

        [Required]
        [Display(Name = "Room Price For One Night")]
        public decimal RoomPriceForOneNight { get; set; }

        [Required(ErrorMessage = "Please enter a sleeps")]
        [Display(Name = "Sleeps")]
        [Range(1, 10, ErrorMessage = "Invalid value. Valid values are from 1 to 10")]
        public int Sleeps { get; set; }

        [Display(Name = "Room photo")] public string RoomImage { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

        [Display(Name = "Room type")] public string RoomType { get; set; }

        [Display(Name = "Room status")] public string RoomStatus { get; set; }

        public int RoomTypeId { get; set; }
        public int RoomStatusId { get; set; }
    }
}