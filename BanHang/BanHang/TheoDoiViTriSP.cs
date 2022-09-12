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
    public partial class TheoDoiViTriSP : DevExpress.XtraEditors.XtraUserControl
    {

        BanHangDbContext db = new BanHangDbContext();
        public TheoDoiViTriSP()
        {
            InitializeComponent();
        }

        private void TheoDoiViTriSP_Load(object sender, EventArgs e)
        {
            var DanhMuc = db.DanhMucSPs.Select(x=>x.TenDanhMuc).ToList();
            cbbDanhMuc.DataSource = DanhMuc;
        }

        private void cbbDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbbDanhMuc.SelectedItem!=null)
            {
                int stt = 1;
                List<LoSanPhamModel>  listLoSP = new List<LoSanPhamModel>();
                var tenDanhMuc = cbbDanhMuc.SelectedItem;
                var danhMuc = db.DanhMucSPs.Where(x => x.TenDanhMuc == tenDanhMuc).FirstOrDefault();
                var loSP = db.LoSanPhams.Where(x => x.MaDanhMuc == danhMuc.MaDanhMuc).ToList();
                foreach(var item in loSP)
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

            List<string> arrName = new List<string> { "A001", "A002", "A003", "A004", "B001", "B002", "B003", "B004", "C001", "C002", "C003", "C004", "D001", "D002", "D003", "D004" };
            int[] arrQuan = new int[arrName.Count];
            foreach (string name in arrName)
            {
                Control ctn = GetControlByName(name);
                ctn.Text = "0";
                ctn.BackColor = Color.Transparent;

            }
            var listSP = db.SanPhams.Where(x => x.MaLo == MaLoSP).ToList();
            foreach (SanPham pd in listSP)
            {
                var vitri = db.ViTris.Where(x => x.MaViTri == pd.ViTriHienTai).FirstOrDefault();
                int index = arrName.IndexOf(vitri.TenViTri);

                arrQuan[index] += 1;

                Control ctn = GetControlByName(vitri.TenViTri);

                if (pd.ViTriHienTai.ToString() == LoSP.MaViTri.ToString()) ctn.BackColor = Color.Green;
                else ctn.BackColor = Color.Red;

                ctn.Text = arrQuan[index].ToString();
                //string pos = name.Substring(3);

            }
            var vitrilo = db.ViTris.Where(x => x.MaViTri == LoSP.MaViTri).FirstOrDefault();
            GetControlByName(vitrilo.TenViTri).BackColor = Color.Green;
        }
        Control GetControlByName(string Name)
        {
            foreach (Control c in panel5.Controls)
                if (c.Name == Name)
                    return c;

            return null;
        }
    }
}
