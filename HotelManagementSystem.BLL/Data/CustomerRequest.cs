﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.BLL.Data
{
	public class CustomerRequest
    {
        public CustomerRequest()
        {
        }

        public int Id { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int Sleeps { get; set; }
        public string CustomerRequestStatus { get; set; }
        public string RoomType { get; set; }
        public int RoomTypeId { get; set; }
        public int CustomerRequestStatusId { get; set; }
        public string HotelUserId { get; set; }
    }
}
