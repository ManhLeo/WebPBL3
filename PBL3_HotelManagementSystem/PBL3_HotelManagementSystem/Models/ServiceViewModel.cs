using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PBL3_HotelManagementSystem.Models
{
    public class ServiceViewModel
    {
        public string TenDV { get; set; }
        public int DonGia { get; set; }
        public int SoLuongMax { get; set; }
        public bool IsSelected { get; set; }
    }
}