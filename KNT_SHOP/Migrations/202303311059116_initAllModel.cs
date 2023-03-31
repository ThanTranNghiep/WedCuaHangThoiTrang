namespace KNT_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initAllModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BangGia",
                c => new
                    {
                        MaGia = c.Int(nullable: false, identity: true),
                        GiaBan = c.Decimal(nullable: false, storeType: "money"),
                        NgayCapNhat = c.DateTime(storeType: "date"),
                        MaSanPham = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaGia)
                .ForeignKey("dbo.SanPham", t => t.MaSanPham, cascadeDelete: true)
                .Index(t => t.MaSanPham);
            
            CreateTable(
                "dbo.SanPham",
                c => new
                    {
                        MaSanPham = c.Int(nullable: false, identity: true),
                        TenSanPham = c.String(nullable: false, maxLength: 100),
                        HinhAnh = c.String(maxLength: 200),
                        MaLoaiSanPham = c.Int(nullable: false),
                        SoLuongTon = c.Int(nullable: false),
                        MauSac = c.String(maxLength: 100),
                        KichThuocSanPham = c.String(maxLength: 50),
                        MaNhaSanXuat = c.Int(),
                        ThongTinSanPham = c.String(maxLength: 3000),
                        status = c.Boolean(),
                    })
                .PrimaryKey(t => t.MaSanPham)
                .ForeignKey("dbo.LoaiSanPham", t => t.MaLoaiSanPham, cascadeDelete: true)
                .ForeignKey("dbo.NhaSanXuat", t => t.MaNhaSanXuat, cascadeDelete: true)
                .Index(t => t.MaLoaiSanPham)
                .Index(t => t.MaNhaSanXuat);
            
            CreateTable(
                "dbo.ChiTiet_NhapHang",
                c => new
                    {
                        MaHDN = c.String(nullable: false, maxLength: 10, unicode: false),
                        MaSanPham = c.Int(nullable: false),
                        SoLuong = c.Int(nullable: false),
                        DonGia = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.MaHDN)
                .ForeignKey("dbo.HoaDonNhapHang", t => t.MaHDN, cascadeDelete: true)
                .ForeignKey("dbo.SanPham", t => t.MaSanPham, cascadeDelete: true)
                .Index(t => t.MaHDN)
                .Index(t => t.MaSanPham);
            
            CreateTable(
                "dbo.HoaDonNhapHang",
                c => new
                    {
                        MaHDN = c.String(nullable: false, maxLength: 10, unicode: false),
                        NgayNhapHang = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.MaHDN);
            
            CreateTable(
                "dbo.ChiTietGioHang",
                c => new
                    {
                        MaDonHang = c.Int(nullable: false, identity: true),
                        MaSanPham = c.Int(nullable: false),
                        MaGioHang = c.Int(nullable: false),
                        SoLuong = c.Int(nullable: false),
                        TrangThai = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MaDonHang, t.MaSanPham, t.MaGioHang })
                .ForeignKey("dbo.GioHang", t => t.MaGioHang, cascadeDelete: true)
                .ForeignKey("dbo.SanPham", t => t.MaSanPham, cascadeDelete: true)
                .Index(t => t.MaSanPham)
                .Index(t => t.MaGioHang);
            
            CreateTable(
                "dbo.GioHang",
                c => new
                    {
                        MaGioHang = c.Int(nullable: false, identity: true),
                        Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.MaGioHang)
                .ForeignKey("dbo.AspNetUsers", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.HoaDon",
                c => new
                    {
                        MaHoaDon = c.Int(nullable: false, identity: true),
                        NgayLapHoaDon = c.DateTime(storeType: "date"),
                        Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.MaHoaDon)
                .ForeignKey("dbo.AspNetUsers", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.ChiTietHoaDon",
                c => new
                    {
                        MaSanPham = c.Int(nullable: false),
                        MaHoaDon = c.Int(nullable: false),
                        SoLuong = c.Int(nullable: false),
                        DonGia = c.Decimal(nullable: false, storeType: "money"),
                        TrangThaiGiaoHang = c.Int(),
                    })
                .PrimaryKey(t => new { t.MaSanPham, t.MaHoaDon })
                .ForeignKey("dbo.HoaDon", t => t.MaHoaDon, cascadeDelete: true)
                .ForeignKey("dbo.SanPham", t => t.MaSanPham, cascadeDelete: true)
                .Index(t => t.MaSanPham)
                .Index(t => t.MaHoaDon);
            
            CreateTable(
                "dbo.QuanHuyen",
                c => new
                    {
                        MaQuan_Huyen = c.String(nullable: false, maxLength: 10, unicode: false),
                        TenQuanHuyen = c.String(nullable: false, maxLength: 100),
                        MaTinh_ThanhPho = c.String(nullable: false, maxLength: 10, unicode: false),
                    })
                .PrimaryKey(t => t.MaQuan_Huyen)
                .ForeignKey("dbo.Tinh_TP", t => t.MaTinh_ThanhPho, cascadeDelete: true)
                .Index(t => t.MaTinh_ThanhPho);
            
            CreateTable(
                "dbo.Tinh_TP",
                c => new
                    {
                        MaTinh_ThanhPho = c.String(nullable: false, maxLength: 10, unicode: false),
                        TenTinh_ThanhPho = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.MaTinh_ThanhPho);
            
            CreateTable(
                "dbo.LoaiSanPham",
                c => new
                    {
                        MaLoaiSanPham = c.Int(nullable: false, identity: true),
                        TenLoaiSanPham = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.MaLoaiSanPham);
            
            CreateTable(
                "dbo.NhaSanXuat",
                c => new
                    {
                        MaNhaSanXuat = c.Int(nullable: false, identity: true),
                        TenNhaSanXuat = c.String(nullable: false, maxLength: 200),
                        DiaChi = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.MaNhaSanXuat);
            
            AddColumn("dbo.AspNetUsers", "Name", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.AspNetUsers", "GioiTinh", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "NgayThangNamSinh", c => c.DateTime(storeType: "date"));
            AddColumn("dbo.AspNetUsers", "MaQuan_Huyen", c => c.String(maxLength: 10, unicode: false));
            AddColumn("dbo.AspNetUsers", "DiaChiNha", c => c.String(maxLength: 200));
            AddColumn("dbo.AspNetUsers", "Rule", c => c.Boolean());
            CreateIndex("dbo.AspNetUsers", "MaQuan_Huyen");
            AddForeignKey("dbo.AspNetUsers", "MaQuan_Huyen", "dbo.QuanHuyen", "MaQuan_Huyen", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SanPham", "MaNhaSanXuat", "dbo.NhaSanXuat");
            DropForeignKey("dbo.SanPham", "MaLoaiSanPham", "dbo.LoaiSanPham");
            DropForeignKey("dbo.ChiTietGioHang", "MaSanPham", "dbo.SanPham");
            DropForeignKey("dbo.ChiTietGioHang", "MaGioHang", "dbo.GioHang");
            DropForeignKey("dbo.QuanHuyen", "MaTinh_ThanhPho", "dbo.Tinh_TP");
            DropForeignKey("dbo.AspNetUsers", "MaQuan_Huyen", "dbo.QuanHuyen");
            DropForeignKey("dbo.ChiTietHoaDon", "MaSanPham", "dbo.SanPham");
            DropForeignKey("dbo.ChiTietHoaDon", "MaHoaDon", "dbo.HoaDon");
            DropForeignKey("dbo.HoaDon", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.GioHang", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ChiTiet_NhapHang", "MaSanPham", "dbo.SanPham");
            DropForeignKey("dbo.ChiTiet_NhapHang", "MaHDN", "dbo.HoaDonNhapHang");
            DropForeignKey("dbo.BangGia", "MaSanPham", "dbo.SanPham");
            DropIndex("dbo.QuanHuyen", new[] { "MaTinh_ThanhPho" });
            DropIndex("dbo.ChiTietHoaDon", new[] { "MaHoaDon" });
            DropIndex("dbo.ChiTietHoaDon", new[] { "MaSanPham" });
            DropIndex("dbo.HoaDon", new[] { "Id" });
            DropIndex("dbo.AspNetUsers", new[] { "MaQuan_Huyen" });
            DropIndex("dbo.GioHang", new[] { "Id" });
            DropIndex("dbo.ChiTietGioHang", new[] { "MaGioHang" });
            DropIndex("dbo.ChiTietGioHang", new[] { "MaSanPham" });
            DropIndex("dbo.ChiTiet_NhapHang", new[] { "MaSanPham" });
            DropIndex("dbo.ChiTiet_NhapHang", new[] { "MaHDN" });
            DropIndex("dbo.SanPham", new[] { "MaNhaSanXuat" });
            DropIndex("dbo.SanPham", new[] { "MaLoaiSanPham" });
            DropIndex("dbo.BangGia", new[] { "MaSanPham" });
            DropColumn("dbo.AspNetUsers", "Rule");
            DropColumn("dbo.AspNetUsers", "DiaChiNha");
            DropColumn("dbo.AspNetUsers", "MaQuan_Huyen");
            DropColumn("dbo.AspNetUsers", "NgayThangNamSinh");
            DropColumn("dbo.AspNetUsers", "GioiTinh");
            DropColumn("dbo.AspNetUsers", "Name");
            DropTable("dbo.NhaSanXuat");
            DropTable("dbo.LoaiSanPham");
            DropTable("dbo.Tinh_TP");
            DropTable("dbo.QuanHuyen");
            DropTable("dbo.ChiTietHoaDon");
            DropTable("dbo.HoaDon");
            DropTable("dbo.GioHang");
            DropTable("dbo.ChiTietGioHang");
            DropTable("dbo.HoaDonNhapHang");
            DropTable("dbo.ChiTiet_NhapHang");
            DropTable("dbo.SanPham");
            DropTable("dbo.BangGia");
        }
    }
}
