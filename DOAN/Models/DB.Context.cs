﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DOAN.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QuanLyBanHangEntities1 : DbContext
    {
        public QuanLyBanHangEntities1()
            : base("name=QuanLyBanHangEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblBinhLuan> tblBinhLuans { get; set; }
        public virtual DbSet<tblChiTietDonHang> tblChiTietDonHangs { get; set; }
        public virtual DbSet<tblChiTietPhieuNhap> tblChiTietPhieuNhaps { get; set; }
        public virtual DbSet<tblDanhMucSP> tblDanhMucSPs { get; set; }
        public virtual DbSet<tblDonHang> tblDonHangs { get; set; }
        public virtual DbSet<tblLoaiTrangThai> tblLoaiTrangThais { get; set; }
        public virtual DbSet<tblNguoiDung> tblNguoiDungs { get; set; }
        public virtual DbSet<tblNhaCungCap> tblNhaCungCaps { get; set; }
        public virtual DbSet<tblPhieuNhap> tblPhieuNhaps { get; set; }
        public virtual DbSet<tblQuyen> tblQuyens { get; set; }
        public virtual DbSet<tblTrangThai> tblTrangThais { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<tblSanPham> tblSanPhams { get; set; }
    }
}