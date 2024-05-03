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
    public class DatDichVuChiTietsController : Controller
    {
        private PBL3_5Entities1 db = new PBL3_5Entities1();

        // GET: DatDichVuChiTiets
        public ActionResult Index()
        {
            var datDichVuChiTiets = db.DatDichVuChiTiets.Include(d => d.DatDichVu);
            return View(datDichVuChiTiets.ToList());
        }

        // GET: DatDichVuChiTiets/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatDichVuChiTiet datDichVuChiTiet = db.DatDichVuChiTiets.Find(id);
            if (datDichVuChiTiet == null)
            {
                return HttpNotFound();
            }
            return View(datDichVuChiTiet);
        }

        // GET: DatDichVuChiTiets/Create
        public ActionResult Create()
        {
            ViewBag.IDDatDV = new SelectList(db.DatDichVus, "IDDatDV", "IDKH");
            return View();
        }

        // POST: DatDichVuChiTiets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDDatDVChiTiet,IDDatDV,SoLuong,GiaTien")] DatDichVuChiTiet datDichVuChiTiet)
        {
            if (ModelState.IsValid)
            {
                db.DatDichVuChiTiets.Add(datDichVuChiTiet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDDatDV = new SelectList(db.DatDichVus, "IDDatDV", "IDKH", datDichVuChiTiet.IDDatDV);
            return View(datDichVuChiTiet);
        }

        // GET: DatDichVuChiTiets/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatDichVuChiTiet datDichVuChiTiet = db.DatDichVuChiTiets.Find(id);
            if (datDichVuChiTiet == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDDatDV = new SelectList(db.DatDichVus, "IDDatDV", "IDKH", datDichVuChiTiet.IDDatDV);
            return View(datDichVuChiTiet);
        }

        // POST: DatDichVuChiTiets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDDatDVChiTiet,IDDatDV,SoLuong,GiaTien")] DatDichVuChiTiet datDichVuChiTiet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(datDichVuChiTiet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDDatDV = new SelectList(db.DatDichVus, "IDDatDV", "IDKH", datDichVuChiTiet.IDDatDV);
            return View(datDichVuChiTiet);
        }

        // GET: DatDichVuChiTiets/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatDichVuChiTiet datDichVuChiTiet = db.DatDichVuChiTiets.Find(id);
            if (datDichVuChiTiet == null)
            {
                return HttpNotFound();
            }
            return View(datDichVuChiTiet);
        }

        // POST: DatDichVuChiTiets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DatDichVuChiTiet datDichVuChiTiet = db.DatDichVuChiTiets.Find(id);
            db.DatDichVuChiTiets.Remove(datDichVuChiTiet);
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
