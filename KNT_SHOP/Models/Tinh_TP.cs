namespace KNT_SHOP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tinh_TP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tinh_TP()
        {
            QuanHuyens = new HashSet<QuanHuyen>();
        }

        [Key]
        [StringLength(10)]
        public string MaTinh_ThanhPho { get; set; }

        [Required]
        [StringLength(200)]
        public string TenTinh_ThanhPho { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuanHuyen> QuanHuyens { get; set; }
    }
}
