using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PBL3_HotelManagementSystem.Models
{
    public class IndexViewModel
    {

        public IEnumerable<DichVu> Services { get; set; }
        public IEnumerable<KhachHang> Customers { get; set; }
        public IEnumerable<Phong> Rooms { get; set; }
        public IEnumerable<HoaDon> Bills { get; set; }
        public IEnumerable<LoaiPhong> KindRooms { get; set; }
        public CustomerViewModel NewCustomer { get; set; }
        public IEnumerable<CustomerViewModel> Customer {get; set; }
    }
}