using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using KNT_Shop.Models;
using KNT_SHOP.Models.Chart;
using Newtonsoft.Json;

namespace KNT_Shop.Controllers;

public class ChartController : Controller
{
    // GET
    public ActionResult Index()
    {
        KNT_ShopDB db = new KNT_ShopDB();
        var listNgayLapHoaDon = db.HoaDons.Select(x => x.NgayLapHoaDon).Distinct().ToList();
        List<Point> data = new List<Point>();
        foreach (var item in listNgayLapHoaDon)
        {
            var tongTien = db.HoaDons.Where(x=>x.NgayLapHoaDon == item)
                    .Sum(x => x.ChiTietHoaDons
                        .Sum(y=>y.SoLuong * y.DonGia));
            data.Add(new Point(item.ToString().Split(' ')[0], tongTien));
        }
        
        ViewBag.DataPoints = JsonConvert.SerializeObject(data);
        
        return View();
    }
}