using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DOAN.Models;

namespace DOAN.Controllers
{
    public class CartController : Controller
    {
        private List<Item> list = new List<Item>();
        private QuanLyBanHangEntities1 db = new QuanLyBanHangEntities1();
        // GET: Cart
        public ActionResult Index()
        {
            if (Session["Cart"] == null)
            {
                list = new List<Item>();
            }
            else
            {
                list = Session["Cart"] as List<Item>;
            }
            float Total = 0;
            foreach (var item in list)
            {
                Total += item.Total;
            }
            ViewBag.Total = Total;

            // thông tin khách hàng đăng nhập


            return View(list);
        }
        public ActionResult AddCart(int? MaSP, string TenSP, string Anh, float? Gia)
        {
            bool flag = false;
            if (Session["Cart"] == null)
            {
                list = new List<Item>();
                flag = false;
            }
            else
            {
                list = Session["Cart"] as List<Item>;
                foreach (var item in list)
                {
                    if (item.MaSP == MaSP)
                    {
                        flag = true;
                        item.SoLuong += 1;
                        break;
                    }
                }
            }
            if (flag == false)
            {
                Item item = new Item();
                item.MaSP = MaSP.Value;
                item.TenSP = TenSP;
                item.Anh = Anh;
                item.Gia = Gia.Value;
                item.SoLuong = 1;
                list.Add(item);
            }
            Session["Cart"] = list;
            //return RedirectToAction("Index", "Cart");
            return Json(1, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateCart(int MaSP, int SoLuong)
        {
            list = Session["Cart"] as List<Item>;
            foreach (var item in list)
            {
                if (item.MaSP == MaSP)
                {
                    item.SoLuong = SoLuong;
                    break;
                }
            }
            Session["Cart"] = list;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult HoaDon()
        {
            try
            {
                if (Session["Cart"] != null)
                {
                  
                    var OrdName = Request.Form["HoTen"];
                    var OrdAdd = Request.Form["DiaChi"];
                    var OrdPhone = Request.Form["SDT"];

                    QuanLyBanHangEntities1 db = new QuanLyBanHangEntities1();

                    tblDonHang donhang = new tblDonHang();
                    var dataKH = Session["UserLogin"] as tblNguoiDung;

                    if (Session["UserLogin"] != null)
                        donhang.MaNguoiDung = dataKH.MaNguoiDung;
                    //ord.MaDonHang = 10;
                    donhang.TenNguoiNhan = OrdName;
                    donhang.DiaChiGiaoHang = OrdAdd;
                    donhang.SDT = OrdPhone;
                    donhang.NgayDatHang = DateTime.Now;
                    donhang.MaTrangThai = 3;

                    db.tblDonHangs.Add(donhang);
                    db.SaveChanges();


                    ViewBag.NguoiNhan = donhang;

                    int ordID = db.tblDonHangs.Max(o => o.MaDonHang);

                    list = Session["Cart"] as List<Item>;
                    float Total = 0.0f;
                    foreach (var item in list)
                    {
                        //Thêm dữ liệu vào bảng Chi tiết đơn hàng
                        tblChiTietDonHang od = new tblChiTietDonHang();
                        od.MaDonHang = ordID;
                        od.MaSanPham = item.MaSP;
                        od.SoLuong = (byte)item.SoLuong;
                        od.DonGia = int.Parse(item.Gia.ToString());

                        db.tblChiTietDonHangs.Add(od);
                        db.SaveChanges();

                        // cập nhật số lượng kho
                        var sanpham = db.tblSanPhams.Where(x => x.MaSanPham == item.MaSP).FirstOrDefault();
                        sanpham.SoLuong -= item.SoLuong;
                        db.Entry(sanpham).State = EntityState.Modified;
                        db.SaveChanges();

                        Total += item.Total;
                    }
                    ViewBag.Total = Total;

                    //cập n
               
                }
                return View();
            }
            catch
            {
                return PartialView("Error");
            }
        }

        public ActionResult XoaMot(int? id)
        {
            list = Session["Cart"] as List<Item>;
            var sp = list.SingleOrDefault(n => n.MaSP == id);
            list.Remove(sp);
            return RedirectToAction("Index");
        }

        public ActionResult Xoa()
        {
            Session.Clear();
            return View("Index");
        }

        //public ActionResult History(tblNguoiDung tblNguoiDung, tblDonHang tblDonHang)
        //{
        //    QuanLyBanHangEntities1 db = new QuanLyBanHangEntities1();
        //    tblNguoiDung.MaNguoiDung = (Session["UserLogin"] as tblNguoiDung).MaNguoiDung;
        //    var DonHang = db.tblDonHangs.Where(x => x.MaNguoiDung == tblNguoiDung.MaNguoiDung).ToList();
        //    foreach(var item in DonHang)
        //    {
        //        var DH = item.MaTrangThai;
        //    }
        //    return View(DonHang.Where(x => x.MaTrangThai == 6));
        //}

        public ActionResult History()
        {
            QuanLyBanHangEntities1 db = new QuanLyBanHangEntities1();
            var DonHang = (from s in db.tblDonHangs
                           where s.tblTrangThai.MaTrangThai == 6
                           select s).ToList();
            ViewBag.DonHangs = DonHang;
            //TH2:
            //var nhanvien = _db.NHANVIENs.ToList();
            //ViewBag.nhanviens = nhanvien;
            return View();

        }
        public ActionResult FollowOrder()
        {
            var user = Session["UserLogin"] as tblNguoiDung;
            if (user == null) return RedirectToAction("Index", "Login");

            var data = db.tblChiTietDonHangs.Include(n => n.tblSanPham).Include(n => n.tblDonHang).
                Where(n => n.tblDonHang.MaNguoiDung == user.MaNguoiDung).ToList();


            if (data != null) ViewBag.Total = data.Sum(n => n.SoLuong * n.DonGia);

            return View(data);

        }

        public ActionResult TotalQtyCart()
        {
            int TotalQty = 0;
            if (Session["Cart"] != null)
            {
                list = Session["Cart"] as List<Item>;
            }

            foreach (var item in list)
            {
                TotalQty += item.SoLuong;
            }
            ViewBag.TotalQty = TotalQty;
            return PartialView("_CountQty");
        }
        public ActionResult HistoryOrder()
        {
            var user = Session["UserLogin"] as tblNguoiDung;
            //if (user == null) return RedirectToAction("Index", "Login");

            var data = db.tblChiTietDonHangs.Include(n => n.tblSanPham).Include(n => n.tblDonHang).
                Where(n => n.tblDonHang.MaNguoiDung == user.MaNguoiDung && n.tblDonHang.MaTrangThai == 6).ToList();
             

            if (data != null) ViewBag.Total = data.Sum(n => n.SoLuong * n.DonGia);

            return View(data);
        }
    }
}