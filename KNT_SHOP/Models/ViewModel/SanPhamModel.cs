using System;
using System;
using System.Collections;
using System.Collections.Generic;
namespace KNT_SHOP.Models.ViewModel;

public class SanPhamModel
{
    public int Check { get; set; }
    public int MaSanPham{ get; set; }
    public string TenSanPham { get; set; }
    public string HinhAnh { get; set; }
    public decimal Gia { get; set; }
    public int SoLuong { get; set; }
    
    public virtual LoaiSanPham LoaiSanPham { get; set; }
    
    public string DiaChi { get; set; }
    
    // private int check;
    // private int maSanPham;
    // private string tenSanPham;
    // private string hinhAnh;
    // private decimal gia;
    // private int soLuong;
    // private string diaChi;
    //
    // public SanPhamModel()
    // {
    // }
    //
    // public SanPhamModel(int check, int maSanPham, string tenSanPham, string hinhAnh, decimal gia, int soLuong, string diaChi)
    // {
    //     this.check = check;
    //     this.maSanPham = maSanPham;
    //     this.tenSanPham = tenSanPham;
    //     this.hinhAnh = hinhAnh;
    //     this.gia = gia;
    //     this.soLuong = soLuong;
    //     this.diaChi = diaChi;
    // }
    //
    // public int Check
    // {
    //     get => check;
    //     set => check = value;
    // }
    //
    // public int MaSanPham
    // {
    //     get => maSanPham;
    //     set => maSanPham = value;
    // }
    //
    // public string TenSanPham
    // {
    //     get => tenSanPham;
    //     set => tenSanPham = value;
    // }
    //
    // public string HinhAnh
    // {
    //     get => hinhAnh;
    //     set => hinhAnh = value;
    // }
    //
    // public decimal Gia
    // {
    //     get => gia;
    //     set => gia = value;
    // }
    //
    // public int SoLuong
    // {
    //     get => soLuong;
    //     set => soLuong = value;
    // }
    //
    // public string DiaChi
    // {
    //     get => diaChi;
    //     set => diaChi = value;
    // }
    //
    // public IEnumerator GetEnumerator()
    // {
    //     throw new NotImplementedException();
    // }
}