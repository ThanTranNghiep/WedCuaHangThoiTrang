namespace KNT_Shop.Models
{
    using KNT_Shop.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GioHang")]
    public partial class GioHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GioHang()
        {
            ChiTietGioHangs = new HashSet<ChiTietGioHang>();
        }

        [Key]
        public int MaGioHang { get; set; }

        [Required]
        [StringLength(200)]
        //public string TenTaiKhoan { get; set; }
        public string Id { get; set;}

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; }

        //public virtual TaiKhoan TaiKhoan { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
