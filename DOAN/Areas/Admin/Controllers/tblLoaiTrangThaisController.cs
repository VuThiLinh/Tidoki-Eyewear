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
    public class tblLoaiTrangThaisController : Controller
    {
        private QuanLyBanHangEntities1 db = new QuanLyBanHangEntities1();

        // GET: Admin/tblLoaiTrangThais
        public ActionResult Index()
        {
            return View(db.tblLoaiTrangThais.ToList());
        }

        public ActionResult MenuTrangThai()
        {
            return PartialView("MenuTrangThai", db.tblLoaiTrangThais.ToList());
        }

        // GET: Admin/tblLoaiTrangThais/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLoaiTrangThai tblLoaiTrangThai = db.tblLoaiTrangThais.Find(id);
            if (tblLoaiTrangThai == null)
            {
                return HttpNotFound();
            }
            return View(tblLoaiTrangThai);
        }

        // GET: Admin/tblLoaiTrangThais/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/tblLoaiTrangThais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLTT,TenLTT")] tblLoaiTrangThai tblLoaiTrangThai)
        {
            if (ModelState.IsValid)
            {
                db.tblLoaiTrangThais.Add(tblLoaiTrangThai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblLoaiTrangThai);
        }

        // GET: Admin/tblLoaiTrangThais/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLoaiTrangThai tblLoaiTrangThai = db.tblLoaiTrangThais.Find(id);
            if (tblLoaiTrangThai == null)
            {
                return HttpNotFound();
            }
            return View(tblLoaiTrangThai);
        }

        // POST: Admin/tblLoaiTrangThais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLTT,TenLTT")] tblLoaiTrangThai tblLoaiTrangThai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblLoaiTrangThai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblLoaiTrangThai);
        }

        // GET: Admin/tblLoaiTrangThais/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLoaiTrangThai tblLoaiTrangThai = db.tblLoaiTrangThais.Find(id);
            if (tblLoaiTrangThai == null)
            {
                return HttpNotFound();
            }
            return View(tblLoaiTrangThai);
        }

        // POST: Admin/tblLoaiTrangThais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblLoaiTrangThai tblLoaiTrangThai = db.tblLoaiTrangThais.Find(id);
            db.tblLoaiTrangThais.Remove(tblLoaiTrangThai);
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
