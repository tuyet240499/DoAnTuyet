namespace BanHangAPI1.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoSanPham")]
    public partial class LoSanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoSanPham()
        {
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
            GioHangs = new HashSet<GioHang>();
            SanPhams = new HashSet<SanPham>();
            ViTriSanPhams = new HashSet<ViTriSanPham>();
        }

        [Key]
        public int MaLo { get; set; }

        [StringLength(50)]
        public string TenLo { get; set; }

        public int? MaViTri { get; set; }

        public int? SoLuong { get; set; }

        [StringLength(50)]
        public string DVT { get; set; }

        public double? GiaBan { get; set; }

        public int? MaDanhMuc { get; set; }

        public int? MaNhaSX { get; set; }

        public int? SoLuongTon { get; set; }

        public int? MaNguonGoc { get; set; }

        [StringLength(100)]
        public string HinhAnh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }

        public virtual DanhMucSP DanhMucSP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GioHang> GioHangs { get; set; }

        public virtual NguonGocSP NguonGocSP { get; set; }

        public virtual NhaSX NhaSX { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham> SanPhams { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ViTriSanPham> ViTriSanPhams { get; set; }
    }
}
