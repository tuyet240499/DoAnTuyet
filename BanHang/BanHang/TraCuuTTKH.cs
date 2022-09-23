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
    public partial class TraCuuTTKH : DevExpress.XtraEditors.XtraUserControl
    {
        BanHangDbContext db = new BanHangDbContext();
        public TraCuuTTKH()
        {
            InitializeComponent();
        }

        private void TraCuuTTKH_Load(object sender, EventArgs e)
        {
            //// var cuaHang = from ch in db.CuaHangs group ch by ch.MaCuaHang into g select new
            //var s = (from hd in db.HoaDons
            //          group new { hd.MaCuaHang } by hd.MaCuaHang into N                    
            //         orderby N.Key
            //         select N).FirstOrDefault();
            //int? maCuaHang = s.Key;
            //var tenCuaHang = db.CuaHangs.Where(x => x.MaCuaHang == maCuaHang).FirstOrDefault();

            
            //txtCuaHang.Text = tenCuaHang.TenCuaHang;
        }

        private async void btnTraCuu_Click(object sender, EventArgs e)
        {
            var SDT = txtSDT.Text;
            bool sdt = false;
            var listkh = db.KhachHangs.ToList();
            foreach(var item in listkh)
            {
                if (item.SDT == SDT) sdt = true;
            } 
            if(sdt==true)
            {
                var khachHang = db.KhachHangs.Where(x => x.SDT == SDT).FirstOrDefault();
                txtMaKH.Text = khachHang.MaKhachHang.ToString();
                txtTenKH.Text = khachHang.TenKhachHang;
                if (khachHang.AnhKhachDangKy != null)
                {
                    ptbAnhKhach.Load(khachHang.AnhKhachDangKy);
                    ptbAnhKhach.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                double tongtien = 0; 
                CuaHangThuongXuyenDen cuahang = new CuaHangThuongXuyenDen();
                using (var context = new BanHangDbContext())
                {
                    SqlParameter param1 = new SqlParameter("@SDT", SDT);
                    cuahang = await context.Database.SqlQuery<CuaHangThuongXuyenDen>("CuaHangThuongXuyen @SDT", param1).SingleAsync();
                }
                using (var context = new BanHangDbContext())
                {
                    SqlParameter param1 = new SqlParameter("@SDT", SDT);
                    tongtien = await context.Database.SqlQuery<double>("TongTien @SDT", param1).SingleAsync();
                }
                var tencuahang = db.CuaHangs.Where(x => x.MaCuaHang == cuahang.MaCuaHang).FirstOrDefault();
                txtCuaHang.Text = tencuahang.TenCuaHang;
                txtTongTien.Text = tongtien.ToString();
                //lấy danh sách sản phẩm đã mua
                string connString = ConfigurationManager.ConnectionStrings["BanHangDbContext"].ConnectionString;
                SqlConnection conn = new SqlConnection(connString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("DanhSachSP", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(
                new SqlParameter()
                {
                    ParameterName = "@SDT",
                    SqlDbType = SqlDbType.NVarChar,
                     Value = SDT
                }
                ) ;

                int stt = 1;
                List<ListSP> listSPs = new List<ListSP>();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ListSP sp = new ListSP();
                        sp.MaSP= Int32.Parse(reader["MaSanPham"].ToString());
                        var losp = db.LoSanPhams.Where(x => x.MaLo == sp.MaSP).FirstOrDefault();
                        sp.TenSP = losp.TenLo;
                        sp.SoLuong = Int32.Parse(reader["soluong"].ToString());
                        sp.STT = stt;
                        listSPs.Add(sp);
                        stt += 1;
                    }
                }
                
                grvSP.DataSource = listSPs;
            }    
            else
            {
                MessageBox.Show("Số điện thoại không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }    
        }
    }
}
