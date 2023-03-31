using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.UI.WebControls;
using KNT_Shop.Models;
using KNT_SHOP.Models;
using Microsoft.AspNet.Identity;

namespace KNT_SHOP.Controllers.API;

public class AddToCartController : ApiController
{
    [HttpPost]
    [Route("api/AddToCart/{id}")]
    public IHttpActionResult Index(int MaSanPham)
    {
        string username = User.Identity.GetUserId();
        if (username == null)
        {
            return NotFound();
        }
        else
        {
            KNT_ShopDB db = new KNT_ShopDB();
            var cart = db.GioHangs.FirstOrDefault(x => x.Id == username);
            // kiểm tra xem sản phẩm đã có trong giỏ hàng chưa
            var ChiTietGioHang = db.ChiTietGioHangs.FirstOrDefault(x => x.MaSanPham == MaSanPham &&
                                                                        x.MaGioHang == cart.MaGioHang &&
                                                                        x.TrangThai <
                                                                        2); // TrangThai = 0: chua chọn mua
            if (ChiTietGioHang != null)
            {
                ChiTietGioHang.SoLuong += 1;
            }
            else
            {
                db.ChiTietGioHangs.Add(new ChiTietGioHang
                {
                    MaGioHang = cart.MaGioHang,
                    MaSanPham = MaSanPham,
                    SoLuong = 1,
                    TrangThai = 0
                });
            }
    
            db.SaveChanges();
            return Ok();
        }
        
    }
    
}