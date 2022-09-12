namespace BanHangAPI1.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GioHang")]
    public partial class GioHang
    {
        [Key]
        public int MaGioHang { get; set; }

        public int? MaSanPham { get; set; }

        public int? MaKhachHang { get; set; }

        public int? SoLuong { get; set; }

        public virtual KhachHang KhachHang { get; set; }

        public virtual LoSanPham LoSanPham { get; set; }
    }
}
