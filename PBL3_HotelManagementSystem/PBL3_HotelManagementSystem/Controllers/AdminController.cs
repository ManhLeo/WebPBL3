using PBL3_HotelManagementSystem.Helpers;
using PBL3_HotelManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using static System.Data.Entity.Infrastructure.Design.Executor;

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

            var viewModel = new List<IndexViewModel>(){
                new IndexViewModel
                {
                    Services = services,
                    Customers = customers,
                    Rooms = rooms,
                    Bills = bills,
                    NewCustomer = new CustomerViewModel()
                }
            };

            return View(viewModel);
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

        [HttpPost]
        public ActionResult SearchRooms(string roomType, string condition, DateTime? fromDate, DateTime? toDate)
        {
            var query = db.Phongs.AsQueryable();

            if (!string.IsNullOrEmpty(roomType) && roomType != "Tất cả")
            {
                query = query.Where(p => p.LoaiPhong.TenLoaiPhong == roomType);
            }

            if (!string.IsNullOrEmpty(condition) && condition != "Tất cả")
            {
                query = query.Where(p => p.TrangThai == condition);
            }
            if (!string.IsNullOrEmpty(condition) && condition != "Tất cả")
            {
                if (condition == "Trống")
                {
                    query = query.Where(p => !db.DatPhongs.Any(d => d.IDPHG == p.IDPHG &&
                    ((d.NgayDat <= fromDate && d.NgayTra >= fromDate) ||
                    (d.NgayDat <= toDate && d.NgayTra >= toDate) ||
                    (d.NgayDat >= fromDate && d.NgayTra <= toDate))));
                }
                else if (condition == "Bận")
                {
                    query = query.Where(p => db.DatPhongs.Any(d => d.IDPHG == p.IDPHG &&
                    ((d.NgayDat <= fromDate && d.NgayTra >= fromDate) ||
                    (d.NgayDat <= toDate && d.NgayTra >= toDate) ||
                    (d.NgayDat >= fromDate && d.NgayTra <= toDate))));
                }
            }
            var searchResult = query.Select(p => new
            {
                p.IDPHG,
                p.TenPHG,
                p.LoaiPhong.TenLoaiPhong,
                p.LoaiPhong.DonGia,
                p.LoaiPhong.SoGiuong,
                p.LoaiPhong.SoNguoi,
                p.TrangThai
            }).ToList();

            return Json(searchResult);
        }



        //==================Add====================//

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCustomer(CustomerViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingUser = db.Accounts.FirstOrDefault(u => u.Email == model.Email);
                    if (existingUser != null)
                    {
                        ViewBag.ErrorMessage = "Email đã tồn tại.";
                        return RedirectToAction("Index");
                    }

                    // Tạo ID khách hàng mới
                    var newIDKH = GenerateNewCustomerId();

                    var newUser = new Account
                    {
                        IDAccount = newIDKH,
                        UserName = model.FullName,
                        Email = model.Email,
                        Pass = model.Password, // Sử dụng mật khẩu chưa hash
                        PhanQuyen = "Khách Hàng"
                    };

                    db.Accounts.Add(newUser);

                    var newCustomer = new KhachHang
                    {
                        IDKH = newUser.IDAccount,
                        HoTen = model.FullName,
                        CCCD = model.CCCD,
                        SDT = model.PhoneNumber,
                        Email = model.Email,
                        GioiTinh = model.Gender,
                        DiaChi = model.Address
                    };

                    db.KhachHangs.Add(newCustomer);
                    db.SaveChanges();

                    // Chuyển hướng về trang quản lý sau khi thêm thành công
                    return RedirectToAction("Index");
                }

                ViewBag.ErrorMessage = "Vui lòng kiểm tra lại thông tin.";
                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        // Hiển thị lỗi cụ thể cho từng thuộc tính
                        Console.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }

                // Xử lý lỗi ở đây hoặc trả về một view với thông báo lỗi phù hợp
                ViewBag.ErrorMessage = "Lỗi khi kiểm tra dữ liệu.";
                return RedirectToAction("Index");
            }
        }

        private string GenerateNewCustomerId()
        {
            var lastCustomer = db.KhachHangs.OrderByDescending(c => c.IDKH).FirstOrDefault();
            if (lastCustomer != null)
            {
                int newIdNumber = int.Parse(lastCustomer.IDKH.Substring(2)) + 1;
                return "KH" + newIdNumber.ToString("D2");
            }
            else
            {
                return "KH01";
            }
        }


    }
}