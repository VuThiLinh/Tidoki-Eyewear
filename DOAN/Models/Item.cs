using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOAN.Models
{
    public class Item
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public string Anh { get; set; }
        public int SoLuong { get; set; }
        public float Gia { get; set; }
        //public int Tien { get; set; }
        public float Total
        {
            get
            {
                return Gia * SoLuong;
            }
        }
    }
}