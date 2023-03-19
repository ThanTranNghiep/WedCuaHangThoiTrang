using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using KNT_SHOP.Models;

namespace KNT_SHOP.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            string token = Session["token"] as string;
            string username = Session["username"] as string;
            KNT_ShopDB db = new KNT_ShopDB();
            TaiKhoan taiKhoan = db.TaiKhoans.FirstOrDefault(x => x.TenTaiKhoan == username);

            if (token == null || taiKhoan == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.Message = "Your application description page.";
                // string url = Request.Url.AbsolutePath.Split("/".ToCharArray()).Last();
                return View();
            }
        }

        [HttpGet]
        public ActionResult SanPham()
        {
            string token = Session["token"] as string;
            string username = Session["username"] as string;
            KNT_ShopDB db = new KNT_ShopDB();
            TaiKhoan taiKhoan = db.TaiKhoans.FirstOrDefault(x => x.TenTaiKhoan == username);
            var sanPham = db.SanPhams.ToList();
            
            if (taiKhoan != null)
            {
                ViewBag.Rule = (taiKhoan.Rule == true) ? "Admin" : "User";
                return View(sanPham);
            }
            return RedirectToAction("Index", "Login");
        }
        [HttpGet]
        public ActionResult ListSanPham()
        {
            string token = Session["token"] as string;
            string username = Session["username"] as string;
            KNT_ShopDB db = new KNT_ShopDB();
            TaiKhoan taiKhoan = db.TaiKhoans.FirstOrDefault(x => x.TenTaiKhoan == username);
            var sanPham = db.SanPhams.ToList();
            var listGiaBan = db.BangGias.Where(x=>x.MaSanPham == x.SanPham.MaSanPham)
                .OrderByDescending(x=>x.NgayCapNhat).ToList();
            if (taiKhoan != null)
            {
                ViewBag.Rule = (taiKhoan.Rule == true) ? "Admin" : "User";
                ViewBag.SanPham = sanPham ;
                ViewBag.ListGiaBan = listGiaBan;
                return View();
            }
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        public ActionResult Search(string keyword)
        {
            string token = Session["token"] as string;
            string username = Session["username"] as string;
            KNT_ShopDB db = new KNT_ShopDB();
            TaiKhoan taiKhoan = db.TaiKhoans.FirstOrDefault(x => x.TenTaiKhoan == username);
            var sanPham = db.SanPhams.Where(x => x.TenSanPham.Contains(keyword)).ToList();
            var listGiaBan = db.BangGias.Where(x=>x.MaSanPham == x.SanPham.MaSanPham)
                .OrderByDescending(x=>x.NgayCapNhat).ToList();
            if (taiKhoan != null)
            {
                ViewBag.Rule = (taiKhoan.Rule == true) ? "Admin" : "User";
                ViewBag.SanPham = sanPham ;
                ViewBag.ListGiaBan = listGiaBan;
                return View("SanPham",sanPham);
            }
            return RedirectToAction("Index", "Login");
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}