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
            var listSanPham = db.ChiTietGioHangs.Where(x => x.MaGioHang == cart.MaGioHang && x.TrangThai == 0).ToList();
            // lấy bảng giá của sản phẩm
            var listGiaBan = db.BangGias.Where(x => x.MaSanPham == x.SanPham.MaSanPham)
                .OrderByDescending(x => x.NgayCapNhat).ToList();


            List<SanPhamModel> listSanPhamModel = new List<SanPhamModel>(
                listSanPham.Select(x => new SanPhamModel()
                {
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
}