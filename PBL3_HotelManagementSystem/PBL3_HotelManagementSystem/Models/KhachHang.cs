using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PBL3_HotelManagementSystem.Models
{
	public class KhachHang
    {
        [Key]
        public string IDKH { get; set; }
        public string HoTen { get; set; }
        public string CCCD { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public bool GioiTinh { get; set; }
        public string DiaChi { get; set; }
    }
}