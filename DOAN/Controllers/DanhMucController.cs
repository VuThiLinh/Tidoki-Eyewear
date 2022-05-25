using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOAN.Models;

namespace DOAN.Controllers
{
    public class DanhMucController : Controller
    {
        QuanLyBanHangEntities1 db = new QuanLyBanHangEntities1();
        // GET: DanhMuc
        public ActionResult Index()
        {
            var lstDanhMuc = db.tblDanhMucSPs.ToList();
            return View(lstDanhMuc);
        }

        public ActionResult Menu()
        {
            return PartialView("Menu", db.tblDanhMucSPs.ToList());
        }
        public ActionResult Menu1()
        {
            return PartialView("Menu1", db.tblDanhMucSPs.ToList());
        }
    }
}