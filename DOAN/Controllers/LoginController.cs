using DOAN.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DOAN.Controllers
{
    public class LoginController : Controller
    {
        QuanLyBanHangEntities1 db = new QuanLyBanHangEntities1();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string userName, string passWord)
        {
            if (ModelState.IsValid)
            {
                TempData["erorr"] = string.Empty;
                var res = db.tblNguoiDungs.Where(x => x.TenDangNhap == userName&&x.MatKhau == passWord).FirstOrDefault();
                if (res != null)
                {
                    if (res.MaTrangThai == 2)
                    {
                        TempData["erorr"] = "Tài khoản này đã bị khóa !";
                        return RedirectToAction("Index");
                    }

                    Session["UserLogin"] = res;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["erorr"] = "Tài khoản hoặc mật khẩu không chính xác";
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create(tblNguoiDung user)
        {
            
                var HoTen = Request.Form["hoTen"];
                var NamSinh = Request.Form["namSinh"];
                var GioiTinh = Request.Form["gioiTinh"];
                var DiaChi = Request.Form["diaChi"];
                var SDT = Request.Form["sdt"];
                var TenDangNhap = Request.Form["tenDN"];
                var MatKhau = Request.Form["matKhau"];
            //if (!ModelState.IsValid)
            //{
                QuanLyBanHangEntities1 db = new QuanLyBanHangEntities1();

                user.TenNguoiDung = HoTen;
                user.NamSinh = int.Parse(NamSinh);
                user.GioiTinh = GioiTinh;
                user.DiaChi = DiaChi;
                user.SDT = SDT;
                user.TenDangNhap = TenDangNhap;
                user.MatKhau = MatKhau;
                user.MaQuyen = 5;
                user.MaTrangThai = 1;

                db.tblNguoiDungs.Add(user);
                db.SaveChanges();
            //}


            // TODO: Add insert logic here
            return RedirectToAction("Index");
            
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblNguoiDung tblNguoiDung = db.tblNguoiDungs.Find(id);
            if (tblNguoiDung == null)
            {
                return HttpNotFound();
            }
            return View(tblNguoiDung);
        }

        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult Edit(/*int id, FormCollection collection*/ tblNguoiDung user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //try
            //{
            //    // TODO: Add update logic here

            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
            return View(user);
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
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
