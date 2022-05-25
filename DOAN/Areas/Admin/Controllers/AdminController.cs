using DOAN.Models;
using DOAN.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace DOAN.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class AdminController : Controller
    {
        QuanLyBanHangEntities1 db = new QuanLyBanHangEntities1();
        // GET: Admin/Admin
        public ActionResult Index()


        {
            var countProduct = db.tblSanPhams.Count();
            var countOrderSuccees = db.tblChiTietDonHangs.Where(n => n.tblDonHang.MaTrangThai == 6).Sum(p => p.SoLuong);
            var countOrderNew = db.tblDonHangs.Where(n => n.MaTrangThai == 3).Count();
            var countOrderCanel = db.tblDonHangs.Where(n => n.MaTrangThai == 7).Count();
            var query = db.Database.SqlQuery<QueryReport>(@"select tblDanhMucSP.MaDanhMuc,
                                                        tblDanhMucSP.TenDanhMuc,
                                                        tblSanPham.SoLuong,
                                                        tblSanPham.DonGia,
                                                        tblChiTietPhieuNhap.SoLuong as SoLuongNhap,
                                                        tblChiTietPhieuNhap.GiaNhap,
                                                        tblDonHang.MaTrangThai,
														tblPhieuNhap.MaTrangThai 'TrangThaiPhieuNhap'
                                                        from tblSanPham
                                                        inner join tblDanhMucSP on tblSanPham.MaDanhMuc = tblDanhMucSP.MaDanhMuc
                                                        inner join tblChiTietPhieuNhap on tblChiTietPhieuNhap.MaSanPham = tblSanPham.MaSanPham
														inner join tblPhieuNhap on tblPhieuNhap.MaPhieuNhap = tblChiTietPhieuNhap.MaPhieuNhap
                                                        inner join tblChiTietDonHang on tblChiTietDonHang.MaSanPham = tblSanPham.MaSanPham
                                                        inner join tblDonHang on tblDonHang.MaDonHang=tblChiTietDonHang.MaDonHang and tblPhieuNhap.MaTrangThai=10 and tblDonHang.MaTrangThai=6
                                                        group by tblDanhMucSP.MaDanhMuc,tblDanhMucSP.TenDanhMuc,tblSanPham.SoLuong,
                                                        tblSanPham.DonGia,
                                                        tblChiTietPhieuNhap.SoLuong,
                                                        tblChiTietPhieuNhap.GiaNhap,
                                                        tblDonHang.MaTrangThai,
														tblPhieuNhap.MaTrangThai").ToList();
            var model = query
                .GroupBy(n=>n.MaDanhMuc).Select(g => new ReportInfo
                {
                    Group = g.First().TenDanhMuc,
                    Count = g.Where(n=>n.TrangThaiPhieuNhap==10).Sum(p => p.SoLuongNhap),
                    Sum = g.Where(n => n.MaTrangThai == 6).Sum(p => p.DonGia * p.SoLuong),
                    SumImportPrice = g.Sum(p => p.GiaNhap * p.SoLuongNhap),
                    Min = g.Min(p => p.DonGia),
                    Max = g.Max(p => p.DonGia),
                    Avg = (double?)g.Average(p => p.DonGia)
                }).ToList();


            var statisticCategory = db.tblChiTietDonHangs.Where(n => n.tblDonHang.MaTrangThai == 6)
                .GroupBy(n => n.tblSanPham.tblDanhMucSP)
                .Select(g => new StatisticCategory
                {
                    Id = g.Key.MaDanhMuc,
                    Name = g.Key.TenDanhMuc,
                    TotalMoney = g.Sum(p => p.DonGia * p.SoLuong)
                }).ToList();

            var statisticManufacture = db.tblChiTietDonHangs.Where(n => n.tblDonHang.MaTrangThai == 6)
                .GroupBy(n => n.tblSanPham.tblNhaCungCap)
               .Select(g => new StatisticManufacture
               {
                   Id = g.Key.MaNCC,
                   Name = g.Key.TenNCC,
                   TotalMoney = g.Sum(p => p.DonGia * p.SoLuong)
               }).ToList();

            var statisticMonthYear = db.tblChiTietDonHangs.Where(n => n.tblDonHang.MaTrangThai == 6)
                    .AsEnumerable()
                    .GroupBy(n => new { n.tblDonHang.NgayDatHang.Month, n.tblDonHang.NgayDatHang.Year })
                   .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                   .Select(g => new StatisticMonthYear
                   {
                       MonthYear = $"{g.Key.Month}/{g.Key.Year}",
                       TotalMoney = g.Sum(p => p.DonGia * p.SoLuong)
                   }).ToList();

            StatisticIndexViewModel viewModel = new StatisticIndexViewModel();

            viewModel.TotalProduct = countProduct;
            viewModel.TotalOrderSuccees = countOrderSuccees;
            viewModel.TotalOrderNew = countOrderNew;
            viewModel.TotalOrderCanel = countOrderCanel;
            viewModel.ReportInfo = model;
            viewModel.StatisticCategory = statisticCategory;
            viewModel.StatisticManufacture = statisticManufacture;
            viewModel.StatisticMonthYear = statisticMonthYear;

            return View(viewModel);
        }
    }
}