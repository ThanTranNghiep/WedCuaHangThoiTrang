using System.Linq;
using System.Web.Mvc;
using KNT_SHOP.Models;

namespace KNT_SHOP.Controllers;

public class ProductController : Controller
{
    // POST: Product
    [HttpPost]
    public ActionResult Detail(string id)
    {
        KNT_ShopDB db = new KNT_ShopDB();
        var sanPham = db.SanPhams.FirstOrDefault(x => x.MaSanPham.CompareTo(id) == 0);
        return View(sanPham);
    }
}