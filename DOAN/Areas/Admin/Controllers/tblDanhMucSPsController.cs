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
    public class tblDanhMucSPsController : Controller
    {
        private QuanLyBanHangEntities1 db = new QuanLyBanHangEntities1();

        // GET: Admin/tblDanhMucSPs
        public ActionResult Index(string SearchString = "")
        {
            if (SearchString != "")
            {
                var DanhMucSP = db.tblDanhMucSPs.Where(x => x.TenDanhMuc.ToUpper().Contains(SearchString.ToUpper()));
                return View(DanhMucSP.ToList());
            }
            var lstDanhMuc = db.tblDanhMucSPs.ToList();
            return View(lstDanhMuc);
        }

        // GET: Admin/tblDanhMucSPs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDanhMucSP tblDanhMucSP = db.tblDanhMucSPs.Find(id);
            if (tblDanhMucSP == null)
            {
                return HttpNotFound();
            }
            return View(tblDanhMucSP);
        }

        // GET: Admin/tblDanhMucSPs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/tblDanhMucSPs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDanhMuc,TenDanhMuc")] tblDanhMucSP tblDanhMucSP)
        {
            if (ModelState.IsValid)
            {
                db.tblDanhMucSPs.Add(tblDanhMucSP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblDanhMucSP);
        }

        // GET: Admin/tblDanhMucSPs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDanhMucSP tblDanhMucSP = db.tblDanhMucSPs.Find(id);
            if (tblDanhMucSP == null)
            {
                return HttpNotFound();
            }
            return View(tblDanhMucSP);
        }

        // POST: Admin/tblDanhMucSPs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDanhMuc,TenDanhMuc")] tblDanhMucSP tblDanhMucSP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblDanhMucSP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblDanhMucSP);
        }

        // GET: Admin/tblDanhMucSPs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDanhMucSP tblDanhMucSP = db.tblDanhMucSPs.Find(id);
            if (tblDanhMucSP == null)
            {
                return HttpNotFound();
            }
            return View(tblDanhMucSP);
        }

        // POST: Admin/tblDanhMucSPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblDanhMucSP tblDanhMucSP = db.tblDanhMucSPs.Find(id);
            db.tblDanhMucSPs.Remove(tblDanhMucSP);
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

        public ActionResult MenuDM()
        {
            return PartialView("MenuDM", db.tblDanhMucSPs.ToList());
        }
    }
}
