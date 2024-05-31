using PBL3_HotelManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

//giao diện đặt dịch vụ, giao diện thông tin đặt phòng của khách hàng, giao diện sửa các thông tin, 

namespace PBL3_HotelManagementSystem.Controllers
{
    
    public class AdminController : Controller
    {
        private HotelManagementDbContext db = new HotelManagementDbContext();

        // GET: Admin
        public ActionResult Index()
        {
            var services = db.DichVus.ToList();
            var customers = db.KhachHangs.ToList();
            var rooms = db.Phongs.ToList();
            var bills = db.HoaDons.ToList(); 

            var viewModelList = new List<IndexViewModel>(){
                new IndexViewModel
                {
                    Services = services,
                    Customers = customers,
                    Rooms = rooms,
                    Bills = bills
                }
            };

            return View(viewModelList);
        }


        //===========DELETE===========//
        // POST: Admin/DeleteRoom
        [HttpPost]
        public ActionResult DeleteRoom(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var rooms = db.Phongs.Find(id);
            if (rooms == null)
            {
                return HttpNotFound();
            }

            db.Phongs.Remove(rooms);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // POST: Admin/DeleteCustomer
        [HttpPost]
        public ActionResult DeleteCustomer(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var customer = db.KhachHangs.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            var account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }


            db.KhachHangs.Remove(customer);
            db.Accounts.Remove(account);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // POST: Admin/DeleteService
        [HttpPost]
        public ActionResult DeleteService(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var services = db.DichVus.Find(id);
            if (services == null)
            {
                return HttpNotFound();
            }

            db.DichVus.Remove(services);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // POST: Admin/DeleteBill
        [HttpPost]
        public ActionResult DeleteBill(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var bills = db.HoaDons.Find(id);
            if (bills == null)
            {
                return HttpNotFound();
            }

            var ifbills = db.HoaDonChiTiets.Find(id);
            if (ifbills == null)
            {
                return HttpNotFound();
            }

            var bookSer = db.DatDichVus.Find(id);
            if (bookSer == null)
            {
                return HttpNotFound();
            }

            var ifbookSer = db.DatDichVuChiTiets.Find(id);
            if (ifbookSer == null)
            {
                return HttpNotFound();
            }

            db.HoaDons.Remove(bills);
            db.HoaDonChiTiets.Remove(ifbills);
            db.DatDichVus.Remove(bookSer);
            db.DatDichVuChiTiets.Remove(ifbookSer);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        //=====================Find=====================//
        [HttpPost]
        public ActionResult SearchServices(string searchText)
        {
            // Tìm kiếm dịch vụ theo tên
            var searchResult = db.DichVus.Where(s => s.TenDV.Contains(searchText)).Select(s => new {
                IDDV = s.IDDV,
                TenDV = s.TenDV,
                LoaiDV = s.LoaiDV.TenLoaiDV,
                DonGia = s.LoaiDV.DonGia
            }).ToList();

            // Trả về kết quả tìm kiếm dưới dạng JSON
            return Json(searchResult);
        }
        [HttpPost]
        public ActionResult SearchCustomers(string searchText)
        {
            // Tìm kiếm dịch vụ theo tên
            var searchResult = db.KhachHangs.Where(s => s.HoTen.Contains(searchText)).Select(s => new {
                IDKH = s.IDKH,
                HoTen = s.HoTen,    
                CCCD = s.CCCD,  
                SDT = s.SDT,        
                Email = s.Email,
                GioiTinh = s.GioiTinh,  
                DiaChi = s.DiaChi
            }).ToList();

            // Trả về kết quả tìm kiếm dưới dạng JSON
            return Json(searchResult);
        }


        public ActionResult GetRoomTypes()
        {
            var roomTypes = db.LoaiPhongs.Select(r => r.TenLoaiPhong).Distinct().ToList();
            return Json(roomTypes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRoomStatuses()
        {
            var roomStatuses = db.Phongs.Select(r => r.TrangThai).Distinct().ToList();
            return Json(roomStatuses, JsonRequestBehavior.AllowGet);
        }

    }
}