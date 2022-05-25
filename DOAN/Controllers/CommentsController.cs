using DOAN.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace DOAN.Controllers
{
    public class CommentsController : Controller
    {
        QuanLyBanHangEntities1 db = new QuanLyBanHangEntities1();
        public ActionResult AddComment(int masp, string nd)
        {
            tblNguoiDung user = Session["UserLogin"] as tblNguoiDung;
            if (user != null)
            {
                tblBinhLuan comment = new tblBinhLuan();
                comment.MaSanPham = masp;
                comment.NoiDungBL = nd;
                comment.MaNguoiDung = user.MaNguoiDung;
                comment.ThoiGianBL = DateTime.Now;
                db.tblBinhLuans.Add(comment);
                db.SaveChanges();
            }
            return PartialView(db.tblBinhLuans.Include(n=>n.tblNguoiDung).Where(n => n.MaSanPham == masp).ToList());
        }

        public ActionResult DeleteComment(int? commentId, int? productId)
        {
            tblBinhLuan comment = db.tblBinhLuans.Find(commentId);
            if (comment != null)
            {
                db.tblBinhLuans.Remove(comment);
                db.SaveChanges();
            }
            return PartialView("AddComment", db.tblBinhLuans.Where(n => n.MaSanPham == productId).ToList());
        }

        public ActionResult UpdateComment([Bind(Include = "MaBinhLuan,NoiDungBL,MaNguoiDung,ThoiGianBL,MaSanPham")] tblBinhLuan bl,
            int mabl, string noidungbt, int masp, DateTime nbl)
        {
            tblBinhLuan comment = db.tblBinhLuans.Find(mabl);
            tblSanPham sp = db.tblSanPhams.Find(masp);
            var user = Session["UserLogin"] as tblNguoiDung;
            if (comment == null || sp == null || user == null)
            {
                return null;
            }
            comment.MaBinhLuan = mabl;
            comment.NoiDungBL = noidungbt;
            comment.MaNguoiDung = user.MaNguoiDung;
            comment.MaSanPham = masp;
            comment.ThoiGianBL = nbl;
            db.Entry(comment).State = EntityState.Modified;
            db.SaveChanges();
            return PartialView("AddComment", db.tblBinhLuans.Include(n => n.tblNguoiDung).Where(n => n.MaSanPham == masp).ToList());
        }
    }
}