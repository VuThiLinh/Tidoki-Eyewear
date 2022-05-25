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
    public class tblChiTietDonHangsController : Controller
    {
        private QuanLyBanHangEntities1 db = new QuanLyBanHangEntities1();

        // GET: Admin/tblChiTietDonHangs
        public ActionResult Index()
        {
            var tblChiTietDonHangs = db.tblChiTietDonHangs.Include(t => t.tblDonHang).Include(t => t.tblSanPham);
            return View(tblChiTietDonHangs.ToList());
        }

        // GET: Admin/tblChiTietDonHangs/Details/5
        public ActionResult Details(int? id, int idMaSP)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblChiTietDonHang tblChiTietDonHang = db.tblChiTietDonHangs.Where(x => x.MaDonHang == id && x.MaSanPham == idMaSP).FirstOrDefault();
            if (tblChiTietDonHang == null)
            {
                return HttpNotFound();
            }
            return View(tblChiTietDonHang);
        }

        public ActionResult ChiTietDonHang(int? id, int? matt)
        {
            var tblChiTietDonHangs = db.tblChiTietDonHangs.Include(p => p.MaDonHang);
            if (id != null)
            {
                tblChiTietDonHangs = db.tblChiTietDonHangs.Where(x => x.MaDonHang == id);
            }
            ViewBag.matt = matt;
            return PartialView(tblChiTietDonHangs.Where(n => n.MaDonHang == id).ToList());
            //return View(tblChiTietDonHangs.ToList());
        }

        // GET: Admin/tblChiTietDonHangs/Create
        public ActionResult Create()
        {
            ViewBag.MaDonHang = new SelectList(db.tblDonHangs, "MaDonHang", "TenNguoiNhan");
            ViewBag.MaSanPham = new SelectList(db.tblSanPhams, "MaSanPham", "TenSanPham");
            return View();
        }

        // POST: Admin/tblChiTietDonHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDonHang,MaSanPham,SoLuong,DonGia")] tblChiTietDonHang tblChiTietDonHang)
        {
            if (ModelState.IsValid)
            {
                db.tblChiTietDonHangs.Add(tblChiTietDonHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDonHang = new SelectList(db.tblDonHangs, "MaDonHang", "TenNguoiNhan", tblChiTietDonHang.MaDonHang);
            ViewBag.MaSanPham = new SelectList(db.tblSanPhams, "MaSanPham", "TenSanPham", tblChiTietDonHang.MaSanPham);
            return View(tblChiTietDonHang);
        }

        // GET: Admin/tblChiTietDonHangs/Edit/5
        public ActionResult Edit(int? id, int? idMaSP)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblChiTietDonHang tblChiTietDonHang = db.tblChiTietDonHangs.Where(x => x.MaDonHang == id && x.MaSanPham == idMaSP).FirstOrDefault();
            if (tblChiTietDonHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDonHang = new SelectList(db.tblDonHangs, "MaDonHang", "TenNguoiNhan", tblChiTietDonHang.MaDonHang);
            ViewBag.MaSanPham = new SelectList(db.tblSanPhams, "MaSanPham", "TenSanPham", tblChiTietDonHang.MaSanPham);
            return View(tblChiTietDonHang);
        }

        // POST: Admin/tblChiTietDonHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDonHang,MaSanPham,SoLuong,DonGia")] tblChiTietDonHang tblChiTietDonHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblChiTietDonHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDonHang = new SelectList(db.tblDonHangs, "MaDonHang", "TenNguoiNhan", tblChiTietDonHang.MaDonHang);
            ViewBag.MaSanPham = new SelectList(db.tblSanPhams, "MaSanPham", "TenSanPham", tblChiTietDonHang.MaSanPham);
            return View(tblChiTietDonHang);
        }

        // GET: Admin/tblChiTietDonHangs/Delete/5
        public ActionResult Delete(int? id, int? idMaSP)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblChiTietDonHang tblChiTietDonHang = db.tblChiTietDonHangs.Where(x => x.MaDonHang == id && x.MaSanPham == idMaSP).FirstOrDefault();
            if (tblChiTietDonHang == null)
            {
                return HttpNotFound();
            }
            return View(tblChiTietDonHang);
        }

        // POST: Admin/tblChiTietDonHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblChiTietDonHang tblChiTietDonHang = db.tblChiTietDonHangs.Find(id);
            db.tblChiTietDonHangs.Remove(tblChiTietDonHang);
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
