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
using System.IO;

namespace BanHang
{
    public partial class ThemLoSP : DevExpress.XtraEditors.XtraUserControl
    {
        BanHangDbContext db = new BanHangDbContext();
        public ThemLoSP()
        {
            InitializeComponent();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = false;
            var tenDanhMuc = cbDanhMuc.SelectedValue.ToString();
            var danhMucSP = new F_DanhMuc().GetSingleByCondition(x => x.TenDanhMuc == tenDanhMuc);
            int maDM = danhMucSP.MaDanhMuc;
            var tenNSX = cbNSX.SelectedValue.ToString();
            var NSX = new F_NSX().GetSingleByCondition(x => x.TenNhaSX == tenNSX);
            int maNSX = NSX.MaNhaSX;
            //them moi
            if (txtMaSP.Text=="")
            {
                LoSanPham sanPham = new LoSanPham();
                if (tbTenSP.Text == "" || tbDVT.Text == "" || tbGiaBan.Text == "" || tbSoLuong.Text == "")
                {
                    MessageBox.Show("Nhập thiếu thông tin!", "Vui lòng nhập đủ thông tin!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    sanPham.MaDanhMuc = maDM;
                    sanPham.TenLo = tbTenSP.Text;
                    sanPham.DVT = tbDVT.Text;
                    sanPham.MaNhaSX = maNSX;
                    sanPham.SoLuong = Int32.Parse(tbSoLuong.Text);
                    sanPham.GiaBan = Int32.Parse(tbGiaBan.Text);
                    if(txtDuongDan!=null)
                    {
                        sanPham.HinhAnh = txtDuongDan.Text;
                    }    
                    F_LoSanPham f_SanPham = new F_LoSanPham();
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
                simpleButton1.Enabled = true;
            }
            else
            {
                int ma = Int32.Parse(txtMaSP.Text);
                F_LoSanPham f_sp = new F_LoSanPham();
                LoSanPham sanPham = f_sp.GetSingleById(ma);
                if (tbTenSP.Text == "" || tbDVT.Text == "" || tbGiaBan.Text == "" || tbSoLuong.Text == "")
                {
                    MessageBox.Show("Nhập thiếu thông tin!", "Vui lòng nhập đủ thông tin!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    sanPham.MaDanhMuc= maDM;
                    sanPham.TenLo = tbTenSP.Text;
                    sanPham.DVT = tbDVT.Text;
                    sanPham.MaNhaSX = maNSX;
                    sanPham.SoLuong = Int32.Parse(tbSoLuong.Text);
                    sanPham.GiaBan = Int32.Parse(tbGiaBan.Text);
                    if (txtDuongDan != null)
                    {
                        sanPham.HinhAnh = txtDuongDan.Text;
                    }
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
            ThemSP_Load(sender, e);
            btnOpenDialog.Enabled = false;
        }

        private void ThemSP_Load(object sender, EventArgs e)
        {
            btnLuu.Enabled = false;
            txtMaSP.Enabled = false;
            tbTenSP.Enabled = false;
            tbDVT.Enabled = false;
            tbGiaBan.Enabled = false;
            tbSoLuong.Enabled = false;
            cbDanhMuc.Enabled = false;           
            cbNSX.Enabled = false;
            btnOpenDialog.Enabled = false;
            txtDuongDan.Visible = false;
            //load lên grdView
            List<ThemLoSPModel> list_sp = new List<ThemLoSPModel>();
            int stt = 1;
            var sanpham = db.LoSanPhams.ToList();
            foreach (var item in sanpham)
            {
                ThemLoSPModel sp = new ThemLoSPModel();
                var danhmuc = db.DanhMucSPs.Where(x => x.MaDanhMuc == item.MaDanhMuc).FirstOrDefault();
                var NSX = db.NhaSXes.Where(x => x.MaNhaSX == item.MaNhaSX).FirstOrDefault();
                sp.MaSP = item.MaLo;
                sp.TenSP = item.TenLo;
                sp.NSX = NSX.TenNhaSX;
                sp.DanhMucSP = danhmuc.TenDanhMuc;
                sp.GiaBan = item.GiaBan;
                sp.SoLuong = item.SoLuong;
                sp.STT = stt;
                sp.DVT = item.DVT;
                list_sp.Add(sp);
                stt += 1;
            }      
            grSanPham.DataSource = list_sp;
            //load lên combobox NSX
            var nsx = db.NhaSXes.Select(x => x.TenNhaSX).ToList();
            cbNSX.DataSource = nsx;
            //load lên combobox DanhMucSP
            var cbdanhmuc = db.DanhMucSPs.Select(x => x.TenDanhMuc).ToList();
            cbDanhMuc.DataSource = cbdanhmuc;
        }

        private void grSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex>=0)
            {
                DataGridViewRow row = this.grSanPham.Rows[e.RowIndex];
                txtMaSP.Text= row.Cells[1].Value.ToString();
                tbTenSP.Text = row.Cells[2].Value.ToString(); 
                tbDVT.Text = row.Cells[4].Value.ToString();
                tbGiaBan.Text = row.Cells[5].Value.ToString();
                tbSoLuong.Text = row.Cells[7].Value.ToString();
                cbDanhMuc.Text = row.Cells[3].Value.ToString();
                cbNSX.Text = row.Cells[6].Value.ToString();
                int maSP = Int32.Parse(txtMaSP.Text);
                var LoSP = db.LoSanPhams.Where(x => x.MaLo == maSP).FirstOrDefault();
                if(LoSP.HinhAnh!=null)
                {
                    ptbSP.Load(LoSP.HinhAnh);
                    ptbSP.SizeMode = PictureBoxSizeMode.StretchImage;
                }    
            }    
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(tbTenSP.Text.Trim()=="")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if(MessageBox.Show("Bạn có muốn xoá bản ghi này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string connString = ConfigurationManager.ConnectionStrings["BanHangDbContext"].ConnectionString;
                    SqlConnection conn = new SqlConnection(connString);
                    conn.Open();

                    int ma = Int32.Parse(txtMaSP.Text);
                    string query1 = @"Delete LoSanPham where MaLo = " + ma;
                    SqlCommand comm1 = new SqlCommand(query1, conn);
                    comm1.CommandType = CommandType.Text;
                    comm1.ExecuteNonQuery();
                    MessageBox.Show("Xoá thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ThemSP_Load(sender, e);
                }    
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if(txtMaSP.Text=="")
            {
                MessageBox.Show("Bạn chưa chọn dòng nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                btnLuu.Enabled = true;
                tbTenSP.Enabled = true;
                tbDVT.Enabled = true;
                tbGiaBan.Enabled = true;
                tbSoLuong.Enabled = true;
                cbDanhMuc.Enabled = true;
                cbNSX.Enabled = true;
                btnSua.Enabled = false;
                btnOpenDialog.Enabled = true;
            }              
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            tbTenSP.Enabled = true;
            tbDVT.Enabled = true;
            tbGiaBan.Enabled = true;
            tbSoLuong.Enabled = true;
            cbDanhMuc.Enabled = true;
            cbNSX.Enabled = true;
            simpleButton1.Enabled = false;
            //
            txtMaSP.Text = "";
            tbTenSP.Text="";
            tbDVT.Text = "";
            tbGiaBan.Text = "";
            tbSoLuong.Text = "";
            cbDanhMuc.Text = "";
            btnOpenDialog.Enabled = true;
        }

        private void btnOpenDialog_Click(object sender, EventArgs e)
        {
            Stream mystream = null;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image File(*.jpe; *.jpeg; *.bmp; *.png; *.jpg) | *.jpg;*.jpeg; *.bmp; *.png; *.jpg";
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    if((mystream = ofd.OpenFile())!=null)
                    {
                        string FileName = ofd.FileName;
                        txtDuongDan.Text = FileName;
                        if(mystream.Length>51200000)
                        {
                            MessageBox.Show("File Size limit exceeded");
                        }
                        else
                        {
                            ptbSP.SizeMode = PictureBoxSizeMode.StretchImage;
                            ptbSP.Load(FileName);
                           
                        }
                    }    
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
