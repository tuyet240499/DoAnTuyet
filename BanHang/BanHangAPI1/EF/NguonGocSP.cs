namespace BanHangAPI1.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NguonGocSP")]
    public partial class NguonGocSP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NguonGocSP()
        {
            LoSanPhams = new HashSet<LoSanPham>();
        }

        [Key]
        public int MaNguonGoc { get; set; }

        public DateTime? ThoiGianVanChuyen { get; set; }

        [StringLength(100)]
        public string DonVIVanChuyen { get; set; }

        [StringLength(50)]
        public string NuocSX { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LoSanPham> LoSanPhams { get; set; }
    }
}
