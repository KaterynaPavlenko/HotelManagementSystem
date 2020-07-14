using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HotelManagementSystem.BLL.Data;
using HotelManagementSystem.DAL.Entity;

namespace HotelManagementSystem.Models
{
    public class CustomerRequestViewModel
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

        [Display(Name = "Sleeps")]
        [Range(1, 10, ErrorMessage = "Invalid value. Valid values are from 1 to 10")]
        public int Sleeps { get; set; }

        [Display(Name = "Room type")] public int RoomTypeId { get; set; }

        [Display(Name = "Room type")] public string RoomType { get; set; }

        public string HotelUserId { get; set; }
        public int CustomerRequestStatusId { get; set; }

        [Display(Name = "Request Status")] public string CustomerRequestStatus { get; set; }
    }

    public class CustomerRequestListViewModel
    {
        public IEnumerable<CustomerRequest> CustomerRequests { get; set; }
        public IEnumerable<HotelUserEntity> Users { get; set; }
        public IEnumerable<RoomType> RoomTypes { get; set; }
    }
}