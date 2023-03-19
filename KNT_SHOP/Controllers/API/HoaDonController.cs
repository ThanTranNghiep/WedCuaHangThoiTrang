using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Web.Http;
using KNT_SHOP.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace KNT_SHOP.Controllers.API;

public class HoaDonController : ApiController
{
    // GET api/<controller>
    [HttpGet]
    public IEnumerable Index()
    {
        KNT_ShopDB db = new KNT_ShopDB();
        var listHoaDon = db.HoaDons.ToList();
        var list = listHoaDon.ToDictionary(x => x.MaHoaDon, x => new
        {
            x.TenTaiKhoan, Date = x.NgayLapHoaDon.Value.ToString("dd/MM/yyyy")
        });
        return list;
    }
    
    [Route ("api/HoaDon/GetAllSanPham")]
    public IEnumerable GetAllSanPham()
    {
        KNT_ShopDB db = new KNT_ShopDB();
        var listSanPham = db.SanPhams.ToList();
        var listGiaBan = db.BangGias.Where(x=>x.MaSanPham == x.SanPham.MaSanPham)
            .OrderByDescending(x=>x.NgayCapNhat).ToList();
        var list = listSanPham.ToDictionary(x => x.MaSanPham, x => new
        {
            TenSanPham = x.TenSanPham,
            SoLuongTon= x.SoLuongTon, 
            HinhAnh = x.HinhAnh,
            GiaBan = listGiaBan.FirstOrDefault(y=>y.MaSanPham == x.MaSanPham)!.GiaBan,
            LoaiSanPham = x.LoaiSanPham.TenLoaiSanPham
        });
        return list;
    }

    [HttpGet]
    [Route("api/HoaDon/GetAllDonHang")]
    public IEnumerable GetAllDonHang()
    {
        KNT_ShopDB db = new KNT_ShopDB();
        var listCtdh = db.ChiTietHoaDons.Where(x => x.TrangThaiGiaoHang >= 0).ToList();
        // Lấy ra tài khoản của đơn hàng
        var listTaiKhoan = db.TaiKhoans.Where(x => x.TenTaiKhoan == x.HoaDons.FirstOrDefault().TenTaiKhoan).ToList();
        var list = listCtdh.GroupBy(x => x.MaHoaDon).ToDictionary(x => x.Key,
            x => x.ToDictionary(y => y.MaSanPham, y => new
            {
                TenTaiKhoan = listTaiKhoan.FirstOrDefault(z => z.TenTaiKhoan == x.FirstOrDefault()?.HoaDon.TenTaiKhoan)!.TenTaiKhoan,
                DiaChiNha = listTaiKhoan.FirstOrDefault(z => z.TenTaiKhoan == x.FirstOrDefault()?.HoaDon.TenTaiKhoan)!.DiaChiNha == null ? "Chưa cập nhật" : listTaiKhoan.FirstOrDefault(z => z.TenTaiKhoan == x.FirstOrDefault().HoaDon.TenTaiKhoan)!.DiaChiNha,
                QuanHuyen = listTaiKhoan.FirstOrDefault(z => z.TenTaiKhoan == x.FirstOrDefault()?.HoaDon.TenTaiKhoan)!.QuanHuyen,
                MaSanPham = y.MaSanPham,
                TenSanPham = y.SanPham.TenSanPham,
                DonGia = y.DonGia,
                SoLuong = y.SoLuong,
                TrangThaiGiaoHang = y.TrangThaiGiaoHang
            }
            ));
    return list;
    }
}