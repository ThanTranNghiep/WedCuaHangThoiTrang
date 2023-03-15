using System.Linq;
using System.Web.Mvc;
using KNT_SHOP.Models;

namespace KNT_SHOP.Controllers;

public class ProductController : Controller
{
    // GET: Product
    public ActionResult Detail(int id)
    {
        KNT_ShopDB db = new KNT_ShopDB();
        var sanPham = db.SanPhams.FirstOrDefault(x => x.MaSanPham== id);
        return View(sanPham);
    }
    public ActionResult Index()
    {
        return View();
    }
}