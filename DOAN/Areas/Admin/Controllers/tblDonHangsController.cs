using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DOAN.Models;

namespace DOAN.Areas.Admin.Controllers
{
    public class tblDonHangsController : Controller
    {
        private QuanLyBanHangEntities1 db = new QuanLyBanHangEntities1();
        private DateTime StartMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        private DateTime CurrenDate = DateTime.Now;
        // GET: Admin/tblDonHangs
        public ActionResult Index(string startDate, string endDate)
        {
            var tblDonHangs = db.tblDonHangs.Include(t => t.tblNguoiDung).Include(t => t.tblTrangThai);
            if (startDate == null && endDate == null)
            {
                tblDonHangs = tblDonHangs.AsNoTracking().Where(n => n.NgayDatHang >= StartMonth && n.NgayDatHang <= CurrenDate);
                ViewData["startDate"] = StartMonth.ToString("dd/MM/yyyy");
                ViewData["endDate"] = CurrenDate.ToString("dd/MM/yyyy");
            }
            else
            {
                string[] formats = {"dd/MM/yyyy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy"};
                var start = DateTime.ParseExact(startDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None);
                var end = DateTime.ParseExact(endDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).AddDays(1);

                tblDonHangs = tblDonHangs.AsNoTracking().Where(n => n.NgayDatHang >= start && n.NgayDatHang <= end);

                ViewData["startDate"] = startDate;
                ViewData["endDate"] = endDate;

            }           
            return View(tblDonHangs.ToList());
        }

        public ActionResult PhanLoaiDonHang(int? id)
        {
            var tblDonHangs = db.tblDonHangs.Include(p => p.MaTrangThai);
            if (id != null)
            {
                tblDonHangs = db.tblDonHangs.Where(x => x.MaTrangThai == id);
            }
            return View(tblDonHangs.ToList());
        }

        // GET: Admin/tblDonHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDonHang tblDonHang = db.tblDonHangs.Find(id);
            if (tblDonHang == null)
            {
                return HttpNotFound();
            }
            return View(tblDonHang);
        }

        public ActionResult DonHangMoi()
        {
            var dh = db.tblDonHangs.Where(x => x.MaTrangThai == 3);
            return View(dh.ToList());
        }
        public ActionResult DonHangXacNhan()
        {
            var dh = db.tblDonHangs.Where(x => x.MaTrangThai == 4);
            return View(dh.ToList());
        }
        public ActionResult DonHangDangGiao()
        {
            var dh = db.tblDonHangs.Where(x => x.MaTrangThai == 5);
            return View(dh.ToList());
        }
        public ActionResult DonHangThanhCong()
        {
            var dh = db.tblDonHangs.Where(x => x.MaTrangThai == 6);
            return View(dh.ToList());
        }
        public ActionResult DonHangHuy()
        {
            var dh = db.tblDonHangs.Where(x => x.MaTrangThai == 7);
            return View(dh.ToList());
        }

        // GET: Admin/tblDonHangs/Create
        public ActionResult Create()
        {
            ViewBag.MaNguoiDung = new SelectList(db.tblNguoiDungs, "MaNguoiDung", "TenNguoiDung");
            ViewBag.MaTrangThai = new SelectList(db.tblTrangThais.Where(x => x.MaLTT == 2), "MaTrangThai", "TenTrangThai");
            return View();
        }

        // POST: Admin/tblDonHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDonHang,TenNguoiNhan,DiaChiGiaoHang,SDT,NgayDatHang,MaNguoiDung,MaTrangThai")] tblDonHang tblDonHang)
        {
            if (ModelState.IsValid)
            {
                db.tblDonHangs.Add(tblDonHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaNguoiDung = new SelectList(db.tblNguoiDungs, "MaNguoiDung", "TenNguoiDung", tblDonHang.MaNguoiDung);
            ViewBag.MaTrangThai = new SelectList(db.tblTrangThais.Where(x => x.MaLTT == 2), "MaTrangThai", "TenTrangThai", tblDonHang.MaTrangThai);
            return View(tblDonHang);
        }

        // GET: Admin/tblDonHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDonHang tblDonHang = db.tblDonHangs.Find(id);
            if (tblDonHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNguoiDung = new SelectList(db.tblNguoiDungs, "MaNguoiDung", "TenNguoiDung", tblDonHang.MaNguoiDung);
            ViewBag.MaTrangThai = new SelectList(db.tblTrangThais.Where(x => x.MaLTT == 2), "MaTrangThai", "TenTrangThai", tblDonHang.MaTrangThai);
            return View(tblDonHang);
        }

        // POST: Admin/tblDonHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDonHang,TenNguoiNhan,DiaChiGiaoHang,SDT,NgayDatHang,MaNguoiDung,MaTrangThai")] tblDonHang tblDonHang)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(tblDonHang).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            if (ModelState.IsValid)
            {
                db.Entry(tblDonHang).State = EntityState.Modified;
                db.SaveChanges();
                // lay trạng thái của phiếu nhập
                int madh = tblDonHang.MaDonHang;
                // int masp = tblChiTietPhieuNhap.MaSanPham;
                int MaTrangThai = (int)db.tblDonHangs.Where(x => x.MaDonHang == madh).FirstOrDefault().MaTrangThai;
                // câp nhật số lượng bảng sản phẩm
                if (MaTrangThai == 7)
                {
                    var CTDH = db.tblChiTietDonHangs.Where(x => x.MaDonHang == madh).ToList();
                    foreach (var item in CTDH)
                    {
                        int masp = item.MaSanPham;
                        var sanpham = db.tblSanPhams.Where(x => x.MaSanPham == masp).FirstOrDefault();
                        sanpham.SoLuong += item.SoLuong;
                        db.Entry(sanpham).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }
            ViewBag.MaNguoiDung = new SelectList(db.tblNguoiDungs, "MaNguoiDung", "TenNguoiDung", tblDonHang.MaNguoiDung);
            ViewBag.MaTrangThai = new SelectList(db.tblTrangThais.Where(x => x.MaLTT == 2), "MaTrangThai", "TenTrangThai", tblDonHang.MaTrangThai);
            return View(tblDonHang);
        }

        // GET: Admin/tblDonHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDonHang tblDonHang = db.tblDonHangs.Find(id);
            if (tblDonHang == null)
            {
                return HttpNotFound();
            }
            return View(tblDonHang);
        }

        // POST: Admin/tblDonHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblDonHang tblDonHang = db.tblDonHangs.Find(id);
            db.tblDonHangs.Remove(tblDonHang);
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
