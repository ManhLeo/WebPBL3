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
    public class DatDichVusController : Controller
    {
        private PBL3_5Entities1 db = new PBL3_5Entities1();

        // GET: DatDichVus
        public ActionResult Index()
        {
            var datDichVus = db.DatDichVus.Include(d => d.DichVu).Include(d => d.KhachHang);
            return View(datDichVus.ToList());
        }

        // GET: DatDichVus/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatDichVu datDichVu = db.DatDichVus.Find(id);
            if (datDichVu == null)
            {
                return HttpNotFound();
            }
            return View(datDichVu);
        }

        // GET: DatDichVus/Create
        public ActionResult Create()
        {
            ViewBag.IDDV = new SelectList(db.DichVus, "IDDV", "TenDV");
            ViewBag.IDKH = new SelectList(db.KhachHangs, "IDKH", "HoTen");
            return View();
        }

        // POST: DatDichVus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDDatDV,IDKH,IDDV,NgaySuDung")] DatDichVu datDichVu)
        {
            if (ModelState.IsValid)
            {
                db.DatDichVus.Add(datDichVu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDDV = new SelectList(db.DichVus, "IDDV", "TenDV", datDichVu.IDDV);
            ViewBag.IDKH = new SelectList(db.KhachHangs, "IDKH", "HoTen", datDichVu.IDKH);
            return View(datDichVu);
        }

        // GET: DatDichVus/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatDichVu datDichVu = db.DatDichVus.Find(id);
            if (datDichVu == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDDV = new SelectList(db.DichVus, "IDDV", "TenDV", datDichVu.IDDV);
            ViewBag.IDKH = new SelectList(db.KhachHangs, "IDKH", "HoTen", datDichVu.IDKH);
            return View(datDichVu);
        }

        // POST: DatDichVus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDDatDV,IDKH,IDDV,NgaySuDung")] DatDichVu datDichVu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(datDichVu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDDV = new SelectList(db.DichVus, "IDDV", "TenDV", datDichVu.IDDV);
            ViewBag.IDKH = new SelectList(db.KhachHangs, "IDKH", "HoTen", datDichVu.IDKH);
            return View(datDichVu);
        }

        // GET: DatDichVus/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatDichVu datDichVu = db.DatDichVus.Find(id);
            if (datDichVu == null)
            {
                return HttpNotFound();
            }
            return View(datDichVu);
        }

        // POST: DatDichVus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DatDichVu datDichVu = db.DatDichVus.Find(id);
            db.DatDichVus.Remove(datDichVu);
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
