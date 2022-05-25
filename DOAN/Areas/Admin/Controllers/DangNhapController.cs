using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DOAN.Models;

namespace DOAN.Areas.Admin.Controllers
{
    public class DangNhapController : Controller
    {
        QuanLyBanHangEntities1 db = new QuanLyBanHangEntities1();

        // GET: Admin/DangNhap
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Index(string userName, string passWord)
        {
            if (ModelState.IsValid)
            {
                TempData["erorrAdmin"] = string.Empty;
                var res = db.tblNguoiDungs.Include(n=>n.tblQuyen).Where(x => x.TenDangNhap == userName && x.MatKhau == passWord).FirstOrDefault();
                if (res != null)
                {
                    if (res.MaTrangThai == 2)
                    {
                        TempData["erorrAdmin"] = "Tài khoản đã bị khóa !";
                        return Redirect("/Admin/DangNhap");
                    }
                    Session["UserAdmin"] = res;
                    Session["role"] = res.tblQuyen.TenQuyen;
                    return Redirect("/Admin/Admin/Index");

                }
                else
                {
                    TempData["erorrAdmin"] = "Tài khoản hoặc mật không chính xác";
                    return Redirect("/Admin/DangNhap");
                }
            }
            //ViewBag.Message = "Email hoặc password không khớp !!!";
            return View();
        }

        // GET: Admin/DangNhap/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/DangNhap/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/DangNhap/Create
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

        // GET: Admin/DangNhap/Edit/5
        [HttpGet]
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

        // POST: Admin/DangNhap/Edit/5
        [HttpPost]
        public ActionResult Edit(tblNguoiDung user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
            //try
            //{
            //    // TODO: Add update logic here

            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: Admin/DangNhap/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/DangNhap/Delete/5
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
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
