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


        // POST: Admin/DeleteCustomer
        [HttpPost]
        public ActionResult DeleteCustomer(string customerId)
        {
            try
            {
                var customer = db.KhachHangs.Find(customerId);
                if (customer == null)
                {
                    return HttpNotFound();
                }

                db.KhachHangs.Remove(customer);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Xóa thông tin khách hàng thành công.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Đã xảy ra lỗi trong quá trình xóa thông tin khách hàng.";
                // Ghi log lỗi và xử lý lỗi khác (nếu cần)
            }

            return RedirectToAction("Index");
        }
    }
}