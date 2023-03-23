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