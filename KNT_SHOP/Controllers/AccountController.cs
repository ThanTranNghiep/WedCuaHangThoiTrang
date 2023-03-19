using System;
using System.Linq;
using System.Web.Mvc;
using KNT_SHOP.Models;

namespace KNT_SHOP.Controllers;

public class AccountController : Controller
{
    // GET
    public ActionResult Index()
    {
        var username = Session["username"];
        if (username == null)
        {
            return RedirectToAction("Index", "Login");
        }
        else
        {
            KNT_ShopDB db = new KNT_ShopDB();
            var user = db.TaiKhoans.FirstOrDefault(x => x.TenTaiKhoan.Trim() == username.ToString().Trim());
            return View(user);
        }
    }

    public ActionResult EditAccount(TaiKhoan taiKhoan)
    {
        KNT_ShopDB db = new KNT_ShopDB();
        var user = db.TaiKhoans.FirstOrDefault(x => x.TenTaiKhoan.Trim() == taiKhoan.TenTaiKhoan.Trim());
        if (user != null)
        {
            user.MatKhau = taiKhoan.MatKhau;
            user.QuanHuyen = taiKhoan.QuanHuyen;
            user.SĐT = taiKhoan.SĐT;
            user.NgayThangNamSinh = taiKhoan.NgayThangNamSinh;
            user.DiaChiNha = taiKhoan.DiaChiNha;
            db.SaveChanges();
            return RedirectToAction("SanPham", "Home");
        }
        return RedirectToAction("Index", "Invoice");
    }
}