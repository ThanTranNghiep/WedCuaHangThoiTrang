namespace KNT_SHOP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiKhoan")]
    public partial class TaiKhoan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaiKhoan()
        {
            GioHangs = new HashSet<GioHang>();
            HoaDons = new HashSet<HoaDon>();
        }

        [Key]
        [StringLength(10)]
        public string TenTaiKhoan { get; set; }

        [Required]
        [StringLength(50)]
        public string MatKhau { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayTao { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(11)]
        public string SĐT { get; set; }

        public int GioiTinh { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayThangNamSinh { get; set; }

        [StringLength(10)]
        public string MaQuan_Huyen { get; set; }

        [StringLength(200)]
        public string DiaChiNha { get; set; }

        public bool? Rule { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GioHang> GioHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon> HoaDons { get; set; }

        public virtual QuanHuyen QuanHuyen { get; set; }
    }
}
