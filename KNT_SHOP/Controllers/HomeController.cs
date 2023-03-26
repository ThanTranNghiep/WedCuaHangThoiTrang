using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using KNT_SHOP.Models;
using KNT_SHOP.Models.ViewModel;

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
                var chiTietHoaDons = db.ChiTietHoaDons.Distinct().GroupBy(x=>x.MaSanPham).ToList();
                if(chiTietHoaDons.Count == 0)
                    return View();
                else
                {
                    var sanPham = db.SanPhams.Where(x=>x.MaSanPham == x.ChiTietHoaDons.FirstOrDefault().MaSanPham).ToList();
                    var listGiaBan = db.BangGias.Where(x => x.MaSanPham == x.SanPham.MaSanPham)
                        .OrderByDescending(x => x.NgayCapNhat).ToList();
                    var listSanPham = new List<SanPhamModel>(
                        sanPham.Select(
                            x => new SanPhamModel()
                            {
                                Check = 0,
                                MaSanPham = x.MaSanPham,
                                HinhAnh = x.HinhAnh,
                                TenSanPham = x.TenSanPham,
                                SoLuong = x.SoLuongTon,
                                Gia = listGiaBan.FirstOrDefault(y => y.MaSanPham == x.MaSanPham).GiaBan
                            })
                    );
                    return View(listSanPham);
                }
            }
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}