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
    public partial class QLKhachHang : DevExpress.XtraEditors.XtraUserControl
    {
        BanHangDbContext db = new BanHangDbContext();
        public QLKhachHang()
        {
            InitializeComponent();
        }

        private void QLKhachHang_Load(object sender, EventArgs e)
        {

            var SL = (from kh in db.KhachHangs where kh.TrongCuaHang==true select kh ).Count();
            txtSL.Text = SL.ToString();

            int stt = 1;
            var listKH = db.KhachHangs.Where(x => x.TrongCuaHang == true).ToList();
            List<TheoDoiKhModel> theoDoiKhModels = new List<TheoDoiKhModel>();
            foreach(var item in listKH)
            {
                TheoDoiKhModel doiKhModels = new TheoDoiKhModel();
                var vitriKH = db.ViTriKhachHangs.Where(x => x.MaKhachHang == item.MaKhachHang).FirstOrDefault();
                var tenvitri = db.ViTris.Where(x => x.MaViTri == vitriKH.MaViTri).FirstOrDefault();
                doiKhModels.ViTri = tenvitri.TenViTri;
                doiKhModels.MaKH = item.MaKhachHang;
                doiKhModels.SDT = item.SDT;
                doiKhModels.TenKH= item.TenKhachHang;
                if(item.GioiTinh == true)
                {
                    doiKhModels.GioiTinh = "Nam";
                }
                else doiKhModels.GioiTinh = "Nữ";
                doiKhModels.DiaChi= item.DiaChi;
                doiKhModels.STT = stt;
                theoDoiKhModels.Add(doiKhModels);
                stt += 1;
            }
            grKH.DataSource = theoDoiKhModels;
        }

        private void grKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.grKH.Rows[e.RowIndex];
                string maKH = row.Cells[1].Value.ToString();               
                int maSP = Int32.Parse(maKH);
                var KH = db.KhachHangs.Where(x => x.MaKhachHang == maSP).FirstOrDefault();
                if (KH.AnhKhachDangKy != null)
                {
                    ptbAnhKH.Load(KH.AnhKhachDangKy);
                    ptbAnhKH.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }
    }
}
