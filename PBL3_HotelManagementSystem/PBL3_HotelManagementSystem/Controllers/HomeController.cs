using PBL3_HotelManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PBL3_HotelManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private HotelManagementDbContext db = new HotelManagementDbContext();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // GET: PageUser
        public ActionResult PageUser()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetCustomerInfo(int id)
        {
            var customer = db.KhachHangs.Find(id);
            if (customer != null)
            {
                return Json(new
                {
                    success = true,
                    customer = new
                    {
                        FullName = customer.HoTen,
                        CCCD = customer.CCCD,
                        PhoneNumber = customer.SDT,
                        Email = customer.Email,
                        Gender = customer.GioiTinh,
                        Address = customer.DiaChi
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "Khách hàng không tồn tại" }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}
