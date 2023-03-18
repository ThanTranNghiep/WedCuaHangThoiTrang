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
    public ActionResult AddToCart(int id)
    {
        var username = Session["username"] as string;
        if(username == null)
        {
            JavaScript("alert('Bạn phải đăng nhập để thực hiện chức năng này!');");
            RedirectToAction("Index", "Login");
        }
        else
        {
            KNT_ShopDB db = new KNT_ShopDB();
            var cart = db.GioHangs.FirstOrDefault(x => x.TenTaiKhoan == username);
            // kiểm tra xem sản phẩm đã có trong giỏ hàng chưa
            var ChiTietGioHang = db.ChiTietGioHangs.
                                    FirstOrDefault(x => x.MaSanPham == id && 
                                                        x.MaGioHang == cart.MaGioHang &&        
                                                        x.TrangThai < 2);                  // TrangThai = 0: chua chọn mua
            if(ChiTietGioHang != null)
            {
                ChiTietGioHang.SoLuong += 1;
            }
            else
            {
                db.ChiTietGioHangs.Add(new ChiTietGioHang
                {
                    MaGioHang = cart.MaGioHang,
                    MaSanPham = id,
                    SoLuong = 1,
                    TrangThai = 0
                });
            }
            db.SaveChanges();
        }
        return RedirectToAction("SanPham", "Home");
    }
    
}