using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Web.Http.Results;
using System.Web.Mvc;
using KNT_SHOP.Models;
using KNT_SHOP.Models.ViewModel;

namespace KNT_SHOP.Controllers;

public class InvoiceController : Controller
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
            var username = Session["username"].ToString();
            var checkAdmin = db.TaiKhoans.FirstOrDefault(x => x.TenTaiKhoan == username);
            if(checkAdmin.Rule == true)
            {
                var list = db.HoaDons.ToList();
                return View(list);
            }
            else
            {
                var list = db.HoaDons.Where(x => x.TenTaiKhoan == username).ToList();
                return View(list);
            }
        }
    }
    
    public ActionResult Details(int id)
    {
        KNT_ShopDB db = new KNT_ShopDB();
        var hoaDon = db.HoaDons.FirstOrDefault(x => x.MaHoaDon == id);
        return View(hoaDon);
    }
}