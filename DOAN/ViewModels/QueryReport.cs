using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOAN.ViewModels
{
    public class QueryReport
    {
        public int MaDanhMuc { get; set; }

        public string TenDanhMuc { get; set; }

        public int? SoLuong { get; set; }
        public int? SoLuongNhap { get; set; }
        public decimal DonGia { get; set; } 

        public int? GiaNhap { get; set; }

        public int? MaTrangThai { get; set; }

        public int TrangThaiPhieuNhap { get; set; }
    }
}