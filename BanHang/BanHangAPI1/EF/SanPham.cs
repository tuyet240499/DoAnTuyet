namespace BanHangAPI1.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [Key]
        public int MaSanPham { get; set; }

        [StringLength(10)]
        public string MaVach { get; set; }

        public int? MaLo { get; set; }

        public int? ViTriHienTai { get; set; }

        public virtual LoSanPham LoSanPham { get; set; }
    }
}
