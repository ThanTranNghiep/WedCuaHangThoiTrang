using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using KNT_Shop.Models;
using KNT_Shop.Models.ViewModel;
using Microsoft.AspNet.Identity;

namespace KNT_Shop.Controllers;

public class CartController : Controller
{
    // GET
    public ActionResult Index()
    {
        var username = User.Identity.GetUserId();
        if (username == null)
        {
            JavaScript("alert('Please login first!');");
            return RedirectToAction("Login", "Account");
        }
        else
        {
            KNT_ShopDB db = new KNT_ShopDB();
            // lấy ra giỏ hàng của user
            var cart = db.GioHangs.FirstOrDefault(x => x.Id == username);
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
            // ViewBag.ListSanPham = listSanPhamModel;
            return View(listSanPhamModel);
        }
    }
    
    public ActionResult Invoice()
    {
        var username = User.Identity.GetUserId();
        if (username != null)
        {
            KNT_ShopDB db = new KNT_ShopDB();
            var cart = db.GioHangs.FirstOrDefault(x => x.Id == username);
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
                    Id = username,
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
                    
                    // update số lượng sản phẩm
                    var sanPham = db.SanPhams.FirstOrDefault(x => x.MaSanPham == item.MaSanPham);
                    sanPham.SoLuongTon -= item.SoLuong;
                }
                // Save hóa đơn và chi tiết hóa đơn
                db.HoaDons.Add(hoaDon);
                db.SaveChanges();
            
                // Update trạng thái của sản phẩm trong giỏ hàng
                foreach (var item in list)
                {
                    var chiTietGioHang = db.ChiTietGioHangs.FirstOrDefault(x => x.MaGioHang == cart.MaGioHang && x.MaSanPham == item.MaSanPham && x.TrangThai < 2);
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
        var username = User.Identity.GetUserId();
        // lấy ra giỏ hàng của user
        var cart = db.GioHangs.FirstOrDefault(x => x.Id == username);
        // lấy ra chi tiết giỏ hàng của san pham
        var chiTietGioHang = db.ChiTietGioHangs.FirstOrDefault(x => x.MaGioHang == cart.MaGioHang && x.MaSanPham == masp && x.TrangThai < 2);
        if (chiTietGioHang != null)
        {
            chiTietGioHang.TrangThai = (chiTietGioHang.TrangThai == 1) ? 0 :
                (chiTietGioHang.TrangThai == 0) ? 1 : chiTietGioHang.TrangThai;
            db.SaveChanges();
        }
        return RedirectToAction("Index", "Cart");
    }

    public ActionResult StepUp(int masp)
    {
        KNT_ShopDB db = new KNT_ShopDB();
        var username = User.Identity.GetUserId();
        // lấy ra giỏ hàng của user
        var cart = db.GioHangs.FirstOrDefault(x => x.Id == username);
        // lấy ra chi tiết giỏ hàng của san pham
        var chiTietGioHang = db.ChiTietGioHangs.FirstOrDefault(x => x.MaGioHang == cart.MaGioHang && x.MaSanPham == masp && x.TrangThai < 2);
        if (chiTietGioHang != null) chiTietGioHang.SoLuong = chiTietGioHang.SoLuong + 1;
        db.SaveChanges();
        return RedirectToAction("Index", "Cart");
    }
    
    public ActionResult StepDown(int masp)
    {
        KNT_ShopDB db = new KNT_ShopDB();
        var username = User.Identity.GetUserId();
        // lấy ra giỏ hàng của user
        var cart = db.GioHangs.FirstOrDefault(x => x.Id == username);
        // lấy ra chi tiết giỏ hàng của san pham
        var chiTietGioHang = db.ChiTietGioHangs.FirstOrDefault(x => x.MaGioHang == cart.MaGioHang && x.MaSanPham == masp && x.TrangThai < 2);
        if (chiTietGioHang != null && chiTietGioHang.SoLuong > 1)
        {
            chiTietGioHang.SoLuong = chiTietGioHang.SoLuong - 1;
            db.SaveChanges();
        }
        return RedirectToAction("Index", "Cart");
    }

    public ActionResult DeleteSanPham(int masp)
    {
        KNT_ShopDB db = new KNT_ShopDB();
        var username = User.Identity.GetUserId();
        // lấy ra giỏ hàng của user
        var cart = db.GioHangs.FirstOrDefault(x => x.Id == username);
        // lấy ra chi tiết giỏ hàng của san pham
        var chiTietGioHang = db.ChiTietGioHangs.FirstOrDefault(x => x.MaGioHang == cart.MaGioHang && x.MaSanPham == masp && x.TrangThai < 2);
        if (chiTietGioHang != null)  db.ChiTietGioHangs.Remove(chiTietGioHang);
        db.SaveChanges();
        return RedirectToAction("Index", "Cart");
    }
}