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
    public partial class QuanLySP : DevExpress.XtraEditors.XtraUserControl
    {
        BanHangDbContext db = new BanHangDbContext();
        public QuanLySP()
        {
            InitializeComponent();
        }

        private void QuanLySP_Load(object sender, EventArgs e)
        {
            btnLuu.Enabled = false;
            txtMaSP.Enabled = false;
            txtMaVach.Enabled = false;
            cbbLoSP.Enabled = false;
            cbNSX.Enabled = false;
            tbDVT.Enabled = false;
            tbViTri.Enabled = false;
            //load lên grdView
            List<ThemSPModel> list_sp = new List<ThemSPModel>();
            int stt = 1;
            var sanpham = db.SanPhams.ToList();
            foreach (var item in sanpham)
            {
                ThemSPModel sp = new ThemSPModel();
                var loSP = db.LoSanPhams.Where(x => x.MaLo == item.MaLo).FirstOrDefault();
                var NSX = db.NhaSXes.Where(x => x.MaNhaSX == loSP.MaNhaSX).FirstOrDefault();
                var vitri = db.ViTris.Where(x => x.MaViTri == loSP.MaViTri).FirstOrDefault();
                sp.MaSP = item.MaSanPham;
                sp.TenLoSP = loSP.TenLo;
                sp.NSX = NSX.TenNhaSX;
                sp.MaVach = item.MaVach;
                sp.STT = stt;
                sp.ViTri = vitri.TenViTri;
                sp.DVT = loSP.DVT;
                list_sp.Add(sp);
                stt += 1;
            }
            grSanPham.DataSource = list_sp;
            //load lên combobox NSX
            var nsx = db.NhaSXes.Select(x => x.TenNhaSX).ToList();
            cbNSX.DataSource = nsx;
            //load lên combobox DanhMucSP
            var LoSP= db.LoSanPhams.Select(x => x.TenLo).ToList();
            cbbLoSP.DataSource = LoSP;
            
        }

        private void grSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.grSanPham.Rows[e.RowIndex];
                txtMaSP.Text = row.Cells[1].Value.ToString();
                txtMaVach.Text = row.Cells[5].Value.ToString();
                cbbLoSP.Text = row.Cells[2].Value.ToString();
                tbViTri.Text = row.Cells[3].Value.ToString();
                tbDVT.Text = row.Cells[4].Value.ToString();
                cbNSX.Text = row.Cells[6].Value.ToString();
            }
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            btnThemMoi.Enabled = false;
            txtMaVach.Enabled = true;
            cbbLoSP.Enabled = true;
            txtMaSP.Text ="";
            txtMaVach.Text = "";
            tbDVT.Text = "";
            tbViTri.Text = "";
        }

        private void cbbLoSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tenLoSP = cbbLoSP.SelectedItem;
            var LoSP = db.LoSanPhams.Where(x => x.TenLo == tenLoSP).FirstOrDefault();
            var NSX = db.NhaSXes.Where(x => x.MaNhaSX == LoSP.MaNhaSX).FirstOrDefault();
            var ViTri = db.ViTris.Where(x => x.MaViTri == LoSP.MaViTri).FirstOrDefault();
            cbNSX.Text = NSX.TenNhaSX;
            tbViTri.Text = ViTri.TenViTri;
            tbDVT.Text = LoSP.DVT;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = false;
            var tenLoSP= cbbLoSP.SelectedValue.ToString();
            var loSP = new F_LoSanPham().GetSingleByCondition(x => x.TenLo==tenLoSP);
            int maLo = loSP.MaLo;
            var tenNSX = cbNSX.SelectedValue.ToString();
            var NSX = new F_NSX().GetSingleByCondition(x => x.TenNhaSX == tenNSX);
            int maNSX = NSX.MaNhaSX;
            //them moi
            if (txtMaSP.Text == "")
            {
                SanPham sanPham = new SanPham();
                if (txtMaVach.Text=="")
                {
                    MessageBox.Show("Nhập thiếu thông tin!", "Vui lòng nhập đủ thông tin!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    sanPham.MaLo = maLo;
                    sanPham.MaVach = txtMaVach.Text;
                    sanPham.ViTriHienTai = loSP.MaViTri;
                    F_SanPham f_SanPham = new F_SanPham();
                    f_SanPham.Add(sanPham);
                    bool result = f_SanPham.SaveChange();
                    if (result == false)
                    {
                        MessageBox.Show("Đã có lỗi xảy ra!", "Vui lòng thử lại sau",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Lưu thành công!", "Thông báo");
                        txtMaSP.Text = sanPham.MaLo.ToString();
                    }
                }
                btnThemMoi.Enabled = true;
            }
            else
            {
                int ma = Int32.Parse(txtMaSP.Text);
                F_SanPham f_sp = new F_SanPham();
                SanPham sanPham = f_sp.GetSingleById(ma);
                if (txtMaVach.Text == "")
                {
                    MessageBox.Show("Nhập thiếu thông tin!", "Vui lòng nhập đủ thông tin!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    sanPham.MaLo = maLo;
                    sanPham.MaVach = txtMaVach.Text;
                    f_sp.Update(sanPham);
                    bool result = f_sp.SaveChange();
                    if (result == false)
                    {
                        MessageBox.Show("Đã có lỗi xảy ra!", "Vui lòng thử lại sau",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Lưu thành công!", "Thông báo");
                    }
                }
                btnSua.Enabled = true;
            }
            QuanLySP_Load(sender, e);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if(txtMaSP.Text=="")
            {
                MessageBox.Show("Bạn chưa chọn sản phẩm nào!", "Vui click chọn sản phẩm",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                btnLuu.Enabled = true;
                btnSua.Enabled = false;
                txtMaVach.Enabled = true;
                cbbLoSP.Enabled = true;
            }          
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaSP.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa chọn sản phẩm nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if (MessageBox.Show("Bạn có muốn xoá sản phẩm này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string connString = ConfigurationManager.ConnectionStrings["BanHangDbContext"].ConnectionString;
                    SqlConnection conn = new SqlConnection(connString);
                    conn.Open();

                    int ma = Int32.Parse(txtMaSP.Text);
                    string query1 = @"Delete SanPham where MaSanPham = " + ma;
                    SqlCommand comm1 = new SqlCommand(query1, conn);
                    comm1.CommandType = CommandType.Text;
                    comm1.ExecuteNonQuery();
                    MessageBox.Show("Xoá thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    QuanLySP_Load(sender, e);
                }
            }
        }

    }
}
