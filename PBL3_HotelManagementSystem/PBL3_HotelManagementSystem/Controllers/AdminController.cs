using PBL3_HotelManagementSystem.Models;
using System;
using System.Collections.Generic;
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
            return View();
        }

        
    }
}