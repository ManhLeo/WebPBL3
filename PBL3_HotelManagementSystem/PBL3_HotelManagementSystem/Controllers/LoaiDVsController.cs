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
    public class LoaiDVsController : Controller
    {
        private PBL3_5Entities1 db = new PBL3_5Entities1();

        // GET: LoaiDVs
        public ActionResult Index()
        {
            return View(db.LoaiDVs.ToList());
        }

        // GET: LoaiDVs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiDV loaiDV = db.LoaiDVs.Find(id);
            if (loaiDV == null)
            {
                return HttpNotFound();
            }
            return View(loaiDV);
        }

        // GET: LoaiDVs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoaiDVs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDLoaiDV,TenLoaiDV,DonGia,SoNguoi")] LoaiDV loaiDV)
        {
            if (ModelState.IsValid)
            {
                db.LoaiDVs.Add(loaiDV);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiDV);
        }

        // GET: LoaiDVs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiDV loaiDV = db.LoaiDVs.Find(id);
            if (loaiDV == null)
            {
                return HttpNotFound();
            }
            return View(loaiDV);
        }

        // POST: LoaiDVs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDLoaiDV,TenLoaiDV,DonGia,SoNguoi")] LoaiDV loaiDV)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiDV).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiDV);
        }

        // GET: LoaiDVs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiDV loaiDV = db.LoaiDVs.Find(id);
            if (loaiDV == null)
            {
                return HttpNotFound();
            }
            return View(loaiDV);
        }

        // POST: LoaiDVs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            LoaiDV loaiDV = db.LoaiDVs.Find(id);
            db.LoaiDVs.Remove(loaiDV);
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
