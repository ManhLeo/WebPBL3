using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PBL3_HotelManagementSystem.Models;

namespace PBL3_HotelManagementSystem.Controllers
{
    public class HoaDonChiTietsController : Controller
    {
        private PBL3_5Entities1 db = new PBL3_5Entities1();

        // GET: HoaDonChiTiets
        public ActionResult Index()
        {
            var hoaDonChiTiets = db.HoaDonChiTiets.Include(h => h.DatDichVuChiTiet).Include(h => h.HoaDon);
            return View(hoaDonChiTiets.ToList());
        }

        // GET: HoaDonChiTiets/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDonChiTiet hoaDonChiTiet = db.HoaDonChiTiets.Find(id);
            if (hoaDonChiTiet == null)
            {
                return HttpNotFound();
            }
            return View(hoaDonChiTiet);
        }

        // GET: HoaDonChiTiets/Create
        public ActionResult Create()
        {
            ViewBag.IDDatDVChiTiet = new SelectList(db.DatDichVuChiTiets, "IDDatDVChiTiet", "IDDatDV");
            ViewBag.IDHD = new SelectList(db.HoaDons, "IDHD", "IDKH");
            return View();
        }

        // POST: HoaDonChiTiets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDHDChiTiet,IDHD,IDDatDVChiTiet,NgayDat,NgayTra")] HoaDonChiTiet hoaDonChiTiet)
        {
            if (ModelState.IsValid)
            {
                db.HoaDonChiTiets.Add(hoaDonChiTiet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDDatDVChiTiet = new SelectList(db.DatDichVuChiTiets, "IDDatDVChiTiet", "IDDatDV", hoaDonChiTiet.IDDatDVChiTiet);
            ViewBag.IDHD = new SelectList(db.HoaDons, "IDHD", "IDKH", hoaDonChiTiet.IDHD);
            return View(hoaDonChiTiet);
        }

        // GET: HoaDonChiTiets/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDonChiTiet hoaDonChiTiet = db.HoaDonChiTiets.Find(id);
            if (hoaDonChiTiet == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDDatDVChiTiet = new SelectList(db.DatDichVuChiTiets, "IDDatDVChiTiet", "IDDatDV", hoaDonChiTiet.IDDatDVChiTiet);
            ViewBag.IDHD = new SelectList(db.HoaDons, "IDHD", "IDKH", hoaDonChiTiet.IDHD);
            return View(hoaDonChiTiet);
        }

        // POST: HoaDonChiTiets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDHDChiTiet,IDHD,IDDatDVChiTiet,NgayDat,NgayTra")] HoaDonChiTiet hoaDonChiTiet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoaDonChiTiet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDDatDVChiTiet = new SelectList(db.DatDichVuChiTiets, "IDDatDVChiTiet", "IDDatDV", hoaDonChiTiet.IDDatDVChiTiet);
            ViewBag.IDHD = new SelectList(db.HoaDons, "IDHD", "IDKH", hoaDonChiTiet.IDHD);
            return View(hoaDonChiTiet);
        }

        // GET: HoaDonChiTiets/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDonChiTiet hoaDonChiTiet = db.HoaDonChiTiets.Find(id);
            if (hoaDonChiTiet == null)
            {
                return HttpNotFound();
            }
            return View(hoaDonChiTiet);
        }

        // POST: HoaDonChiTiets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HoaDonChiTiet hoaDonChiTiet = db.HoaDonChiTiets.Find(id);
            db.HoaDonChiTiets.Remove(hoaDonChiTiet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
