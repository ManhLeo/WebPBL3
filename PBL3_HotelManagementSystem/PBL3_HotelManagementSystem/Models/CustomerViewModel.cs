using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PBL3_HotelManagementSystem.Models
{
    public class CustomerViewModel
    {
        public string FullName { get; set; }
        public string CCCD { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool? Gender { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
    }
}