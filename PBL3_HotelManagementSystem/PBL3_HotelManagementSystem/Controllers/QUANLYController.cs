using PBL3_HotelManagementSystem.Models.BUS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PBL3_HotelManagementSystem.Controllers
{
    public class QUANLYController : Controller
    {
        // GET: QUANLY
        public ActionResult Index()
        {
            var db = new HotelBUS();
            return View(db);
        }

        // GET: QUANLY/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: QUANLY/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QUANLY/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: QUANLY/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: QUANLY/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: QUANLY/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: QUANLY/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
