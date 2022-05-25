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
    public class tblPhieuNhapsController : Controller
    {
        private QuanLyBanHangEntities1 db = new QuanLyBanHangEntities1();
        private DateTime StartMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        private DateTime CurrenDate = DateTime.Now;
        // GET: Admin/tblPhieuNhaps
        public ActionResult Index(string startDate, string endDate)
        {
            //var tblPhieuNhaps = db.tblPhieuNhaps.Include(t => t.tblNhaCungCap).Include(t => t.tblTrangThai);
            //return View(tblPhieuNhaps.ToList());

            var tblPhieuNhaps = db.tblPhieuNhaps.Include(t => t.tblNguoiDung).Include(t => t.tblTrangThai);
            if (startDate == null && endDate == null)
            {
                tblPhieuNhaps = tblPhieuNhaps.AsNoTracking().Where(n => n.NgayNhap >= StartMonth && n.NgayNhap <= CurrenDate);
                ViewData["startDate"] = StartMonth.ToString("dd/MM/yyyy");
                ViewData["endDate"] = CurrenDate.ToString("dd/MM/yyyy");
            }
            else
            {
                string[] formats = { "dd/MM/yyyy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy" };
                var start = DateTime.ParseExact(startDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None);
                var end = DateTime.ParseExact(endDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).AddDays(1);

                tblPhieuNhaps = tblPhieuNhaps.AsNoTracking().Where(n => n.NgayNhap >= start && n.NgayNhap <= end);

                ViewData["startDate"] = startDate;
                ViewData["endDate"] = endDate;

            }
            return View(tblPhieuNhaps.ToList());
        }

        // GET: Admin/tblPhieuNhaps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPhieuNhap tblPhieuNhap = db.tblPhieuNhaps.Find(id);
            if (tblPhieuNhap == null)
            {
                return HttpNotFound();
            }
            //ViewBag.matt = tblPhieuNhap.MaTrangThai;
            return View(tblPhieuNhap);
        }

        // GET: Admin/tblPhieuNhaps/Create
        public ActionResult Create()
        {
            //ViewBag.MaNguoiDung = new SelectList(db.tblNguoiDungs, "MaNguoiDung", "TenNguoiDung");
            ViewBag.NguoiDung = (Session["UserAdmin"] as tblNguoiDung);
            ViewBag.MaNCC = new SelectList(db.tblNhaCungCaps.ToList(), "MaNCC", "TenNCC");
            List<tblTrangThai> listStatus = GetListStatus();

            ViewBag.MaTrangThai = new SelectList(listStatus, "MaTrangThai", "TenTrangThai");
            return View();
        }

        private List<tblTrangThai> GetListStatus()
        {
            var listStatus = new List<tblTrangThai>();
            var dataStatus = db.tblTrangThais.Where(n => n.MaLTT == 3).ToList();

            foreach (var status in dataStatus)
            {
                var obj = new tblTrangThai();
                if (Session["role"] != null)
                {
                    if (Session["role"].ToString() == Constant.EMPLOYEER
                        && status.MaTrangThai == 8)
                    {
                        obj.MaTrangThai = status.MaTrangThai;
                        obj.TenTrangThai = status.TenTrangThai;
                        obj.MaLTT = status.MaLTT;
                        listStatus.Add(obj);
                    }
                    if (Session["role"].ToString() == Constant.ADMIN)
                    {
                        obj.MaTrangThai = status.MaTrangThai;
                        obj.TenTrangThai = status.TenTrangThai;
                        obj.MaLTT = status.MaLTT;
                        listStatus.Add(obj);
                    }
                }
            }

            return listStatus;
        }

        // POST: Admin/tblPhieuNhaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaPhieuNhap,NgayNhap,GhiChu,MaNCC,MaNguoiDung,MaTrangThai")] tblPhieuNhap tblPhieuNhap)
        {
            if (ModelState.IsValid)
            {
                tblPhieuNhap.MaNguoiDung = (Session["UserAdmin"] as tblNguoiDung).MaNguoiDung;
                db.tblPhieuNhaps.Add(tblPhieuNhap);
                db.SaveChanges();
                return RedirectToAction("Create", "tblChiTietPhieuNhaps");
                //return RedirectToAction("Index");
            }

            //ViewBag.MaNguoiDung = new SelectList(db.tblNguoiDungs, "MaNguoiDung", "TenNguoiDung", tblPhieuNhap.MaNguoiDung);
            ViewBag.NguoiDung = (Session["UserAdmin"] as tblNguoiDung);
            ViewBag.MaNCC = new SelectList(db.tblNhaCungCaps, "MaNCC", "TenNCC", tblPhieuNhap.MaNCC);
            List<tblTrangThai> listStatus = GetListStatus();
            ViewBag.MaTrangThai = new SelectList(listStatus, "MaTrangThai", "TenTrangThai", tblPhieuNhap.MaTrangThai);
            return View(tblPhieuNhap);
        }

        public ActionResult PhieuDaNhap()
        {
            var PhieuNhap = db.tblPhieuNhaps.Where(x => x.MaTrangThai == 10);
            return View(PhieuNhap.ToList());
        }
        public ActionResult PhieuNhapDangCho()
        {
            var PhieuNhap = db.tblPhieuNhaps.Where(x => x.MaTrangThai == 8);
            return View(PhieuNhap.ToList());
        }
        public ActionResult PhieuNhapDaThongQua()
        {
            var PhieuNhap = db.tblPhieuNhaps.Where(x => x.MaTrangThai == 9);
            return View(PhieuNhap.ToList());
        }
        public ActionResult PhieuNhapHuy()
        {
            var PhieuNhap = db.tblPhieuNhaps.Where(x => x.MaTrangThai == 11);
            return View(PhieuNhap.ToList());
        }

        // GET: Admin/tblPhieuNhaps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPhieuNhap tblPhieuNhap = db.tblPhieuNhaps.Find(id);
            if (tblPhieuNhap == null)
            {
                return HttpNotFound();
            }

            ViewBag.MaNguoiDung = new SelectList(db.tblNguoiDungs, "MaNguoiDung", "TenNguoiDung", tblPhieuNhap.MaNguoiDung);
            ViewBag.MaNCC = new SelectList(db.tblNhaCungCaps, "MaNCC", "TenNCC", tblPhieuNhap.MaNCC);
            List<tblTrangThai> listStatus = GetListStatus();
            ViewBag.MaTrangThai = new SelectList(listStatus, "MaTrangThai", "TenTrangThai", tblPhieuNhap.MaTrangThai);
            return View(tblPhieuNhap);
        }

        // POST: Admin/tblPhieuNhaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaPhieuNhap,NgayNhap,GhiChu,MaNCC,MaNguoiDung,MaTrangThai")] tblPhieuNhap tblPhieuNhap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblPhieuNhap).State = EntityState.Modified;
                db.SaveChanges();
                // lay trạng thái của phiếu nhập
                int mapn = tblPhieuNhap.MaPhieuNhap;
                // int masp = tblChiTietPhieuNhap.MaSanPham;
                int MaTrangThai = db.tblPhieuNhaps.Where(x => x.MaPhieuNhap == mapn).FirstOrDefault().MaTrangThai;
                // câp nhật số lượng bảng sản phẩm
                if (MaTrangThai == 10)
                {
                    var CTPN = db.tblChiTietPhieuNhaps.Where(x => x.MaPhieuNhap == mapn).ToList();
                    foreach (var item in CTPN)
                    {
                        int masp = item.MaSanPham;
                        var sanpham = db.tblSanPhams.Where(x => x.MaSanPham == masp).FirstOrDefault();
                        sanpham.SoLuong += item.SoLuong;
                        sanpham.DonGia = item.GiaBan;
                        db.Entry(sanpham).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }



            ViewBag.MaNguoiDung = new SelectList(db.tblNguoiDungs, "MaNguoiDung", "TenNguoiDung", tblPhieuNhap.MaNguoiDung);
            ViewBag.MaNCC = new SelectList(db.tblNhaCungCaps, "MaNCC", "TenNCC", tblPhieuNhap.MaNCC);
            List<tblTrangThai> listStatus = GetListStatus();
            ViewBag.MaTrangThai = new SelectList(listStatus, "MaTrangThai", "TenTrangThai", tblPhieuNhap.MaTrangThai);
            return View(tblPhieuNhap);
        }

        // GET: Admin/tblPhieuNhaps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPhieuNhap tblPhieuNhap = db.tblPhieuNhaps.Find(id);
            if (tblPhieuNhap == null)
            {
                return HttpNotFound();
            }
            return View(tblPhieuNhap);
        }

        // POST: Admin/tblPhieuNhaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblPhieuNhap tblPhieuNhap = db.tblPhieuNhaps.Find(id);
            db.tblPhieuNhaps.Remove(tblPhieuNhap);
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
