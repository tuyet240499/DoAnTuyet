using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanHangAPI1.EF;
using BanHang.Model;

namespace BanHangAPI1.API
{
    public class SanPhamService
    {
        BanHangDbContext db = new BanHangDbContext();
        public ThongTinSanPhamModel thongTinSP(int maSP)
        {
            ThongTinSanPhamModel thongTinSanPham = new ThongTinSanPhamModel();
            var LoSP = db.LoSanPhams.Where(x => x.MaLo == maSP).FirstOrDefault();
            var maNguonGoc = LoSP.MaNguonGoc;
            var maNSX = LoSP.MaNhaSX;
            var nguonGoc = db.NguonGocSPs.Where(x => x.MaNguonGoc == maNguonGoc).FirstOrDefault();
            var NSX = db.NhaSXes.Where(x => x.MaNhaSX == maNSX).FirstOrDefault();

            thongTinSanPham.MaSP = maSP;
            thongTinSanPham.NhaSX = NSX.TenNhaSX;
            thongTinSanPham.NuocSX = nguonGoc.NuocSX;
            thongTinSanPham.GiaBan = LoSP.GiaBan.ToString();
            thongTinSanPham.DVT = LoSP.DVT;
            thongTinSanPham.TenSP = LoSP.TenLo;
            return thongTinSanPham;
        }
    }
}