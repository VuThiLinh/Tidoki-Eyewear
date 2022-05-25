using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DOAN.Models
{
    public class Login
    {
        [Key]
        public int MaNguoiDung { get; set; }

        [Required]
        public string TenNguoiDung { get; set; }
        public int NamSinh { get; set; }
        public string GioiTinh { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public int MaQuyen { get; set; }
        public int MaTrangThai { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }

    }
}