
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

                        var newUser = new Account
                        {
                            IDAccount = newIDKH,
                            UserName = model.FullName,
                            Email = model.Email,
                            Pass = null, // Hash mật khẩu trước khi lưu
                            PhanQuyen = "Khách Hàng"
                        };

                        db.Accounts.Add(newUser);

                        var newCustomer = new KhachHang
                        {
                            IDKH = newIDKH,
                            HoTen = model.FullName,
                            CCCD = model.CCCD,
                            SDT = model.PhoneNumber,
                            Email = model.Email,
                            GioiTinh = model.Gender,
                            DiaChi = model.Address
                        };

                        var selectedRoom = FindAvailableRoom(model.RoomType, model.CheckInDate, model.CheckOutDate);
                        if (selectedRoom == null)
                        {
                            return Json(new { success = false, message = "Không tìm thấy phòng phù hợp." });
                        }

                        var bookingRoom = new DatPhong
                        {
                            IDDatPhong = newIDBooking,
                            IDKH = newIDKH,
                            IDPHG = selectedRoom.IDPHG,
                            NgayDat = model.CheckInDate,
                            NgayTra = model.CheckOutDate,
                            SoNgayThue = (model.CheckOutDate - model.CheckInDate).Days,
                            TrangThai = "Đã đặt"
                        };

                        selectedRoom.TrangThai = "Bận";

                        db.KhachHangs.Add(newCustomer);
                        db.DatPhongs.Add(bookingRoom);

                        // Add selected services if any
                        if (model.SelectedServices != null && model.SelectedServices.Any())
                        {
                            foreach (var IDservice in model.SelectedServices)
                            {
                                var newIDServiceBooking = GenerateNewBookServiceId();

                                var bookingService = new DatDichVu
                                {
                                    IDDatDV = newIDServiceBooking,
                                    IDKH = newIDKH,
                                    IDDV = IDservice,
                                    NgaySuDung = model.CheckInDate
                                };
                                db.DatDichVus.Add(bookingService);

                                // Add details to DatDichVuChiTiet
                                var serviceDetails = new DatDichVuChiTiet
                                {
                                    IDDatDVChiTiet = GenerateNewBookServiceDetailId(),
                                    IDDatDV = newIDServiceBooking,
                                    SoLuong = model.NumberOfPeople,
                                    GiaTien = CalculateServicePrice(IDservice, model.NumberOfPeople)
                                };

                                db.DatDichVuChiTiets.Add(serviceDetails);
                            }
                        }

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

        public double CalculateServicePrice(string serviceId, int numberOfPeople)
        {
            // Tìm dịch vụ dựa trên ID
            var service = db.DichVus.FirstOrDefault(s => s.IDDV == serviceId);

            if (service != null)
            {
                // Lấy loại dịch vụ của dịch vụ
                var serviceType = service.LoaiDV;

                // Kiểm tra xem có loại dịch vụ không
                if (serviceType != null)
                {
                    // Lấy giá dịch vụ từ loại dịch vụ
                    double servicePrice = serviceType.DonGia ?? 0;

                    // Tính toán giá dịch vụ dựa trên số người
                    if (serviceType.SoNguoi.HasValue)
                    {
                        // Nếu số người vượt quá số người quy định của loại dịch vụ, tính giá theo số người vượt quá
                        int additionalPeople = Math.Max(0, numberOfPeople - serviceType.SoNguoi.Value);
                        servicePrice += additionalPeople * 0.5; // Giả sử giá cho mỗi người vượt quá là 0.5 đơn vị tiền tệ
                    }

                    return servicePrice;
                }
            }

            // Trả về 0 nếu không tìm thấy dịch vụ hoặc loại dịch vụ
            return 0;
        }


        private Phong FindAvailableRoom(string roomType, DateTime checkInDate, DateTime checkOutDate)
        {
            var availableRooms = db.Phongs.Where(p => p.TrangThai == "Trống" && p.LoaiPhong.TenLoaiPhong == roomType).ToList();
            foreach (var room in availableRooms)
            {
                var bookings = db.DatPhongs.Where(b => b.IDPHG == room.IDPHG &&
                                                       (checkInDate < b.NgayTra && checkOutDate > b.NgayDat)).ToList();
                if (bookings.Count == 0)
                {
                    return room;
                }
            }
            return null;
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
            var lastBooking = db.DatPhongs.OrderByDescending(c => c.IDDatPhong).FirstOrDefault();
            if (lastBooking != null)
            {
                int newIdNumber = int.Parse(lastBooking.IDDatPhong.Substring(2)) + 1;
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

        private string GenerateNewBookServiceDetailId()
        {
            var lastBookingServiceDetail = db.DatDichVuChiTiets.OrderByDescending(c => c.IDDatDVChiTiet).FirstOrDefault();
            if (lastBookingServiceDetail != null)
            {
                int newIdNumber = int.Parse(lastBookingServiceDetail.IDDatDVChiTiet.Substring(5)) + 1;
                return "DDVCT" + newIdNumber.ToString("D2");
            }
            else
            {
                return "DDVCT01";
            }
        }

        //===================Add==================//

        [HttpGet]
        public ActionResult GetServiceTypes()
        {
            var serviceTypes = db.LoaiDVs.Select(r => r.TenLoaiDV).Distinct().ToList();
            return Json(serviceTypes, JsonRequestBehavior.AllowGet);
        }

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

                        var tenloaiDV = model.TenLoaiDV;
                        var loaiDV = db.LoaiDVs.FirstOrDefault(l => l.TenLoaiDV == tenloaiDV);

                        var newService = new DichVu
                        {
                            IDDV = newIDDV,
                            TenDV = model.TenDV,
                            IDLoaiDV = loaiDV.IDLoaiDV // Gán ID của loại dịch vụ đã tìm được
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
    }
}