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
    public class tblChiTietPhieuNhapsController : Controller
    {
        private QuanLyBanHangEntities1 db = new QuanLyBanHangEntities1();

        // GET: Admin/tblChiTietPhieuNhaps
        public ActionResult Index()
        {
            var tblChiTietPhieuNhaps = db.tblChiTietPhieuNhaps.Include(t => t.tblPhieuNhap).Include(t => t.tblSanPham);
            return View(tblChiTietPhieuNhaps.ToList());
        }
        
        public PartialViewResult ChiTietPhieuNhap(int? id, int? matt)
        {

            var tblChiTietPhieuNhaps = db.tblChiTietPhieuNhaps.Include(p => p.MaPhieuNhap);
            if (id != null)
            {
                tblChiTietPhieuNhaps = db.tblChiTietPhieuNhaps.Where(x => x.MaPhieuNhap == id);
            }
            ViewBag.matt = matt;
            return PartialView(tblChiTietPhieuNhaps.Where(n=>n.MaPhieuNhap == id).ToList());
        }

        // GET: Admin/tblChiTietPhieuNhaps/Details/5
        public ActionResult Details(int? id, int idMaSP)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblChiTietPhieuNhap tblChiTietPhieuNhap = db.tblChiTietPhieuNhaps.Where(x => x.MaPhieuNhap == id && x.MaSanPham == idMaSP).FirstOrDefault();
            if (tblChiTietPhieuNhap == null)
            {
                return HttpNotFound();
            }
            return View(tblChiTietPhieuNhap);
        }

        // GET: Admin/tblChiTietPhieuNhaps/Create
        public ActionResult Create()
        {
            TempData["MaPhieuNhap"] = db.tblPhieuNhaps.Max(x => x.MaPhieuNhap);
            int mapn= db.tblPhieuNhaps.Max(x => x.MaPhieuNhap);
            ViewBag.MaPhieuNhap = new SelectList(db.tblPhieuNhaps, "MaPhieuNhap", "MaPhieuNhap");
           // ViewBag.MaSanPham = new SelectList(db.tblSanPhams, "MaSanPham", "TenSanPham");
            int[] lstMaSP = db.tblChiTietPhieuNhaps.Where(x => x.MaPhieuNhap == mapn).Select(x => x.MaSanPham).ToArray();
            List<tblSanPham> dataSanPham = db.tblSanPhams.Where(x => !lstMaSP.Contains(x.MaSanPham)).ToList();
            ViewBag.MaSanPham = new SelectList(dataSanPham, "MaSanPham", "TenSanPham");
            return View();
        }

        // POST: Admin/tblChiTietPhieuNhaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaPhieuNhap,MaSanPham,SoLuong,GiaNhap,GiaBan")] tblChiTietPhieuNhap tblChiTietPhieuNhap)
        {
            int mapn = 0;
            int masp = 0;
            if (ModelState.IsValid)
            {
                //TempData["MaPhieuNhap"] = db.tblPhieuNhaps.Max(x => x.MaPhieuNhap);
                db.tblChiTietPhieuNhaps.Add(tblChiTietPhieuNhap);
                db.SaveChanges();

                // lay trạng thái của phiếu nhập
                 mapn = tblChiTietPhieuNhap.MaPhieuNhap;
                 masp = tblChiTietPhieuNhap.MaSanPham;
                int MaTrangThai = db.tblPhieuNhaps.Where(x => x.MaPhieuNhap == mapn).FirstOrDefault().MaTrangThai;
                // câp nhật số lượng bảng sản phẩm
                if(MaTrangThai == 10)
                {
                    var sanpham = db.tblSanPhams.Where(x => x.MaSanPham == masp).FirstOrDefault();
                    sanpham.SoLuong +=  tblChiTietPhieuNhap.SoLuong;
                    sanpham.DonGia = tblChiTietPhieuNhap.GiaBan;
                    db.Entry(sanpham).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            TempData["MaPhieuNhap"] = db.tblPhieuNhaps.Max(x => x.MaPhieuNhap);
            //ViewBag.MaPhieuNhap = new SelectList(db.tblPhieuNhaps, "MaPhieuNhap", "GhiChu", tblChiTietPhieuNhap.MaPhieuNhap);
            int[] lstMaSP = db.tblChiTietPhieuNhaps.Where(x => x.MaPhieuNhap == mapn).Select(x => x.MaSanPham).ToArray();
            List<tblSanPham> dataSanPham = db.tblSanPhams.Where(x => !lstMaSP.Contains(x.MaSanPham)).ToList();
            ViewBag.MaSanPham = new SelectList(dataSanPham, "MaSanPham", "TenSanPham", tblChiTietPhieuNhap.MaSanPham);
            return View(tblChiTietPhieuNhap);
        }
        // GET: Admin/tblChiTietPhieuNhaps/Edit/5
        public ActionResult Edit(int? id, int? idMaSP)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblChiTietPhieuNhap tblChiTietPhieuNhap = db.tblChiTietPhieuNhaps.Where(x => x.MaPhieuNhap == id && x.MaSanPham == idMaSP).FirstOrDefault();
            if (tblChiTietPhieuNhap == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaPhieuNhap = new SelectList(db.tblPhieuNhaps, "MaPhieuNhap", "GhiChu", tblChiTietPhieuNhap.MaPhieuNhap);
            ViewBag.MaSanPham = new SelectList(db.tblSanPhams, "MaSanPham", "TenSanPham", tblChiTietPhieuNhap.MaSanPham);
            return View(tblChiTietPhieuNhap);
        }

        // POST: Admin/tblChiTietPhieuNhaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaPhieuNhap,MaSanPham,SoLuong,GiaNhap,GiaBan")] tblChiTietPhieuNhap tblChiTietPhieuNhap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblChiTietPhieuNhap).State = EntityState.Modified;
                db.SaveChanges();

                //// lay trạng thái của phiếu nhập
                //int mapn = tblChiTietPhieuNhap.MaPhieuNhap;
                //int masp = tblChiTietPhieuNhap.MaSanPham;
                //int MaTrangThai = db.tblPhieuNhaps.Where(x => x.MaPhieuNhap == mapn).FirstOrDefault().MaTrangThai;
                //// câp nhật số lượng bảng sản phẩm
                //if (MaTrangThai == 10)
                //{
                //    var sanpham = db.tblSanPhams.Where(x => x.MaSanPham == masp).FirstOrDefault();
                //    sanpham.SoLuong += tblChiTietPhieuNhap.SoLuong;
                //    db.Entry(sanpham).State = EntityState.Modified;
                //    db.SaveChanges();
                //}
                return RedirectToAction("Index");
            }
            ViewBag.MaPhieuNhap = new SelectList(db.tblPhieuNhaps, "MaPhieuNhap", "GhiChu", tblChiTietPhieuNhap.MaPhieuNhap);
            ViewBag.MaSanPham = new SelectList(db.tblSanPhams, "MaSanPham", "TenSanPham", tblChiTietPhieuNhap.MaSanPham);
            return View(tblChiTietPhieuNhap);
        }

        // GET: Admin/tblChiTietPhieuNhaps/Delete/5
        public ActionResult Delete(int? id, int? idMaSP)
        {
            if (id == null || idMaSP == null )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblChiTietPhieuNhap tblChiTietPhieuNhap = db.tblChiTietPhieuNhaps.Where(x => x.MaPhieuNhap == id && x.MaSanPham == idMaSP).FirstOrDefault();
            if (tblChiTietPhieuNhap == null)
            {
                return HttpNotFound();
            }
            
            return View(tblChiTietPhieuNhap);

        }

        // POST: Admin/tblChiTietPhieuNhaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int idMaSP)
        {
            //tblChiTietPhieuNhap tblChiTietPhieuNhap = db.tblChiTietPhieuNhaps.Find(id);
            tblChiTietPhieuNhap tblChiTietPhieuNhap = db.tblChiTietPhieuNhaps.Where(x => x.MaPhieuNhap == id && x.MaSanPham == idMaSP).FirstOrDefault();
            
            db.tblChiTietPhieuNhaps.Remove(tblChiTietPhieuNhap);
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
