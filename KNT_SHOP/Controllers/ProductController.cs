using System;
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
        var sanPham = db.SanPhams.FirstOrDefault(x => x.MaSanPham == id);
        return View(sanPham);
    }

    public ActionResult AddToCart(int id)
    {
        var username = Session["username"] as string;
        if (username == null)
        {
            JavaScript("alert('Bạn phải đăng nhập để thực hiện chức năng này!');");
            RedirectToAction("Index", "Login");
        }
        else
        {
            KNT_ShopDB db = new KNT_ShopDB();
            var cart = db.GioHangs.FirstOrDefault(x => x.TenTaiKhoan == username);
            // kiểm tra xem sản phẩm đã có trong giỏ hàng chưa
            var ChiTietGioHang = db.ChiTietGioHangs.FirstOrDefault(x => x.MaSanPham == id &&
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
                    MaSanPham = id,
                    SoLuong = 1,
                    TrangThai = 0
                });
            }

            db.SaveChanges();
        }

        return RedirectToAction("SanPham", "Product");
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
        var listGiaBan = db.BangGias.Where(x => x.MaSanPham == x.SanPham.MaSanPham)
            .OrderByDescending(x => x.NgayCapNhat).ToList();
        if (taiKhoan != null)
        {
            ViewBag.Rule = (taiKhoan.Rule == true) ? "Admin" : "User";
            ViewBag.SanPham = sanPham;
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
        var listGiaBan = db.BangGias.Where(x => x.MaSanPham == x.SanPham.MaSanPham)
            .OrderByDescending(x => x.NgayCapNhat).ToList();
        if (taiKhoan != null)
        {
            ViewBag.Rule = (taiKhoan.Rule == true) ? "Admin" : "User";
            ViewBag.SanPham = sanPham;
            ViewBag.ListGiaBan = listGiaBan;
            return View("SanPham", sanPham);
        }

        return RedirectToAction("Index", "Login");
    }
    
    public ActionResult Edit(int id)
    {
        KNT_ShopDB db = new KNT_ShopDB();
        var sanPham = db.SanPhams.FirstOrDefault(x => x.MaSanPham == id);
        return View(sanPham);
    }

    [HttpPost]
    public ActionResult Edit(SanPham sanPham)
    {
        KNT_ShopDB db = new KNT_ShopDB();
        var SanPham = db.SanPhams.FirstOrDefault(x => x.MaSanPham == sanPham.MaSanPham);
        SanPham.TenSanPham = sanPham.TenSanPham;
        SanPham.HinhAnh = sanPham.HinhAnh;
        SanPham.ThongTinSanPham = sanPham.ThongTinSanPham;
        SanPham.SoLuongTon = sanPham.SoLuongTon;
        SanPham.ThongTinSanPham = sanPham.ThongTinSanPham;
        SanPham.status = sanPham.status;
        db.SaveChanges();
        return View(SanPham);
    }

    public ActionResult AddPrice(int id)
    {
        KNT_ShopDB db = new KNT_ShopDB();
        var bangGia = db.BangGias.FirstOrDefault(x => x.MaSanPham == id);
        if (bangGia != null)
        {
            return View(bangGia);
        }
        else
        {
            BangGia giaNull = new BangGia();
            return View(giaNull);
        }
    }
    
    public ActionResult AddNewPrice(int id, decimal price)
    {
        KNT_ShopDB db = new KNT_ShopDB();
        var bangGia = db.BangGias.FirstOrDefault(x => x.MaSanPham == id);
        if (bangGia != null)
        {
            // bảng giá có tồn tại ngày hiện tại thì update giá
            var giaBan = db.BangGias.FirstOrDefault(x => x.MaSanPham == id && x.NgayCapNhat.Value.Day == DateTime.Now.Day &&
                                                        x.NgayCapNhat.Value.Month == DateTime.Now.Month &&
                                                        x.NgayCapNhat.Value.Year == DateTime.Now.Year);
            if (giaBan != null)
            {
                giaBan.GiaBan = price;
                db.SaveChanges();
                return View("AddPrice",bangGia);
            }
            else
            {
                BangGia gia = new BangGia();
                gia.MaSanPham = id;
                gia.GiaBan = price;
                gia.NgayCapNhat = DateTime.Now;
                db.BangGias.Add(gia);
                db.SaveChanges();
                return View("AddPrice",bangGia);
            }
        }
        else
        {
            BangGia giaNull = new BangGia();
            return View("AddPrice",giaNull);
        }
    }

    // [HttpPost]
    // public ActionResult AddPrice(BangGia gia)
    // {
    //     KNT_ShopDB db = new KNT_ShopDB();
    //     var bangGia = db.BangGias.FirstOrDefault(x => x.MaGia == gia.MaGia);
    //     if (bangGia != null)
    //     {
    //         bangGia.GiaBan = gia.GiaBan;
    //         bangGia.NgayCapNhat = DateTime.Now;
    //         db.SaveChanges();
    //         return View(bangGia);
    //     }
    //     else
    //     {
    //         BangGia giaNull = new BangGia();
    //         return View(giaNull);
    //     }
    // }
}