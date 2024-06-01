using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PBL3_HotelManagementSystem.Helpers;
using PBL3_HotelManagementSystem.Models;

namespace PBL3_HotelManagementSystem.Controllers
{
    public class AccountsController : Controller
    {
        private HotelManagementDbContext db = new HotelManagementDbContext();

        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var account = db.Accounts.SingleOrDefault(predicate: a => a.Email == email && a.Pass == password);
                if (account != null)
                {
                    Session["UserID"] = account.IDAccount;
                    Session["UserName"] = account.UserName;
                    Session["UserRole"] = account.PhanQuyen;
                    if (account.PhanQuyen == "admin")
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (account.PhanQuyen == "Khách Hàng")
                    {
                        return RedirectToAction("PageUser", "Home"); // Chuyển hướng đến trang chính của khách hàng
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Sai tài khoản hoặc mật khẩu!";
                }
            }
            return View();
        }



        // GET: Account/Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }



        // GET: Accounts/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: Accounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDAccount,UserName,Email,Pass,PhanQuyen")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(account);
        }

        // GET: Accounts/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDAccount,UserName,Email,Pass,PhanQuyen")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: Accounts/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
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

        //=================//

        // GET: Accounts/Register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        // POST: Accounts/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = db.Accounts.FirstOrDefault(u => u.Email == model.Email);
                if (existingUser != null)
                {
                    ViewBag.ErrorMessage = "Email đã tồn tại.";
                    return View(model);
                }

                // Tạo ID khách hàng mới
                var newIDKH = GenerateNewCustomerId();

                var newUser = new Account
                {
                    IDAccount = newIDKH,
                    UserName = model.FullName,
                    Email = model.Email,
                    Pass = PasswordHelper.HashPassword(model.Password), // Hash mật khẩu trước khi lưu
                    PhanQuyen = "Khách Hàng"
                };

                db.Accounts.Add(newUser);

                var newCustomer = new KhachHang
                {
                    IDKH = newUser.IDAccount,
                    HoTen = model.FullName,
                    CCCD = model.CCCD,
                    SDT = model.PhoneNumber,
                    Email = model.Email,
                    GioiTinh = model.Gender,
                    DiaChi = model.Address
                };

                db.KhachHangs.Add(newCustomer);
                db.SaveChanges();

                return RedirectToAction("PageUser", "Home");
            }

            ViewBag.ErrorMessage = "Vui lòng kiểm tra lại thông tin.";
            return View(model);
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

    }
}
