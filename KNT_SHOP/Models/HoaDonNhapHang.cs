namespace KNT_SHOP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDonNhapHang")]
    public partial class HoaDonNhapHang
    {
        [Key]
        [StringLength(10)]
        public string MaHDN { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayNhapHang { get; set; }

        public virtual ChiTiet_NhapHang ChiTiet_NhapHang { get; set; }
    }
}
