using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BanHang.EF;
using BanHang.Model;
using System.Data.Entity;
using BanHang.Services.Services;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace BanHangAPI1.API
{
    public class KhachHangService
    {
        BanHangDbContext db = new BanHangDbContext();
        public DangNhapModel DangNhap(string tendn, string matkhau)
        {
            DangNhapModel dn = new DangNhapModel();
            List<TaiKhoan> taiKhoans = db.TaiKhoans.ToList();
            foreach (var item in taiKhoans)
            {
                if (item.TenDangNhap == tendn && item.MatKhau == matkhau)
                {
                    dn.Ok = true;
                    dn.MatKhau = matkhau;
                    dn.TenDN = tendn;
                    dn.MaKH = item.MaNguoiDung;
                }
            }
            return dn;
        }
        public ThongTinKHModel ThongTinKH(int maKh)
        {
            ThongTinKHModel thongTinKH = new ThongTinKHModel();
            var khachHang = db.KhachHangs.Where(x => x.MaKhachHang == maKh).FirstOrDefault();
            thongTinKH.MaKH = maKh;
            thongTinKH.HoTen = khachHang.TenKhachHang;
            thongTinKH.DiaChi = khachHang.DiaChi;
            thongTinKH.SDT = khachHang.SDT;
            thongTinKH.Anh = khachHang.AnhKhachDangKy;
            return thongTinKH;
        }
        public List<GioHangModel> gioHang(int maKH)
        {
            List<GioHangModel> listGioHang = new List<GioHangModel>();
            var gioHangs = db.GioHangs.Where(x => x.MaKhachHang == maKH).ToList();
            foreach (var item in gioHangs)
            {
                GioHangModel gioHang = new GioHangModel();
                var SanPham = db.LoSanPhams.Where(x => x.MaLo == item.MaSanPham).FirstOrDefault();
                gioHang.maSP = SanPham.MaLo;
                gioHang.soLuong = item.SoLuong;
                gioHang.AnhSP = SanPham.HinhAnh;
                gioHang.TenSP = SanPham.TenLo;
                gioHang.Gia = SanPham.GiaBan;
                listGioHang.Add(gioHang);
            }
            return listGioHang;
        }
        public bool Dangky(string hoTen, string SDT, bool gioiTinh, string tenDangNhap, string matKhau)
        {
            bool result = true;
            F_KhachHang f_KhachHang = new F_KhachHang();
            F_TaiKhoan f_TaiKhoan = new F_TaiKhoan();
            KhachHang khachHang = new KhachHang();
            TaiKhoan taiKhoan = new TaiKhoan();
            khachHang.TenKhachHang = hoTen;
            khachHang.SDT = SDT;
            khachHang.GioiTinh =gioiTinh;
            khachHang.TrongCuaHang = false;
            f_KhachHang.Add(khachHang);
            bool save1= f_KhachHang.SaveChange();
            if (save1 == false) result = false;
            taiKhoan.MaNguoiDung = khachHang.MaKhachHang;
            taiKhoan.TenDangNhap = tenDangNhap;
            taiKhoan.MatKhau = matKhau;
            f_TaiKhoan.Add(taiKhoan);
            bool save2 = f_TaiKhoan.SaveChange();
            if (save2 == false) result = false;
            return result;
        }
        public bool BoSungThongTin(int maKH, string diaChi, string ngaySinh, string anhKH)
        {
            F_KhachHang f_KhachHang = new F_KhachHang();
            KhachHang khachHang = db.KhachHangs.Where(x => x.MaKhachHang == maKH).FirstOrDefault();
            khachHang.DiaChi = diaChi;
            khachHang.NgaySinh = DateTime.ParseExact(ngaySinh, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            khachHang.AnhKhachDangKy = anhKH;
            f_KhachHang.Update(khachHang);
            bool result = f_KhachHang.SaveChange();
            return result;
        }
        public bool LuuAnhKH(string path, int MaKhachHang)
        {
            F_KhachHang f_KhachHang = new F_KhachHang();
            var khachHang = db.KhachHangs.Where(x => x.MaKhachHang == MaKhachHang).FirstOrDefault();
            khachHang.AnhKhachDangKy = path;
            f_KhachHang.Update(khachHang);
            bool result = f_KhachHang.SaveChange();
            return result;
        }
        public bool Checkin(int MaKH)
        {
            var khachHang = db.KhachHangs.Where(x => x.MaKhachHang == MaKH).FirstOrDefault();
            if (khachHang != null)
            {
                F_GioHang f_GioHang = new F_GioHang();
                GioHang gioHang = new GioHang();
                gioHang.MaKhachHang = MaKH;
                f_GioHang.Add(gioHang);
                f_GioHang.SaveChange();
                return true;
            }
            else return false;

        }
        public List<CheckOut> checkOut(int maKH)
        {

            double TongTien = 0;
            var khachHang = db.KhachHangs.Where(x => x.MaKhachHang == maKH).FirstOrDefault();
            var gioHang = db.GioHangs.Where(x => x.MaKhachHang == maKH).ToList();
            List<CheckOut> checkOuts = new List<CheckOut>();
            if (khachHang != null)
            {
                foreach (var item in gioHang)
                {
                    var loSanPham = db.LoSanPhams.Where(x => x.MaLo == item.MaSanPham).FirstOrDefault();
                    CheckOut checkOut = new CheckOut();
                    checkOut.TenSP = loSanPham.TenLo;
                    checkOut.DonGia = loSanPham.GiaBan;
                    checkOut.SoLuong = item.SoLuong;
                    checkOut.MaSP = loSanPham.MaLo;
                    checkOut.ThanhTien = (Double.Parse(checkOut.SoLuong.ToString()) * Int32.Parse(checkOut.DonGia.ToString()));
                    checkOuts.Add(checkOut);
                }
            }
            return checkOuts;
        }
        public bool OKcheckOut(int maKH)
        {
            bool result = true;
            F_HoaDon f_HoaDon = new F_HoaDon();
            HoaDon hoaDon = new HoaDon();
            hoaDon.MaKhachHang = 1;
            hoaDon.NgayBan = DateTime.Now;
            f_HoaDon.Add(hoaDon);
            bool save1 = f_HoaDon.SaveChange();
            if (save1 == false) result = false;
            F_ChiTietHoaDon f_ChiTietHoaDon = new F_ChiTietHoaDon();
            var gioHang = db.GioHangs.Where(x => x.MaKhachHang == 1).ToList();
            List<ChiTietHoaDonModel> chiTietHoaDon = new List<ChiTietHoaDonModel>();
            foreach (var item in gioHang)
            {
                var loSanPham = db.LoSanPhams.Where(x => x.MaLo == item.MaSanPham).FirstOrDefault();

                ChiTietHoaDon chiTiet = new ChiTietHoaDon();
                chiTiet.MaSanPham = loSanPham.MaLo;
                chiTiet.SoLuong = item.SoLuong;
                chiTiet.ThanhTien = (chiTiet.ThanhTien) * (loSanPham.GiaBan);
                chiTiet.MaHoaDon = hoaDon.MaHoaDon;
                f_ChiTietHoaDon.Add(chiTiet);
                bool save2 = f_ChiTietHoaDon.SaveChange();
                if (save2 == false) result = false;
            }
            string connString = ConfigurationManager.ConnectionStrings["BanHangDbContext"].ConnectionString;
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            string query1 = @"Delete GioHang where MaKhachHang = " + 1;
            SqlCommand comm1 = new SqlCommand(query1, conn);
            comm1.CommandType = CommandType.Text;
            comm1.ExecuteNonQuery();
            return true;
        }
    }
}
