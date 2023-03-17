using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using KNT_SHOP.Models;
using KNT_SHOP.Models.ViewModel;

namespace KNT_SHOP.Controllers;

public class CartController : Controller
{
    // GET
    public ActionResult Index()
    {
        if (Session["username"] == null)
        {
            JavaScript("alert('Please login first!');");
            return RedirectToAction("Index", "Login");
        }
        else
        {
            KNT_ShopDB db = new KNT_ShopDB();
            string username = Session["username"].ToString();
            // lấy ra giỏ hàng của user
            var cart = db.GioHangs.FirstOrDefault(x => x.TenTaiKhoan == username);
            // lấy ra danh sách sản phẩm trong giỏ hàng
            var listSanPham = db.ChiTietGioHangs.Where(x => x.MaGioHang == cart.MaGioHang && x.TrangThai < 2).ToList();
            // lấy bảng giá của sản phẩm
            var listGiaBan = db.BangGias.Where(x => x.MaSanPham == x.SanPham.MaSanPham)
                .OrderByDescending(x => x.NgayCapNhat).ToList();


            List<SanPhamModel> listSanPhamModel = new List<SanPhamModel>(
                listSanPham.Select(x => new SanPhamModel()
                {
                    Check = x.TrangThai,
                    MaSanPham = x.SanPham.MaSanPham,
                    TenSanPham = x.SanPham.TenSanPham,
                    SoLuong = x.SoLuong,
                    Gia = listGiaBan.FirstOrDefault(y => y.MaSanPham == x.SanPham.MaSanPham)!.GiaBan,
                    HinhAnh = x.SanPham.HinhAnh
                })
            );
            ViewBag.ListSanPham = listSanPhamModel;
            return View();
        }
    }
    
    public ActionResult Invoice()
    {
        string username = Session["username"].ToString();
        if (username != null)
        {
            KNT_ShopDB db = new KNT_ShopDB();
            var cart = db.GioHangs.FirstOrDefault(x => x.TenTaiKhoan == username);
            var list = db.ChiTietGioHangs.Where(x =>
                x.TrangThai == 1 && x.MaGioHang == cart.MaGioHang).ToList();
        
            if (list.Count > 0)
            {
                // lấy bảng giá của sản phẩm
                var listGiaBan = db.BangGias.Where(x => x.MaSanPham == x.SanPham.MaSanPham)
                    .OrderByDescending(x => x.NgayCapNhat).ToList();
                // Tạo hóa đơn
                HoaDon hoaDon = new HoaDon()
                {
                    TenTaiKhoan = username,
                    NgayLapHoaDon = DateTime.Now
                };
                // Tạo chi tiết hóa đơn
                foreach (var item in list)
                {
                    ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon()
                    {
                        MaHoaDon = hoaDon.MaHoaDon,
                        MaSanPham = item.MaSanPham,
                        SoLuong = item.SoLuong,
                        DonGia = listGiaBan.FirstOrDefault(x => x.MaSanPham == item.MaSanPham)!.GiaBan,
                        TrangThaiGiaoHang = 0
                    };
                    hoaDon.ChiTietHoaDons.Add(chiTietHoaDon);
                }
                // Save hóa đơn và chi tiết hóa đơn
                db.HoaDons.Add(hoaDon);
                db.SaveChanges();
            
                // Update trạng thái của sản phẩm trong giỏ hàng
                foreach (var item in list)
                {
                    var chiTietGioHang = db.ChiTietGioHangs.FirstOrDefault(x => x.MaGioHang == cart.MaGioHang && x.MaSanPham == item.MaSanPham);
                    if (chiTietGioHang != null) chiTietGioHang.TrangThai = 2;   // đã mua
                }
                db.SaveChanges();
                return RedirectToAction("Index", "Cart");
            }
            else
            {
                return RedirectToAction("Index", "Cart");
            }
        }
        else
        {
            return RedirectToAction("Index", "Cart");
        }
        
    }
    
    public ActionResult UpdateCart(int masp)
    {
        KNT_ShopDB db = new KNT_ShopDB();
        string username = Session["username"].ToString();
        // lấy ra giỏ hàng của user
        var cart = db.GioHangs.FirstOrDefault(x => x.TenTaiKhoan == username);
        // lấy ra chi tiết giỏ hàng của san pham
        var chiTietGioHang = db.ChiTietGioHangs.FirstOrDefault(x => x.MaGioHang == cart.MaGioHang && x.MaSanPham == masp);
        chiTietGioHang.TrangThai = (chiTietGioHang.TrangThai == 1)? 0 : 1;
        db.SaveChanges();
        return RedirectToAction("Index", "Cart");
    }

    public ActionResult UpdateSoLuong(int masp, int soLuong)
    {
        KNT_ShopDB db = new KNT_ShopDB();
        string username = Session["username"].ToString();
        // lấy ra giỏ hàng của user
        var cart = db.GioHangs.FirstOrDefault(x => x.TenTaiKhoan == username);
        // lấy ra chi tiết giỏ hàng của san pham
        var chiTietGioHang = db.ChiTietGioHangs.FirstOrDefault(x => x.MaGioHang == cart.MaGioHang && x.MaSanPham == masp);
        chiTietGioHang.SoLuong = soLuong;
        db.SaveChanges();
        return RedirectToAction("Index", "Cart");
    }
}