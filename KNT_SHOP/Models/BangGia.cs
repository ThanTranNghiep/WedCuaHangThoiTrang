namespace KNT_Shop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BangGia")]
    public partial class BangGia
    {
        [Key]
        public int MaGia { get; set; }

        [Column(TypeName = "money")]
        public decimal GiaBan { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayCapNhat { get; set; }

        public int MaSanPham { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
