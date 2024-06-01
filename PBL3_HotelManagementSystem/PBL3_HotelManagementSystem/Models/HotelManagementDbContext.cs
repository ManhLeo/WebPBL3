using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace PBL3_HotelManagementSystem.Models
{
    public class HotelManagementDbContext : DbContext
    {
        public HotelManagementDbContext() : base("name=HotelManagementDbContext") { }

        public DbSet<KhachHang> KhachHangs { get; set; }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<LoaiPhong> LoaiPhongs { get; set; }
        public DbSet<Phong> Phongs { get; set; }
        public DbSet<DatPhong> DatPhongs { get; set; }
        public DbSet<LoaiDV> LoaiDVs { get; set; }
        public DbSet<DichVu> DichVus { get; set; }
        public DbSet<DatDichVu> DatDichVus { get; set; }
        public DbSet<DatDichVuChiTiet> DatDichVuChiTiets { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<HoaDonChiTiet> HoaDonChiTiets { get; set; }
        public DbSet<CustomerViewModel> Customers { get; set; }
    }
}