using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PBL3_HotelManagementSystem.Models
{
    public class BookViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string CCCD { get; set; }
        public string PhoneNumber { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }
        public string RoomType { get; set; }
        public int NumberOfPeople { get; set; }
        public List<string> SelectedServices { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
