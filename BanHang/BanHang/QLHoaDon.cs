using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BanHang.EF;
using BanHang.Services.Services;
using BanHang.Services.Infrastructure;
using System.Data.SqlClient;
using BanHang.Model;
using System.Reflection;
using System.Configuration;

namespace BanHang
{
    public partial class QLHoaDon : DevExpress.XtraEditors.XtraUserControl
    {
        BanHangDbContext db = new BanHangDbContext();
        public QLHoaDon()
        {
            InitializeComponent();
        }

        private void HoaDon_Load(object sender, EventArgs e)
        {
            txtTenKH.Enabled = false;
            txtDiaChi.Enabled = false;
            txtSDT.Enabled = false;
            txtMaHoaDon.Enabled = false;
            dtpNgayBan.Enabled = false;

            txtDonGia.Enabled = false;
            txtTenSP.Enabled = false;

            var listKH = db.KhachHangs.Select(x => x.MaKhachHang).ToList();
            cbbMaKH.DataSource = listKH;
            var maKH = Int32.Parse(cbbMaKH.SelectedValue.ToString());
            var KH = db.KhachHangs.Where(x => x.MaKhachHang == maKH).FirstOrDefault();
            txtDiaChi.Text = KH.DiaChi;
            txtSDT.Text = KH.SDT;
            dtpNgayBan.Value = DateTime.Now;
            txtTenKH.Text = KH.TenKhachHang;

            var listLoSP = db.LoSanPhams.Select(x => x.MaLo).ToList();
            cbbMaSP.DataSource = listLoSP;
        }

        private void cbbMaKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            var maKH = Int32.Parse(cbbMaKH.SelectedValue.ToString());
            var KH = db.KhachHangs.Where(x => x.MaKhachHang == maKH).FirstOrDefault();
            txtDiaChi.Text = KH.DiaChi;
            txtSDT.Text = KH.SDT;
            dtpNgayBan.Value = DateTime.Now;
            txtTenKH.Text = KH.TenKhachHang;
        }

        private void cbbMaSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            var maSP = Int32.Parse(cbbMaSP.SelectedValue.ToString());
            var SP = db.LoSanPhams.Where(x => x.MaLo == maSP).FirstOrDefault();
            txtTenSP.Text = SP.TenLo;
            txtDonGia.Text = SP.GiaBan.ToString();
        }

        private void txtSL_TextChanged(object sender, EventArgs e)
        {
            if (txtSL.Text != "")
            {
                var thanhtien = Double.Parse(txtDonGia.Text) * Double.Parse(txtSL.Text);
                txtThanhTien.Text = thanhtien.ToString();
            }
            else txtThanhTien.Text = "";
        }

        private void btnThemSP_Click(object sender, EventArgs e)
        {
            if(txtMaHoaDon.Text=="")
            {
                if(txtSL.Text=="")
                {
                    MessageBox.Show("Nhập thiếu thông tin!", "Vui lòng nhập đủ thông tin!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //lưu hoá đơn
                    F_HoaDon f_HoaDon = new F_HoaDon();
                    HoaDon hoaDon = new HoaDon();
                    hoaDon.MaKhachHang = Int32.Parse(cbbMaKH.SelectedValue.ToString());
                    hoaDon.NgayBan = dtpNgayBan.Value;
                    f_HoaDon.Add(hoaDon);
                    f_HoaDon.SaveChange();
                    //lưu sản phẩm
                    F_ChiTietHoaDon f_ChiTietHoaDon = new F_ChiTietHoaDon();
                    ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon();
                    chiTietHoaDon.MaSanPham =Int32.Parse(cbbMaSP.SelectedValue.ToString());
                    chiTietHoaDon.SoLuong = Int32.Parse(txtSL.Text);
                    chiTietHoaDon.MaHoaDon = hoaDon.MaHoaDon;
                    chiTietHoaDon.ThanhTien = Int32.Parse(txtThanhTien.Text);
                    txtMaHoaDon.Text = hoaDon.MaHoaDon.ToString();
                    //load lên grv
                    var listSP = db.ChiTietHoaDons.Where(x => x.MaHoaDon == hoaDon.MaHoaDon).ToList();

                }
            }
            else
            {
                if (txtSL.Text == "")
                {
                    MessageBox.Show("Nhập thiếu thông tin!", "Vui lòng nhập đủ thông tin!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {                   
                    //lưu sản phẩm
                    F_ChiTietHoaDon f_ChiTietHoaDon = new F_ChiTietHoaDon();
                    ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon();
                    chiTietHoaDon.MaSanPham = Int32.Parse(cbbMaSP.SelectedValue.ToString());
                    chiTietHoaDon.SoLuong = Int32.Parse(txtSL.Text);
                    chiTietHoaDon.MaHoaDon = Int32.Parse(txtMaHoaDon.Text);
                }
            }
        }
    }
}
