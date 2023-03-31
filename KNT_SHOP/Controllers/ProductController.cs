using System;
using System.Linq;
using System.Web.Mvc;
using KNT_Shop.Models;
using KNT_Shop.Models.ViewModel;
using Microsoft.AspNet.Identity;

namespace KNT_Shop.Controllers;

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
        string username = User.Identity.GetUserId();
        if (username == null)
        {
            JavaScript("alert('Bạn phải đăng nhập để thực hiện chức năng này!');");
            RedirectToAction("Login", "Account");
        }
        else
        {
            KNT_ShopDB db = new KNT_ShopDB();
            var cart = db.GioHangs.FirstOrDefault(x => x.Id == username);
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
        string username = User.Identity.GetUserId();
        KNT_ShopDB db = new KNT_ShopDB();
        ApplicationUser taiKhoan = db.Users.FirstOrDefault(x => x.Id == username);
        var sanPham = db.SanPhams.ToList();

        if (taiKhoan != null)
        {
            ViewBag.Rule = (taiKhoan.Rule == true) ? "Admin" : "User";
            return View(sanPham);
        }

        return RedirectToAction("Login", "Account");
    }

    [HttpGet]
    public ActionResult ListSanPham()
    {
        string username = User.Identity.GetUserId();
        KNT_ShopDB db = new KNT_ShopDB();
        ApplicationUser taiKhoan = db.Users.FirstOrDefault(x => x.Id == username);
        var sanPham = db.SanPhams.ToList();
        var listGiaBan = db.BangGias.Where(x => x.MaSanPham == x.SanPham.MaSanPham)
            .OrderByDescending(x => x.NgayCapNhat).ToList();
        if (taiKhoan != null)
        {
            ViewBag.Rule = (taiKhoan.Rule == true) ? "Admin" : "User";
            return View(sanPham);
        }

        return RedirectToAction("Login", "Account");
    }

    [HttpPost]
    public ActionResult Search(string keyword)
    {
        string username = User.Identity.GetUserId();
        KNT_ShopDB db = new KNT_ShopDB();
        ApplicationUser taiKhoan = db.Users.FirstOrDefault(x => x.Id == username);
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

        return RedirectToAction("Login", "Account");
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
    
    [HttpPost]
    public ActionResult AddNewPrice(int MaGia, decimal GiaBan)
    {
        KNT_ShopDB db = new KNT_ShopDB();
        var bangGia = db.BangGias.FirstOrDefault(x => x.MaSanPham == MaGia);
        if (bangGia != null)
        {
            // bảng giá có tồn tại ngày hiện tại thì update giá
            var giaBan = db.BangGias.FirstOrDefault(x => x.MaSanPham == MaGia && x.NgayCapNhat.Value.Day == DateTime.Now.Day &&
                                                         x.NgayCapNhat.Value.Month == DateTime.Now.Month &&
                                                         x.NgayCapNhat.Value.Year == DateTime.Now.Year);
            if (giaBan != null)
            {
                giaBan.GiaBan = GiaBan;
                db.SaveChanges();
                return View("AddPrice",bangGia);
            }
            else
            {
                BangGia gia = new BangGia();
                gia.MaSanPham = MaGia;
                gia.GiaBan = GiaBan;
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
    public ActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Add(SanPhamModel sanPham)
    {
        if (sanPham != null)
        {
            KNT_ShopDB db = new KNT_ShopDB();
            SanPham sp = new SanPham()
            {
                TenSanPham = sanPham.TenSanPham,
                HinhAnh = sanPham.HinhAnh,
                SoLuongTon = sanPham.SoLuong,
                LoaiSanPham = sanPham.LoaiSanPham
            };
            db.SanPhams.Add(sp);
            db.SaveChanges();
        }
        return View("SanPham");
    }
}