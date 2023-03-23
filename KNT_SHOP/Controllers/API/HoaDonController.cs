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
    [AllowCrossSiteJson]
    public IEnumerable GetAllDonHang()
    {


        KNT_ShopDB db = new KNT_ShopDB();
        var listCtdh = db.ChiTietHoaDons.Where(x => x.TrangThaiGiaoHang >= 0).ToList();
        // Lấy ra tài khoản của đơn hàng
        // var listTaiKhoan = db.TaiKhoans.Where(x => x.TenTaiKhoan == x.HoaDons.FirstOrDefault().TenTaiKhoan).ToList();
        var listTaiKhoan = db.HoaDons.GroupBy(x=>x.TenTaiKhoan).Select(x=>x.FirstOrDefault()).ToList();
        var list = listCtdh.GroupBy(x => x.MaHoaDon).ToDictionary(x => x.Key,
            x => x.ToDictionary(y => y.MaSanPham, y => new
                {
                    TenTaiKhoan = listTaiKhoan.FirstOrDefault(z => z.TenTaiKhoan == x.FirstOrDefault()?.HoaDon.TenTaiKhoan)!.TenTaiKhoan,
                    DiaChiNha = listTaiKhoan.FirstOrDefault(z => z.TenTaiKhoan == x.FirstOrDefault()?.HoaDon.TenTaiKhoan)!.TaiKhoan.DiaChiNha,
                    MaSanPham = y.MaSanPham,
                    TenSanPham = y.SanPham.TenSanPham,
                    NgayDatHang = y.HoaDon.NgayLapHoaDon,
                    Email = listTaiKhoan.FirstOrDefault(z => z.TenTaiKhoan == x.FirstOrDefault()?.HoaDon.TenTaiKhoan)!.TaiKhoan.Email,
                    SDT = listTaiKhoan.FirstOrDefault(z => z.TenTaiKhoan == x.FirstOrDefault()?.HoaDon.TenTaiKhoan)!.TaiKhoan.SĐT,
                    MaQuanHuyen = listTaiKhoan.FirstOrDefault(z => z.TenTaiKhoan == x.FirstOrDefault()?.HoaDon.TenTaiKhoan)!.TaiKhoan.MaQuan_Huyen,
                    DonGia = y.DonGia,
                    SoLuong = y.SoLuong,
                    TrangThaiGiaoHang = y.TrangThaiGiaoHang
                }
            ));
        
        // var list = listCtdh.GroupBy(x => x.MaHoaDon).ToDictionary(x => x.Key,
        //     x => x.ToDictionary(y => y.MaSanPham, y => new
        //     {
        //         TenTaiKhoan = listTaiKhoan.FirstOrDefault(z => z.TenTaiKhoan == x.FirstOrDefault()?.HoaDon.TenTaiKhoan)!.TenTaiKhoan,
        //         DiaChiNha = listTaiKhoan.FirstOrDefault(z => z.TenTaiKhoan == x.FirstOrDefault()?.HoaDon.TenTaiKhoan)!.DiaChiNha == null ? "Chưa cập nhật" : listTaiKhoan.FirstOrDefault(z => z.TenTaiKhoan == x.FirstOrDefault().HoaDon.TenTaiKhoan)!.DiaChiNha,
        //         MaSanPham = y.MaSanPham,
        //         TenSanPham = y.SanPham.TenSanPham,
        //         NgayDatHang = y.HoaDon.NgayLapHoaDon,
        //         Email = listTaiKhoan.FirstOrDefault(z => z.TenTaiKhoan == x.FirstOrDefault()?.HoaDon.TenTaiKhoan)!.Email,
        //         SDT = listTaiKhoan.FirstOrDefault(z => z.TenTaiKhoan == x.FirstOrDefault()?.HoaDon.TenTaiKhoan)!.SĐT,
        //         MaQuanHuyen = listTaiKhoan.FirstOrDefault(z => z.TenTaiKhoan == x.FirstOrDefault()?.HoaDon.TenTaiKhoan)!.MaQuan_Huyen,
        //         DonGia = y.DonGia,
        //         SoLuong = y.SoLuong,
        //         TrangThaiGiaoHang = y.TrangThaiGiaoHang
        //     }
        //     ));
        return list.SelectMany(x => x.Value.Select(y => new {
            MaHoaDon = x.Key,
            MaSanPham = y.Key,
            TenTaiKhoan = y.Value.TenTaiKhoan,
            DiaChiNha = y.Value.DiaChiNha,
            NgayDatHang = y.Value.NgayDatHang,
            Email = y.Value.Email,
            SDT = y.Value.SDT,
            MaQuanHuyen = y.Value.MaQuanHuyen,
            TenSanPham = y.Value.TenSanPham,
            DonGia = y.Value.DonGia,
            SoLuong = y.Value.SoLuong,
            TrangThaiGiaoHang = y.Value.TrangThaiGiaoHang
        })).ToList(); ;
    }
}