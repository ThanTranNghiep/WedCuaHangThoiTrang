using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using KNT_SHOP.Models;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using KNT_SHOP.Models.Cryptography;
using System.Web.Helpers;
using KNT_SHOP.Models.ViewModel;

namespace KNT_SHOP.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public void initSession(string username, int gioiTinh, string ngayTao, string email, string sdt, string diaChi, bool rule)
        {
            Session["username"] = username;
            Session["gioiTinh"] = gioiTinh;
            Session["ngayTao"] = ngayTao;
            Session["email"] = email;
            Session["sdt"] = sdt;
            Session["diaChi"] = diaChi;
            Session["rule"] = rule;
            MD5Encrypt md5 = new MD5Encrypt(username);
            string token = md5.MaHoa;
            Session["token"] = token;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost,ActionName("Index")]
        public ActionResult Login(string username, string password)
        {
            KNT_ShopDB db = new KNT_ShopDB();
            var TaiKhoan = db.TaiKhoans.FirstOrDefault(x => x.TenTaiKhoan == username && x.MatKhau == password);
            if (TaiKhoan != null)
            {
                Session["username"] = TaiKhoan.TenTaiKhoan;
                //SessionModel
                bool ? rule = TaiKhoan.Rule;
                if(rule == true)
                {
                    initSession(TaiKhoan.TenTaiKhoan, TaiKhoan.GioiTinh, TaiKhoan.NgayTao.ToString(), TaiKhoan.Email, TaiKhoan.SĐT, TaiKhoan.DiaChiNha, true);
                }    
                else
                {
                    initSession(TaiKhoan.TenTaiKhoan, TaiKhoan.GioiTinh, TaiKhoan.NgayTao.ToString(), TaiKhoan.Email, TaiKhoan.SĐT, TaiKhoan.DiaChiNha, false);
                }                    
                ViewBag.Token = TaiKhoan.TenTaiKhoan;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        
        public ActionResult Register()
        {
            ViewBag.Message = "";
            return View();
        }

        [HttpPost, ActionName("Register")]
        public ActionResult RegisterAccount(string username, string email, string gender, string password, string confirm)
        {
            KNT_ShopDB db = new KNT_ShopDB();
            var TaiKhoan = db.TaiKhoans.FirstOrDefault(x => x.TenTaiKhoan == username);
            if (TaiKhoan == null)
            {
                TaiKhoan = new TaiKhoan();
                TaiKhoan.TenTaiKhoan = username;
                TaiKhoan.Email = email;
                TaiKhoan.MatKhau = password;
                TaiKhoan.NgayTao = DateTime.Now;
                TaiKhoan.GioiTinh = (gender == "Male") ? 1 : 0;
                TaiKhoan.Rule = false;
                db.TaiKhoans.Add(TaiKhoan);
                db.SaveChanges();
                var GioHang = new GioHang();
                GioHang.TenTaiKhoan = username;
                db.GioHangs.Add(GioHang);
                db.SaveChanges();
                ViewBag.Message = "Đăng ký thành công";
                return RedirectToAction("Index","Home");
            }
            else
            {
                ViewBag.Message = "Tên tài khoản đã tồn tại";
                return View("Register");
            }
        }
    }
}