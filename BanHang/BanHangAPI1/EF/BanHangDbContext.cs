using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BanHangAPI1.EF
{
    public partial class BanHangDbContext : DbContext
    {
        public BanHangDbContext()
            : base("name=BanHangDbContext")
        {
        }

        public virtual DbSet<ChiTietCuaHang> ChiTietCuaHangs { get; set; }
        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual DbSet<CuaHang> CuaHangs { get; set; }
        public virtual DbSet<DanhMucSP> DanhMucSPs { get; set; }
        public virtual DbSet<GioHang> GioHangs { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<LoSanPham> LoSanPhams { get; set; }
        public virtual DbSet<NguonGocSP> NguonGocSPs { get; set; }
        public virtual DbSet<NhaSX> NhaSXes { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<ViTri> ViTris { get; set; }
        public virtual DbSet<ViTriKhachHang> ViTriKhachHangs { get; set; }
        public virtual DbSet<ViTriSanPham> ViTriSanPhams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CuaHang>()
                .HasMany(e => e.ChiTietCuaHangs)
                .WithRequired(e => e.CuaHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoaDon>()
                .HasMany(e => e.ChiTietHoaDons)
                .WithRequired(e => e.HoaDon)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.TaiKhoans)
                .WithOptional(e => e.KhachHang)
                .HasForeignKey(e => e.MaNguoiDung);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.ViTriKhachHangs)
                .WithRequired(e => e.KhachHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoSanPham>()
                .HasMany(e => e.ChiTietHoaDons)
                .WithRequired(e => e.LoSanPham)
                .HasForeignKey(e => e.MaSanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoSanPham>()
                .HasMany(e => e.GioHangs)
                .WithOptional(e => e.LoSanPham)
                .HasForeignKey(e => e.MaSanPham);

            modelBuilder.Entity<LoSanPham>()
                .HasMany(e => e.ViTriSanPhams)
                .WithRequired(e => e.LoSanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.MaVach)
                .IsFixedLength();

            modelBuilder.Entity<ViTri>()
                .HasMany(e => e.ViTriKhachHangs)
                .WithRequired(e => e.ViTri)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ViTri>()
                .HasMany(e => e.ViTriSanPhams)
                .WithRequired(e => e.ViTri)
                .WillCascadeOnDelete(false);
        }
    }
}
