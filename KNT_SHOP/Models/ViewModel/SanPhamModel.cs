namespace KNT_Shop.Models.ViewModel;

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
}