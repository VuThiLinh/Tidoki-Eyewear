using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DOAN.Models;
using PagedList;

namespace DOAN.Controllers
{
    public class SanPhamController : Controller
    {
        QuanLyBanHangEntities1 db = new QuanLyBanHangEntities1();
        // GET: SanPham

        public ActionResult Index(int? page, int? sh, string SearchString = "")
        {
            var lstSanPham = db.tblSanPhams.ToList();

            if (!string.IsNullOrWhiteSpace(SearchString))
            {
                lstSanPham = lstSanPham.Where(x => x.TenSanPham.ToUpper().Contains(SearchString.ToUpper())).ToList();
            }

            int pageNumber = page ?? 1;
            int pageSize = sh ?? 6;
            ViewBag.sh = sh;
            ViewBag.search = SearchString;

            return View(lstSanPham.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult SanPhamMoi()
        {
            var tblSanPhams = db.tblSanPhams.OrderByDescending(p => p.MaSanPham).Take(4);

            return PartialView("SanPhamMoi", tblSanPhams.ToList());
        }

        public ActionResult SanPhamTheoDanhMuc(int? id)
        {
            var tblSanPhams = db.tblSanPhams.Include(p => p.MaDanhMuc);
            if(id != null)
            {
                tblSanPhams = db.tblSanPhams.Where(x => x.MaDanhMuc == id);
            }
            return View(tblSanPhams.ToList());
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
        public ActionResult Detail(int MaSP)
        {
            var detail = db.tblSanPhams.Where(n => n.MaSanPham == MaSP).FirstOrDefault();
            List<tblBinhLuan> comments = db.tblBinhLuans.Where(n => n.MaSanPham == MaSP).ToList();
            ViewBag.comments = comments;
            return View(detail);
        }

        
    }
}