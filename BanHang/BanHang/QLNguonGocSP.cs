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
    public partial class QLNguonGocSP : DevExpress.XtraEditors.XtraUserControl
    {
        BanHangDbContext db = new BanHangDbContext();
        public QLNguonGocSP()
        {
            InitializeComponent();
        }

        private void cbbLoSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbDanhMuc.SelectedItem != null)
            {
                int stt = 1;
                List<LoSanPhamModel> listLoSP = new List<LoSanPhamModel>();
                var tenDanhMuc = cbbDanhMuc.SelectedItem;
                var danhMuc = db.DanhMucSPs.Where(x => x.TenDanhMuc == tenDanhMuc).FirstOrDefault();
                var loSP = db.LoSanPhams.Where(x => x.MaDanhMuc == danhMuc.MaDanhMuc).ToList();
                foreach (var item in loSP)
                {
                    LoSanPhamModel sp = new LoSanPhamModel();
                    sp.MaSP = item.MaLo;
                    sp.TenSP = item.TenLo;
                    sp.STT = stt;
                    listLoSP.Add(sp);
                    stt += 1;
                }
                grvSanPham.DataSource = listLoSP;
            }
        }

        private void QLNguonGocSP_Load(object sender, EventArgs e)
        {
            txtDonViVC.Enabled = false;
            txtQuocGiaSX.Enabled = false;
            dtpThoiGianVC.Enabled = false;
            btnLuu.Enabled = false;
            txtMaNguonGoc.Enabled = false;
            var DanhMuc = db.DanhMucSPs.Select(x => x.TenDanhMuc).ToList();
            cbbDanhMuc.DataSource = DanhMuc;
        }

        private void grvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var MaLoSP = Int16.Parse(grvSanPham.Rows[e.RowIndex].Cells[1].Value.ToString());
            var LoSP = db.LoSanPhams.Where(x => x.MaLo == MaLoSP).FirstOrDefault();
            txtDVT.Text = LoSP.DVT;
            txtGiaBan.Text = LoSP.GiaBan.ToString();
            txtMaSP.Text = LoSP.MaLo.ToString();
            var NSX = db.NhaSXes.Where(x => x.MaNhaSX == LoSP.MaNhaSX).FirstOrDefault();
            txtNSX.Text = NSX.TenNhaSX;
            txtSL.Text = LoSP.SoLuong.ToString();
            txtTenSP.Text = LoSP.TenLo;
            if(LoSP.MaNguonGoc!=null)
            {
                var NguonGoc = db.NguonGocSPs.Where(x => x.MaNguonGoc == LoSP.MaNguonGoc).FirstOrDefault();
                txtDonViVC.Text = NguonGoc.DonVIVanChuyen;
                txtQuocGiaSX.Text = NguonGoc.NuocSX;
                DateTime? dt = NguonGoc.ThoiGianVanChuyen;
                dtpThoiGianVC.Value = dt.Value;
                txtMaNguonGoc.Text = NguonGoc.MaNguonGoc.ToString();
            }
            else
            {
                txtMaNguonGoc.Text = "";
                txtDonViVC.Text ="";
                txtQuocGiaSX.Text = "";
                dtpThoiGianVC.Value = DateTime.Now;
            }
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            if (txtMaSP.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn sản phẩm nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(txtQuocGiaSX.Text != "" && txtDonViVC.Text !="")
            {
                MessageBox.Show("Sản phẩm đã có nguồn gốc xuất xứ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }    
            else
            {
                txtDonViVC.Enabled = true;
                txtQuocGiaSX.Enabled = true;
                dtpThoiGianVC.Enabled = true;
                btnLuu.Enabled = true;
                btnThemMoi.Enabled = false;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if(txtMaSP.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn sản phẩm nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtQuocGiaSX.Text == "" && txtDonViVC.Text == "")
            {
                MessageBox.Show("Sản phẩm chưa có nguồn gốc xuất xứ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                txtDonViVC.Enabled = true;
                txtQuocGiaSX.Enabled = true;
                dtpThoiGianVC.Enabled = true;
                btnLuu.Enabled = true;
                btnSua.Enabled = false;
            }
        }

        private void btnLuu1_Click(object sender, EventArgs e)
        {
            if(txtMaNguonGoc.Text=="")
            {
                F_NguonGocSP nguonGocSP = new F_NguonGocSP();
                NguonGocSP nguonGoc = new NguonGocSP();
                nguonGoc.DonVIVanChuyen = txtDonViVC.Text;
                nguonGoc.NuocSX = txtQuocGiaSX.Text;
                nguonGoc.ThoiGianVanChuyen = dtpThoiGianVC.Value;
                nguonGocSP.Add(nguonGoc);
                bool result = nguonGocSP.SaveChange();
                if (result == false)
                {
                    MessageBox.Show("Đã có lỗi xảy ra!", "Vui lòng thử lại sau",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Lưu thành công!", "Thông báo");
                    txtDonViVC.Enabled = false;
                    txtQuocGiaSX.Enabled = false;
                    dtpThoiGianVC.Enabled = false;
                    btnLuu1.Enabled =false;
                    btnThemMoi.Enabled = true;
                }
                F_LoSanPham f_LoSanPham = new F_LoSanPham();
                int malo = Int32.Parse(txtMaSP.Text);
                LoSanPham loSP = f_LoSanPham.GetSingleById(malo);
                loSP.MaNguonGoc = nguonGoc.MaNguonGoc;
                f_LoSanPham.Update(loSP);
                f_LoSanPham.SaveChange();
            }
            else
            {
                F_NguonGocSP f_NguonGocSP = new F_NguonGocSP();
                int maNguonGoc = Int32.Parse(txtMaNguonGoc.Text);
                NguonGocSP nguonGocSp = f_NguonGocSP.GetSingleById(maNguonGoc);
                nguonGocSp.DonVIVanChuyen = txtDonViVC.Text;
                nguonGocSp.ThoiGianVanChuyen = dtpThoiGianVC.Value;
                nguonGocSp.NuocSX = txtQuocGiaSX.Text;
                f_NguonGocSP.Update(nguonGocSp);
                bool result = f_NguonGocSP.SaveChange();
                if (result == false)
                {
                    MessageBox.Show("Đã có lỗi xảy ra!", "Vui lòng thử lại sau",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Lưu thành công!", "Thông báo");
                    txtDonViVC.Enabled = false;
                    txtQuocGiaSX.Enabled = false;
                    dtpThoiGianVC.Enabled = false;
                    btnLuu1.Enabled = false;
                    btnSua.Enabled = true;
                }
            }
            QLNguonGocSP_Load(sender, e);
        }
    }
}
