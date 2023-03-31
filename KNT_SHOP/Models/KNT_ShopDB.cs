using KNT_SHOP.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KNT_Shop.Models
{
    public class KNT_ShopDB : IdentityDbContext<ApplicationUser>
    {

        public KNT_ShopDB()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static KNT_ShopDB Create()
        {
            return new KNT_ShopDB();
        }

        public virtual DbSet<BangGia> BangGias { get; set; }
        public virtual DbSet<ChiTiet_NhapHang> ChiTiet_NhapHang { get; set; }
        public virtual DbSet<ChiTietGioHang> ChiTietGioHangs { get; set; }
        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual DbSet<GioHang> GioHangs { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<HoaDonNhapHang> HoaDonNhapHangs { get; set; }
        public virtual DbSet<LoaiSanPham> LoaiSanPhams { get; set; }
        public virtual DbSet<NhaSanXuat> NhaSanXuats { get; set; }
        public virtual DbSet<QuanHuyen> QuanHuyens { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        //public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<Tinh_TP> Tinh_TP { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BangGia>()
                .Property(e => e.GiaBan)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ChiTiet_NhapHang>()
                .Property(e => e.MaHDN)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTiet_NhapHang>()
                .Property(e => e.DonGia)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ChiTietHoaDon>()
                .Property(e => e.DonGia)
                .HasPrecision(19, 4);

            modelBuilder.Entity<GioHang>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDonNhapHang>()
                .Property(e => e.MaHDN)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDonNhapHang>()
                .HasOptional(e => e.ChiTiet_NhapHang)
                .WithRequired(e => e.HoaDonNhapHang)
                .WillCascadeOnDelete();

            modelBuilder.Entity<NhaSanXuat>()
                .HasMany(e => e.SanPhams)
                .WithOptional(e => e.NhaSanXuat)
                .WillCascadeOnDelete();

            modelBuilder.Entity<QuanHuyen>()
                .Property(e => e.MaQuan_Huyen)
                .IsUnicode(false);

            modelBuilder.Entity<QuanHuyen>()
                .Property(e => e.MaTinh_ThanhPho)
                .IsUnicode(false);

            modelBuilder.Entity<QuanHuyen>()
                .HasMany(e => e.ApplicationUsers)
                .WithOptional(e => e.QuanHuyen)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.MaQuan_Huyen)
                .IsUnicode(false);

            modelBuilder.Entity<Tinh_TP>()
                .Property(e => e.MaTinh_ThanhPho)
                .IsUnicode(false);
            base.OnModelCreating(modelBuilder);
        }
    }
}