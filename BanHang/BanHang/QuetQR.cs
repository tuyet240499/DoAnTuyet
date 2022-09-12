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
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using BanHang.EF;
using BanHang.Services.Services;
using System.Data.Entity;
using BanHang.Model;
using System.Configuration;
using System.Data.SqlClient;

namespace BanHang
{
    public partial class QuetQR : DevExpress.XtraEditors.XtraUserControl
    {
        BanHangDbContext db = new BanHangDbContext();
        public QuetQR()
        {
            InitializeComponent();
        }
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice captureDevice;  
        private void QuetQR_Load(object sender, EventArgs e)
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in filterInfoCollection)
            {
                cbbDevice.Items.Add(filterInfo.Name);
            }
            cbbDevice.SelectedIndex = 0;           
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            captureDevice = new VideoCaptureDevice(filterInfoCollection[cbbDevice.SelectedIndex].MonikerString);
            captureDevice.NewFrame += CaptureDevice_NewFrame;
            captureDevice.Start();
            timer1.Start();

        }

        private void CaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox2.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(pictureBox2.Image != null)
            {
                BarcodeReader barcodeReader = new BarcodeReader();
                Result result = barcodeReader.Decode((Bitmap)pictureBox2.Image);
                if(result!=null)
                {
                    txtMaKH.Text = result.ToString();
                    timer1.Stop();
                    if (captureDevice.IsRunning)
                        captureDevice.Stop();
                }    
            }    
        }

        private void txtTenKH_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMaKH_TextChanged(object sender, EventArgs e)
        {
            var maKH = Int32.Parse(txtMaKH.Text);
            var KhachHang = db.KhachHangs.Where(x => x.MaKhachHang == maKH).FirstOrDefault();
            txtDiaChi.Text = KhachHang.DiaChi;
            txtSDT.Text = KhachHang.SDT;
            txtTenKH.Text = KhachHang.TenKhachHang;
            if (KhachHang.AnhKhachDangKy != null)
            {
                tbAnh.Load(KhachHang.AnhKhachDangKy);
                tbAnh.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
    }
}
