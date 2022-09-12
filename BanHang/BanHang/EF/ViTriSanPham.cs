namespace BanHang.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ViTriSanPham")]
    public partial class ViTriSanPham
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaViTri { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaLo { get; set; }

        public int? SoLuong { get; set; }

        public virtual LoSanPham LoSanPham { get; set; }

        public virtual ViTri ViTri { get; set; }
    }
}
