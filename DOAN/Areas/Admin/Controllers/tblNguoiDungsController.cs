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
    public class tblNguoiDungsController : Controller
    {
        private QuanLyBanHangEntities1 db = new QuanLyBanHangEntities1();

        // GET: Admin/tblNguoiDungs
        public ActionResult Index(string SearchString = "")
        {
            if(SearchString != "")
            {
                var user = db.tblNguoiDungs.Where(x => x.TenNguoiDung.ToUpper().Contains(SearchString.ToUpper()));
                return View(user.ToList());
            }
            var lstNguoiDung = db.tblNguoiDungs.Where(x => x.MaQuyen == 5).ToList();
            return View(lstNguoiDung);
            //var tblNguoiDungs = db.tblNguoiDungs.Include(t => t.tblQuyen).Include(t => t.tblTrangThai);
            //return View(tblNguoiDungs.ToList());
        }

        // GET: Admin/tblNguoiDungs/Details/5
        public ActionResult Details(int? id)
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

        // GET: Admin/tblNguoiDungs/Create
        public ActionResult Create()
        {
            ViewBag.MaQuyen = new SelectList(db.tblQuyens, "MaQuyen", "TenQuyen");
            ViewBag.MaTrangThai = new SelectList(db.tblTrangThais.Where(x => x.MaLTT == 1), "MaTrangThai", "TenTrangThai");
            return View();
        }

        // POST: Admin/tblNguoiDungs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNguoiDung,TenNguoiDung,NamSinh,GioiTinh,DiaChi,SDT,MaQuyen,MaTrangThai,TenDangNhap,MatKhau")] tblNguoiDung tblNguoiDung)
        {
            if (ModelState.IsValid)
            {
                db.tblNguoiDungs.Add(tblNguoiDung);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaQuyen = new SelectList(db.tblQuyens, "MaQuyen", "TenQuyen", tblNguoiDung.MaQuyen);
            ViewBag.MaTrangThai = new SelectList(db.tblTrangThais.Where(x => x.MaLTT == 1), "MaTrangThai", "TenTrangThai", tblNguoiDung.MaTrangThai);
            return View(tblNguoiDung);
        }

        // GET: Admin/tblNguoiDungs/Edit/5
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
            ViewBag.MaQuyen = new SelectList(db.tblQuyens, "MaQuyen", "TenQuyen", tblNguoiDung.MaQuyen);
            ViewBag.MaTrangThai = new SelectList(db.tblTrangThais.Where(x => x.MaLTT == 1), "MaTrangThai", "TenTrangThai", tblNguoiDung.MaTrangThai);
            return View(tblNguoiDung);
        }

        // POST: Admin/tblNguoiDungs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNguoiDung,TenNguoiDung,NamSinh,GioiTinh,DiaChi,SDT,MaQuyen,MaTrangThai,TenDangNhap,MatKhau")] tblNguoiDung tblNguoiDung)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblNguoiDung).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaQuyen = new SelectList(db.tblQuyens, "MaQuyen", "TenQuyen", tblNguoiDung.MaQuyen);
            ViewBag.MaTrangThai = new SelectList(db.tblTrangThais, "MaTrangThai", "TenTrangThai", tblNguoiDung.MaTrangThai);
            return View(tblNguoiDung);
        }

        // GET: Admin/tblNguoiDungs/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Admin/tblNguoiDungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblNguoiDung tblNguoiDung = db.tblNguoiDungs.Find(id);
            db.tblNguoiDungs.Remove(tblNguoiDung);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DSNhanVien()
        {
            var NhanVien = db.tblNguoiDungs.Where(x => x.MaQuyen == 4);
            return View(NhanVien.ToList());
        }
        public ActionResult DSKhachHang()
        {
            var NhanVien = db.tblNguoiDungs.Where(x => x.MaQuyen == 5);
            return View(NhanVien.ToList());
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
