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
    public class tblNhaCungCapsController : Controller
    {
        private QuanLyBanHangEntities1 db = new QuanLyBanHangEntities1();

        // GET: Admin/tblNhaCungCaps
        public ActionResult Index(string SearchString = "")
        {
            if (SearchString != "")
            {
                var NhaCungCap = db.tblNhaCungCaps.Where(x => x.TenNCC.ToUpper().Contains(SearchString.ToUpper()));
                return View(NhaCungCap.ToList());
            }
            var lstNhaCC = db.tblNhaCungCaps.ToList();
            return View(lstNhaCC);
        }

        // GET: Admin/tblNhaCungCaps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblNhaCungCap tblNhaCungCap = db.tblNhaCungCaps.Find(id);
            if (tblNhaCungCap == null)
            {
                return HttpNotFound();
            }
            return View(tblNhaCungCap);
        }

        // GET: Admin/tblNhaCungCaps/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/tblNhaCungCaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNCC,TenNCC,DiaChi,SDT")] tblNhaCungCap tblNhaCungCap)
        {
            if (ModelState.IsValid)
            {
                db.tblNhaCungCaps.Add(tblNhaCungCap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblNhaCungCap);
        }

        // GET: Admin/tblNhaCungCaps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblNhaCungCap tblNhaCungCap = db.tblNhaCungCaps.Find(id);
            if (tblNhaCungCap == null)
            {
                return HttpNotFound();
            }
            return View(tblNhaCungCap);
        }

        // POST: Admin/tblNhaCungCaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNCC,TenNCC,DiaChi,SDT")] tblNhaCungCap tblNhaCungCap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblNhaCungCap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblNhaCungCap);
        }

        // GET: Admin/tblNhaCungCaps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblNhaCungCap tblNhaCungCap = db.tblNhaCungCaps.Find(id);
            if (tblNhaCungCap == null)
            {
                return HttpNotFound();
            }
            return View(tblNhaCungCap);
        }

        // POST: Admin/tblNhaCungCaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblNhaCungCap tblNhaCungCap = db.tblNhaCungCaps.Find(id);
            db.tblNhaCungCaps.Remove(tblNhaCungCap);
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
