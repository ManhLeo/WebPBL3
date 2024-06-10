using PBL3_HotelManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Services.Description;

namespace PBL3_HotelManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private HotelManagementDbContext db = new HotelManagementDbContext();
        // GET: Home
        public ActionResult Index()
        {
            var services = db.DichVus.ToList();
            var rooms = db.Phongs.ToList();
            var viewModel = new List<HomeIndexViewModel>(){
                new HomeIndexViewModel
                {
                    Services = services,
                    Rooms = rooms
                }
            };
            return View(viewModel);
        }

        public ActionResult PageUser()
        {
            if (Session["UserID"] != null)
            {
                string userID = Session["UserID"].ToString();
                var user = db.KhachHangs.FirstOrDefault(k => k.IDKH == userID);
                if (user != null)
                {
                    var services = db.DichVus.ToList();
                    var rooms = db.Phongs.ToList();
                    var bills = db.HoaDons.FirstOrDefault(k=>k.IDKH == userID);
                    var bookr = db.DatPhongs.FirstOrDefault(k=>k.IDHD==userID);
                    var viewModel = new Home1IndexViewModel
                    {
                        User = user,
                        Services = services,
                        Rooms = rooms,
                        Bills = bills,
                        BookRooms = bookr
                    };
                    return View(new List<Home1IndexViewModel> { viewModel }); // Truyền vào một danh sách chứa một phần tử
                }
                else
                {
                    // Trường hợp không tìm thấy thông tin khách hàng
                    return HttpNotFound();
                }
            }
            else
            {
                // Chuyển hướng đến trang đăng nhập nếu người dùng chưa đăng nhập
                return RedirectToAction("Login", "Account");
            }
        }


        [HttpGet]
        public ActionResult GetRoomTypes()
        {
            var roomTypes = db.LoaiPhongs.Select(r => r.TenLoaiPhong).Distinct().ToList();
            return Json(roomTypes, JsonRequestBehavior.AllowGet);
        }

        //===================================================//
        [HttpPost]
        public ActionResult BookRoom1(BookViewModel model) //Dành cho người chưa đăng nhập
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        // Generate new IDs
                        var newIDKH = GenerateNewCustomerId();
                        var newIDHD = GenerateNewBillId();
                        var newIDBooking = GenerateNewBookRoomId();

                       

                        // Create new customer
                        var newCustomer = new KhachHang
                        {
                            IDKH = newIDKH,
                            HoTen = model.FullName,
                            CCCD = model.CCCD,
                            SDT = model.PhoneNumber,
                            Email = model.Email,
                            DiaChi = model.Address
                        };
                        db.KhachHangs.Add(newCustomer);



                        
                        // Find available room
                        var selectedRoom = FindAvailableRoom(model.RoomType, model.CheckInDate, model.CheckOutDate);
                        if (selectedRoom == null)
                        {
                            return Json(new { success = false, message = "Không tìm thấy phòng phù hợp." });
                        }

                        var songay = (model.CheckOutDate - model.CheckInDate).Days;
                        // Create new booking
                        var bookingRoom = new DatPhong
                        {
                            IDDatPHG = newIDBooking,
                            IDHD = newIDHD,
                            IDPHG = selectedRoom.IDPHG,
                            NgayNhan = model.CheckInDate,
                            NgayTra = model.CheckOutDate,
                            DonGia = selectedRoom.LoaiPhong.DonGia * songay
                        };
                        selectedRoom.TrangThai = "Bận";
                        db.DatPhongs.Add(bookingRoom);

                        var newBills = new HoaDon
                        {
                            IDHD = newIDHD,
                            IDKH = newIDKH,
                            DonGia = bookingRoom.DonGia,
                            TrangThai = "Chưa thanh toán"
                        };
                        db.HoaDons.Add(newBills);

                        // Save changes and commit transaction
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


        public ActionResult BookRoom2(BookViewModel model) //Dành cho người đã đăng nhập
        {
            if (Session["UserID"] != null)
            {
                string userID = Session["UserID"].ToString();
                var user = db.KhachHangs.FirstOrDefault(k => k.IDKH == userID);
                if (user != null)
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            // Generate new IDs
                            var newIDKH = user.IDKH; // Sử dụng ID của khách hàng đăng nhập
                            var newIDHD = GenerateNewBillId();
                            var newIDBooking = GenerateNewBookRoomId();

                            // Find available room
                            var selectedRoom = FindAvailableRoom(model.RoomType, model.CheckInDate, model.CheckOutDate);
                            if (selectedRoom == null)
                            {
                                return Json(new { success = false, message = "Không tìm thấy phòng phù hợp." });
                            }
                            var songay = (model.CheckOutDate-model.CheckInDate).Days;
                            // Create new booking
                            var bookingRoom = new DatPhong
                            {
                                IDDatPHG = newIDBooking,
                                IDHD = newIDHD,
                                IDPHG = selectedRoom.IDPHG,
                                NgayNhan = model.CheckInDate,
                                NgayTra = model.CheckOutDate,
                                DonGia = selectedRoom.LoaiPhong.DonGia * songay
                            };
                            selectedRoom.TrangThai = "Bận";
                            db.DatPhongs.Add(bookingRoom);

                            var newBills = new HoaDon
                            {
                                IDHD = newIDHD,
                                IDKH = newIDKH,
                                DonGia = bookingRoom.DonGia,
                                TrangThai = "Chưa thanh toán"
                            };
                            db.HoaDons.Add(newBills);

                            // Save changes and commit transaction
                            db.SaveChanges();
                            transaction.Commit();

                            return Json(new { success = true, message = "Đặt phòng thành công." });
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            return Json(new { success = false, message = "Lỗi khi đặt phòng: " + ex.Message });
                        }
                    }
                }
            }
            // Trường hợp không tìm thấy thông tin khách hàng hoặc người dùng chưa đăng nhập, chuyển hướng về trang đăng nhập
            return RedirectToAction("Login", "Account");
        }


        [HttpPost]
        public ActionResult BookService1(BookViewModel model) //Dành cho người chưa đăng nhập
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
                        else return Json(new { success = false, message = "Email không tồn tại trong hệ thống, vui lòng đăng nhập để đặt dịch vụ" });
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

        private string GenerateNewBillId()
        {
            var lastBill = db.HoaDons.OrderByDescending(c => c.IDHD).FirstOrDefault();
            if (lastBill != null)
            {
                int newIdNumber = int.Parse(lastBill.IDHD.Substring(2)) + 1;
                return "HD" + newIdNumber.ToString("D2");
            }
            return "HD01";
        }

        private string GenerateNewCustomerId()
        {
            var lastCustomer = db.KhachHangs.OrderByDescending(c => c.IDKH).FirstOrDefault();
            if (lastCustomer != null)
            {
                int newIdNumber = int.Parse(lastCustomer.IDKH.Substring(2)) + 1;
                return "KH" + newIdNumber.ToString("D2");
            }
            return "KH01";
        }

        private string GenerateNewBookRoomId()
        {
            var lastBooking = db.DatPhongs.OrderByDescending(c => c.IDDatPHG).FirstOrDefault();
            if (lastBooking != null)
            {
                int newIdNumber = int.Parse(lastBooking.IDDatPHG.Substring(2)) + 1;
                return "DP" + newIdNumber.ToString("D2");
            }
            return "DP01";
        }

        private string GenerateNewBookServiceId()
        {
            var lastBookingService = db.DatDichVus.OrderByDescending(c => c.IDDatDV).FirstOrDefault();
            if (lastBookingService != null)
            {
                int newIdNumber = int.Parse(lastBookingService.IDDatDV.Substring(3)) + 1;
                return "DDV" + newIdNumber.ToString("D2");
            }
            return "DDV01";
        }


        //=================================//

        [HttpPost]
        public ActionResult UpdatePersonalInfo(KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                // Lấy thông tin khách hàng từ database
                var existingKhachHang = db.KhachHangs.Find(khachHang.IDKH);

                if (existingKhachHang != null)
                {
                    // Cập nhật thông tin từ form
                    existingKhachHang.HoTen = khachHang.HoTen;
                    existingKhachHang.CCCD = khachHang.CCCD;
                    existingKhachHang.SDT = khachHang.SDT;
                    existingKhachHang.Email = khachHang.Email;
                    existingKhachHang.DiaChi = khachHang.DiaChi;

                    try
                    {
                        // Lưu các thay đổi vào cơ sở dữ liệu
                        db.SaveChanges();

                        // Trả về kết quả thành công
                        return Json(new { success = true });
                    }
                    catch (Exception ex)
                    {
                        // Xử lý ngoại lệ nếu có lỗi khi lưu vào cơ sở dữ liệu
                        return Json(new { success = false, message = ex.Message });
                    }
                }
                else
                {
                    // Không tìm thấy khách hàng cần cập nhật
                    return Json(new { success = false, message = "Không tìm thấy thông tin khách hàng" });
                }
            }
            else
            {
                // Trả về thông báo lỗi nếu dữ liệu không hợp lệ
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, message = "Validation failed", errors = errorMessages });
            }
        }



    }
}
