namespace BanHangAPI1.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietCuaHang")]
    public partial class ChiTietCuaHang
    {
        [Key]
        [Column(Order = 0)]
        public int MaSanPham { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaCuaHang { get; set; }

        public int? SoLuong { get; set; }

        public virtual CuaHang CuaHang { get; set; }
    }
}
