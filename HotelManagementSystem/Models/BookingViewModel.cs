using System;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models
{
    public class BookingViewModel
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Check In date is required")]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Arrival date")]
        public DateTime DateFrom { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Check Out date is required")]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Departure date")]
        public DateTime DateTo { get; set; }

        [Display(Name = "Full price")] public decimal TotalPrice { get; set; }

        public string HotelUserId { get; set; }

        [StringLength(30, MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(30, MinimumLength = 3)]
        public string LastName { get; set; }

        [Display(Name = "Email")] public string Email { get; set; }

        public int RoomId { get; set; }

        [Display(Name = "Room Number")] public string RoomNumber { get; set; }

        [Display(Name = "Payment status")] public bool Payment { get; set; }
    }
}