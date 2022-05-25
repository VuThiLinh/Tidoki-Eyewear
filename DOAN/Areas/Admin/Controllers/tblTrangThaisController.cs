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
    public class tblTrangThaisController : Controller
    {
        private QuanLyBanHangEntities1 db = new QuanLyBanHangEntities1();

        // GET: Admin/tblTrangThais
        public ActionResult Index()
        {
            var tblTrangThais = db.tblTrangThais.Include(t => t.tblLoaiTrangThai);
            return View(tblTrangThais.ToList());
        }

        public ActionResult TrangThaiTheoLTT(int? id)
        {
            var tblTrangThais = db.tblTrangThais.Include(p => p.MaLTT);
            if (id != null)
            {
                tblTrangThais = db.tblTrangThais.Where(x => x.MaLTT == id);
            }
            return View(tblTrangThais.ToList());
        }

        // GET: Admin/tblTrangThais/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblTrangThai tblTrangThai = db.tblTrangThais.Find(id);
            if (tblTrangThai == null)
            {
                return HttpNotFound();
            }
            return View(tblTrangThai);
        }

        // GET: Admin/tblTrangThais/Create
        public ActionResult Create()
        {
            ViewBag.MaLTT = new SelectList(db.tblLoaiTrangThais, "MaLTT", "TenLTT");
            return View();
        }

        // POST: Admin/tblTrangThais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTrangThai,TenTrangThai,MaLTT")] tblTrangThai tblTrangThai)
        {
            if (ModelState.IsValid)
            {
                db.tblTrangThais.Add(tblTrangThai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLTT = new SelectList(db.tblLoaiTrangThais, "MaLTT", "TenLTT", tblTrangThai.MaLTT);
            return View(tblTrangThai);
        }

        // GET: Admin/tblTrangThais/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblTrangThai tblTrangThai = db.tblTrangThais.Find(id);
            if (tblTrangThai == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLTT = new SelectList(db.tblLoaiTrangThais, "MaLTT", "TenLTT", tblTrangThai.MaLTT);
            return View(tblTrangThai);
        }

        // POST: Admin/tblTrangThais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTrangThai,TenTrangThai,MaLTT")] tblTrangThai tblTrangThai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblTrangThai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLTT = new SelectList(db.tblLoaiTrangThais, "MaLTT", "TenLTT", tblTrangThai.MaLTT);
            return View(tblTrangThai);
        }

        // GET: Admin/tblTrangThais/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblTrangThai tblTrangThai = db.tblTrangThais.Find(id);
            if (tblTrangThai == null)
            {
                return HttpNotFound();
            }
            return View(tblTrangThai);
        }

        // POST: Admin/tblTrangThais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblTrangThai tblTrangThai = db.tblTrangThais.Find(id);
            db.tblTrangThais.Remove(tblTrangThai);
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
