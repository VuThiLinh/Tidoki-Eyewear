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
    public class tblSanPhamsController : Controller
    {
        private readonly QuanLyBanHangEntities1 db = new QuanLyBanHangEntities1();

        // GET: Admin/tblSanPhams
        public ActionResult Index(string SearchString = "")
        {
            var models = db.tblSanPhams.AsQueryable();
            if (SearchString != "")
            {
                IQueryable<tblSanPham> sanPhams = db.tblSanPhams.Where(x => x.TenSanPham.ToUpper().Contains(SearchString.ToUpper()));
                return View(sanPhams.ToList());
            }
            var tblSanPhams = db.tblSanPhams.Include(t => t.tblDanhMucSP).Include(t => t.tblNhaCungCap).ToList();
            return View(tblSanPhams);
        }

        // GET: Admin/tblSanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSanPham tblSanPham = db.tblSanPhams.Find(id);
            if (tblSanPham == null)
            {
                return HttpNotFound();
            }
            return View(tblSanPham);
        }

        // GET: Admin/tblSanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaDanhMuc = new SelectList(db.tblDanhMucSPs, "MaDanhMuc", "TenDanhMuc");
            ViewBag.MaNCC = new SelectList(db.tblNhaCungCaps, "MaNCC", "TenNCC");
            return View();
        }

        // POST: Admin/tblSanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSanPham,TenSanPham,HinhAnh,MoTa,SoLuong,DonViTinh,DonGia,MaDanhMuc,MaNCC")] tblSanPham tblSanPham, HttpPostedFileBase fileImage)
        {
            if (ModelState.IsValid)
            {
                if(fileImage.FileName != null)
                {
                    fileImage.SaveAs(Server.MapPath("~/Content/images/SanPham/" + fileImage.FileName));
                    tblSanPham.HinhAnh = "/Content/images/SanPham/" + fileImage.FileName;
                }
                db.tblSanPhams.Add(tblSanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDanhMuc = new SelectList(db.tblDanhMucSPs, "MaDanhMuc", "TenDanhMuc", tblSanPham.MaDanhMuc);
            ViewBag.MaNCC = new SelectList(db.tblNhaCungCaps, "MaNCC", "TenNCC", tblSanPham.MaNCC);
            return View(tblSanPham);
        }

        // GET: Admin/tblSanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSanPham tblSanPham = db.tblSanPhams.Find(id);
            if (tblSanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDanhMuc = new SelectList(db.tblDanhMucSPs, "MaDanhMuc", "TenDanhMuc", tblSanPham.MaDanhMuc);
            ViewBag.MaNCC = new SelectList(db.tblNhaCungCaps, "MaNCC", "TenNCC", tblSanPham.MaNCC);
            return View(tblSanPham);
        }

        // POST: Admin/tblSanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSanPham,TenSanPham,HinhAnh,MoTa,SoLuong,DonViTinh,DonGia,MaDanhMuc,MaNCC")] tblSanPham tblSanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblSanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDanhMuc = new SelectList(db.tblDanhMucSPs, "MaDanhMuc", "TenDanhMuc", tblSanPham.MaDanhMuc);
            ViewBag.MaNCC = new SelectList(db.tblNhaCungCaps, "MaNCC", "TenNCC", tblSanPham.MaNCC);
            return View(tblSanPham);
        }

        // GET: Admin/tblSanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSanPham tblSanPham = db.tblSanPhams.Find(id);
            if (tblSanPham == null)
            {
                return HttpNotFound();
            }
            return View(tblSanPham);
        }

        // POST: Admin/tblSanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            tblSanPham tblSanPham = db.tblSanPhams.Find(id);
            db.tblSanPhams.Remove(tblSanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SanPhamTheoDanhMuc1(int? id)
        {
            var tblSanPhams = db.tblSanPhams.Include(p => p.MaDanhMuc);
            if (id != null)
            {
                tblSanPhams = db.tblSanPhams.Where(x => x.MaDanhMuc == id);
            }
            return View(tblSanPhams.ToList());
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
