namespace KNT_Shop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ChiTiet_NhapHang
    {
        [Key]
        [StringLength(10)]
        public string MaHDN { get; set; }

        public int MaSanPham { get; set; }

        public int SoLuong { get; set; }

        [Column(TypeName = "money")]
        public decimal DonGia { get; set; }

        public virtual SanPham SanPham { get; set; }

        public virtual HoaDonNhapHang HoaDonNhapHang { get; set; }
    }
}
