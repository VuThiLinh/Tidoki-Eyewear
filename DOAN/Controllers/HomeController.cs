using DOAN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOAN.Controllers
{
    public class HomeController : Controller
    {
        QuanLyBanHangEntities1 obj = new QuanLyBanHangEntities1();
        // GET: Home
        public ActionResult Index()
        {
            var lstSanPham = obj.tblSanPhams.ToList();
            return View(lstSanPham);
        }

        public ActionResult GioiThieu()
        {
            return View();
        }
        public ActionResult TinTuc()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}