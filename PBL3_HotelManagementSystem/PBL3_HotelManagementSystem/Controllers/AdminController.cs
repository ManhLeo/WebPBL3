using PBL3_HotelManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;


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
    }
}