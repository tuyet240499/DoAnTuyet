namespace BanHang.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ViTriKhachHang")]
    public partial class ViTriKhachHang
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaViTri { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaKhachHang { get; set; }

        public DateTime? ThoiGian { get; set; }

        public virtual KhachHang KhachHang { get; set; }

        public virtual ViTri ViTri { get; set; }
    }
}
