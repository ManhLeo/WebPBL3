
using PBL3_HotelManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net.Configuration;
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



            var bills = db.HoaDons.Where(m => m.IDKH == id).ToList();
            foreach (var bill in bills)
            {
                bill.IDKH = null;
            }


            db.KhachHangs.Remove(customer);

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

            var bookRoom = db.DatPhongs.FirstOrDefault(s => s.IDHD == id);
            if (bookRoom == null)
            {
                return HttpNotFound();
            }


            var bills = db.HoaDons.Find(id);
            if (bills == null)
            {
                return HttpNotFound();
            }

            db.DatPhongs.Remove(bookRoom);
            db.HoaDons.Remove(bills);
            
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
                DonGia = s.DonGia,
                SoLuongMax = s.SoLuongMax,
                Soluong = s.Soluong

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
                DiaChi = s.DiaChi
            }).ToList();

            // Trả về kết quả tìm kiếm dưới dạng JSON
            return Json(searchResult);
        }
        [HttpPost]
        public ActionResult SearchBills(string searchText)
        {
            // Tìm kiếm dịch vụ theo tên
            var searchResult = db.HoaDons.Where(s => s.KhachHang.HoTen.Contains(searchText)).Select(s => new {
                IDHD = s.IDHD,
                IDKH = s.IDKH,
                HoTen = s.KhachHang.HoTen,
                CCCD = s.KhachHang.CCCD,
                NgayNhan = s.DatPhongs.FirstOrDefault().NgayNhan,
                NgayTra = s.DatPhongs.FirstOrDefault().NgayTra,
                DonGia=s.DonGia,
                TrangThai = s.TrangThai
            }).ToList();

            // Trả về kết quả tìm kiếm dưới dạng JSON
            return Json(searchResult);
        }

        [HttpGet]
        public ActionResult GetRoomTypes()
        {
            var roomTypes = db.LoaiPhongs.Select(r => r.TenLoaiPhong).Distinct().ToList();
            return Json(roomTypes, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
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
                    ((d.NgayNhan <= fromDate && d.NgayTra >= fromDate) ||
                    (d.NgayNhan <= toDate && d.NgayTra >= toDate) ||
                    (d.NgayNhan >= fromDate && d.NgayTra <= toDate))));
                }
                else if (condition == "Bận")
                {
                    query = query.Where(p => db.DatPhongs.Any(d => d.IDPHG == p.IDPHG &&
                    ((d.NgayNhan <= fromDate && d.NgayTra >= fromDate) ||
                    (d.NgayNhan <= toDate && d.NgayTra >= toDate) ||
                    (d.NgayNhan >= fromDate && d.NgayTra <= toDate))));
                }
            }
            var searchResult = query.Select(p => new
            {
                p.IDPHG,
                p.TenPHG,
                p.LoaiPhong.TenLoaiPhong,
                p.LoaiPhong.DonGia,
                p.SoGiuong,
                p.TrangThai
            }).ToList();

            return Json(searchResult);
        }



        //==================Book====================//

        [HttpPost]
        public ActionResult BookRoom(BookViewModel model)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var newIDKH = GenerateNewCustomerId();
                        var newIDBooking = GenerateNewBookRoomId();
                        var newBills = GenerateNewBillId();

                        

                        var newCustomer = new KhachHang
                        {
                            IDKH = newIDKH,
                            HoTen = model.FullName,
                            CCCD = model.CCCD,
                            SDT = model.PhoneNumber,
                            Email = model.Email,
                            DiaChi = model.Address
                        };

                        var selectedRoom = FindAvailableRoom(model.RoomType, model.CheckInDate, model.CheckOutDate);
                        if (selectedRoom == null)
                        {
                            return Json(new { success = false, message = "Không tìm thấy phòng phù hợp." });
                        }

                        var bookingRoom = new DatPhong
                        {
                            IDDatPHG = newIDBooking,
                            IDHD = newBills,
                            IDPHG = selectedRoom.IDPHG,
                            NgayNhan = model.CheckInDate,
                            NgayTra = model.CheckOutDate,
                            DonGia = (selectedRoom.LoaiPhong.DonGia) * (model.CheckOutDate - model.CheckInDate).Days
                        };

                        selectedRoom.TrangThai = "Bận";

                        var bills = new HoaDon
                        {
                            IDHD = newBills,
                            IDKH = newIDKH,
                            DonGia = bookingRoom.DonGia,
                            TrangThai = "Chưa thanh toán"
                        };

                        db.HoaDons.Add(bills);
                        db.KhachHangs.Add(newCustomer);
                        db.DatPhongs.Add(bookingRoom);

                        

                        db.SaveChanges();
                        transaction.Commit();

                        return Json(new { success = true, message = "Thêm khách hàng và đặt phòng thành công." });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Dữ liệu không hợp lệ." });
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Json(new { success = false, message = "Lỗi khi thêm khách hàng và đặt phòng: " + ex.Message });
                }
            }
        }



        private Phong FindAvailableRoom(string roomType, DateTime checkInDate, DateTime checkOutDate)
        {
            var availableRooms = db.Phongs.Where(p => p.TrangThai == "Trống" && p.LoaiPhong.TenLoaiPhong == roomType).ToList();
            foreach (var room in availableRooms)
            {
                var bookings = db.DatPhongs.Where(b => b.IDPHG == room.IDPHG &&
                                                       (checkInDate < b.NgayTra && checkOutDate > b.NgayNhan)).ToList();
                if (bookings.Count == 0)
                {
                    return room;
                }
            }
            return null;
        }


        [HttpPost]
        public ActionResult BookService(BookViewModel model)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid && model.Email != null)
                    {
                        var newIDBooking = GenerateNewBookRoomId();

                        var kh = db.KhachHangs.FirstOrDefault(s => s.Email == model.Email);
                        if (kh != null)
                        {
                            var hd = db.HoaDons.FirstOrDefault(k => k.IDKH == kh.IDKH);
                            if (hd != null && model.SelectedServices != null && model.SelectedServices.Any())
                            {
                                foreach (var IDservice in model.SelectedServices)
                                {
                                    var newIDServiceBooking = GenerateNewBookServiceId();
                                    var selectedService = db.DichVus.FirstOrDefault(s => s.IDDV == IDservice);

                                    if (selectedService != null)
                                    {
                                        var bookingService = new DatDichVu
                                        {
                                            IDDatDV = newIDServiceBooking,
                                            IDHD = hd.IDHD,
                                            IDDV = IDservice,
                                            NgaySD = model.NgaySuDung,
                                            SoLuong = model.NumberOfService,
                                            DonGia = selectedService.DonGia * model.NumberOfService
                                        };
                                        selectedService.Soluong += model.NumberOfService;
                                        db.DatDichVus.Add(bookingService);
                                    }
                                }
                            }
                            db.SaveChanges();
                            transaction.Commit();

                            return Json(new { success = true, message = "Đặt dịch vụ thành công." });
                        }
                        else return Json(new { success = false, message = "Email không tồn tại" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Dữ liệu không hợp lệ." });
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Json(new { success = false, message = "Lỗi khi Đặt dịch vụ: " + ex.Message });
                }
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

        private string GenerateNewBookRoomId()
        {
            var lastBooking = db.DatPhongs.OrderByDescending(c => c.IDDatPHG).FirstOrDefault();
            if (lastBooking != null)
            {
                int newIdNumber = int.Parse(lastBooking.IDDatPHG.Substring(2)) + 1;
                return "DP" + newIdNumber.ToString("D2");
            }
            else
            {
                return "DP01";
            }
        }

        private string GenerateNewBookServiceId()
        {
            var lastBookingService = db.DatDichVus.OrderByDescending(c => c.IDDatDV).FirstOrDefault();
            if (lastBookingService != null)
            {
                int newIdNumber = int.Parse(lastBookingService.IDDatDV.Substring(3)) + 1;
                return "DDV" + newIdNumber.ToString("D2");
            }
            else
            {
                return "DDV01";
            }
        }

       
        //===================Add==================//

        

        [HttpPost]
        public ActionResult AddService(ServiceViewModel model)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var newIDDV = GenerateNewServiceId();

                        var newService = new DichVu
                        {
                            IDDV = newIDDV,
                            TenDV = model.TenDV,
                            DonGia = model.DonGia,
                            SoLuongMax = model.SoLuongMax,
                            Soluong = 0
                            
                        };

                        
                        db.DichVus.Add(newService);
                        db.SaveChanges();
                        transaction.Commit();

                        return Json(new { success = true, message = "Thêm dịch vụ thành công." });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Dữ liệu không hợp lệ." });
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Json(new { success = false, message = "Lỗi khi thêm dịch vụ: " + ex.Message });
                }
            }
        }

        private string GenerateNewServiceId()
        {
            var lastService = db.DichVus.OrderByDescending(c => c.IDDV).FirstOrDefault();
            if (lastService != null)
            {
                int newIdNumber = int.Parse(lastService.IDDV.Substring(2)) + 1;
                return "DV" + newIdNumber.ToString("D2");
            }
            else
            {
                return "DV01";
            }
        }


        

        private string GenerateNewBillId()
        {
            var lastBill = db.HoaDons.OrderByDescending(c => c.IDHD).FirstOrDefault();
            if (lastBill != null)
            {
                int newIdNumber = int.Parse(lastBill.IDHD.Substring(2)) + 1;
                return "HD" + newIdNumber.ToString("D2");
            }
            else
            {
                return "HD01";
            }
        }



        public ActionResult CreateBill(string roomId)
        {
            try
            {
                // Tìm phòng dựa trên roomId
                var room = db.Phongs.FirstOrDefault(r => r.IDPHG == roomId);
                if (room != null)
                {
                    // Tìm thông tin đặt phòng liên quan đến phòng này
                    var booking = db.DatPhongs.FirstOrDefault(b => b.IDPHG == roomId);
                    if (booking != null)
                    {
                        var HD = db.HoaDons.FirstOrDefault(b => b.IDHD == booking.IDHD);
                        var HDDV = db.DatDichVus.FirstOrDefault(b => b.IDHD == HD.IDHD);

                        HD.TrangThai = "Đã thanh toán";
                        if(HDDV != null) HD.DonGia += HDDV.DonGia;

                        var dv = db.DichVus.FirstOrDefault(s=>s.IDDV == HDDV.IDDV);
                        if (dv != null)
                        {
                                dv.Soluong -= HDDV.SoLuong;
                        }
                        // Cập nhật trạng thái của phòng thành trống
                        room.TrangThai = "Trống";



                        db.SaveChanges();

                        return Json(new { success = true, message = "Tạo hóa đơn thành công." });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Không tìm thấy thông tin đặt phòng." });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Không tìm thấy phòng." });
                }
            }
            catch (Exception ex)
            {
                // Log chi tiết lỗi inner exception nếu có
                var innerExceptionMessage = ex.InnerException?.Message ?? "No inner exception";
                return Json(new { success = false, message = "Lỗi khi tạo hóa đơn: " + ex.Message + " | Inner Exception: " + innerExceptionMessage });
            }
        }


        //============================Edit

        [HttpGet]
        public JsonResult GetRoomData(string id)
        {
            var room = db.Phongs.Find(id);
            if (room == null)
            {
                return Json(new { success = false, message = "Không tìm thấy phòng." }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, IDPHG = room.IDPHG, TenPHG = room.TenPHG, LoaiPhong = room.LoaiPhong.TenLoaiPhong, SoGiuong = room.SoGiuong }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCustomerData(string id)
        {
            var customer = db.KhachHangs.Find(id);
            if (customer == null)
            {
                return Json(new { success = false, message = "Không tìm thấy khách hàng." }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, IDKH = customer.IDKH, HoTen = customer.HoTen, CCCD = customer.CCCD, SDT = customer.SDT, DiaChi = customer.DiaChi }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetServiceData(string id)
        {
            var service = db.DichVus.Find(id);
            if (service == null)
            {
                return Json(new { success = false, message = "Không tìm thấy dịch vụ." }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, IDDV = service.IDDV, TenDV = service.TenDV, DonGia = service.DonGia, SoLuongMax = service.SoLuongMax }, JsonRequestBehavior.AllowGet);
        }


        //============================//

        [HttpPost]
        public ActionResult EditRoom(Phong phong)
        {
            try
            {
                // Kiểm tra xem model có hợp lệ không
                if (ModelState.IsValid)
                {
                    var room = db.Phongs.Find(phong.IDPHG);
                    if (room != null)
                    {
                        // Cập nhật thông tin phòng từ dữ liệu gửi từ form
                        room.TenPHG = phong.TenPHG;
                        room.SoGiuong = phong.SoGiuong;

                        // Kiểm tra xem phong.LoaiPhong có null hay không trước khi truy cập vào thuộc tính TenLoaiPhong
                        if (phong.LoaiPhong != null)
                        {
                            var loaiPhong = db.LoaiPhongs.FirstOrDefault(lp => lp.TenLoaiPhong == phong.LoaiPhong.TenLoaiPhong);
                            if (loaiPhong != null)
                            {
                                room.IDLoaiPhong = loaiPhong.IDLoaiPhong;
                            }
                        }

                        // Lưu thay đổi vào cơ sở dữ liệu
                        db.SaveChanges();

                        return Json(new { success = true, message = "Chỉnh sửa thông tin phòng thành công." });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Không tìm thấy phòng cần chỉnh sửa." });
                    }
                }
                else
                {
                    // Trả về thông báo lỗi nếu dữ liệu không hợp lệ
                    return Json(new { success = false, message = "Dữ liệu không hợp lệ." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Đã xảy ra lỗi khi chỉnh sửa thông tin phòng: " + ex.Message });
            }
        }


        [HttpPost]
        public ActionResult EditCustomer(KhachHang khachhang)
        {
            try
            {
                // Kiểm tra xem model có hợp lệ không
                if (ModelState.IsValid)
                {
                    var customer = db.KhachHangs.Find(khachhang.IDKH);
                    if (customer != null)
                    {
                        // Cập nhật thông tin phòng từ dữ liệu gửi từ form
                        customer.HoTen = khachhang.HoTen;
                        customer.CCCD = khachhang.CCCD;
                        customer.SDT = khachhang.SDT;
                        customer.DiaChi = khachhang.DiaChi;

                        

                        // Lưu thay đổi vào cơ sở dữ liệu
                        db.SaveChanges();

                        return Json(new { success = true, message = "Chỉnh sửa thông tin khách hàng thành công." });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Không tìm thấy khách hàng cần chỉnh sửa." });
                    }
                }
                else
                {
                    // Trả về thông báo lỗi nếu dữ liệu không hợp lệ
                    return Json(new { success = false, message = "Dữ liệu không hợp lệ." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Đã xảy ra lỗi khi chỉnh sửa thông tin khách hàng: " + ex.Message });
            }
        }


        [HttpPost]
        public ActionResult EditService(DichVu dichvu)
        {
            try
            {
                // Kiểm tra xem model có hợp lệ không
                if (ModelState.IsValid)
                {
                    var service = db.DichVus.Find(dichvu.IDDV);
                    if (service != null)
                    {
                        // Cập nhật thông tin phòng từ dữ liệu gửi từ form
                        service.TenDV = dichvu.TenDV;
                        service.DonGia = dichvu.DonGia;
                        service.SoLuongMax = dichvu.SoLuongMax;



                        // Lưu thay đổi vào cơ sở dữ liệu
                        db.SaveChanges();

                        return Json(new { success = true, message = "Chỉnh sửa thông tin dịch vụ thành công." });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Không tìm thấy dịch vụ cần chỉnh sửa." });
                    }
                }
                else
                {
                    // Trả về thông báo lỗi nếu dữ liệu không hợp lệ
                    return Json(new { success = false, message = "Dữ liệu không hợp lệ." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Đã xảy ra lỗi khi chỉnh sửa thông tin dịch vụ: " + ex.Message });
            }
        }


    }
}

