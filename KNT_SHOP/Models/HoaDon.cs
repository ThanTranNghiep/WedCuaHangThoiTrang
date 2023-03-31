namespace KNT_Shop.Models
{
    using KNT_Shop.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDon")]
    public partial class HoaDon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoaDon()
        {
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
        }

        [Key]
        public int MaHoaDon { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayLapHoaDon { get; set; }

        [Required]
        [StringLength(200)]
        //public string TenTaiKhoan { get; set; }
        public string Id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }

        //public virtual TaiKhoan TaiKhoan { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
