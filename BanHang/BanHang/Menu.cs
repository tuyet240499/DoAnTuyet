using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Http;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace BanHang
{
    public partial class Menu : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            pn_Main.Controls.Clear();
            pn_Main.Dock = DockStyle.Fill;
            ThemLoSP themSP = new ThemLoSP();
            themSP.Dock = DockStyle.Fill;
            pn_Main.Controls.Add(themSP);
        }


        private void barBtnThongKe_ItemClick(object sender, ItemClickEventArgs e)
        {
            pn_Main.Controls.Clear();
            pn_Main.Dock = DockStyle.Fill;
            TheoDoiViTriSP thongKe = new TheoDoiViTriSP();
            thongKe.Dock = DockStyle.Fill;
            pn_Main.Controls.Add(thongKe);
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            pn_Main.Controls.Clear();
            pn_Main.Dock = DockStyle.Fill;
            QLHoaDon hoadon = new QLHoaDon();
            hoadon.Dock = DockStyle.Fill;
            pn_Main.Controls.Add(hoadon);
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            pn_Main.Controls.Clear();
            pn_Main.Dock = DockStyle.Fill;
            QuetQR quetQR = new QuetQR();
            quetQR.Dock = DockStyle.Fill;
            pn_Main.Controls.Add(quetQR);
        }

        private void btnQlySpham_ItemClick(object sender, ItemClickEventArgs e)
        {
            pn_Main.Controls.Clear();
            pn_Main.Dock = DockStyle.Fill;
            QuanLySP quetQR = new QuanLySP();
            quetQR.Dock = DockStyle.Fill;
            pn_Main.Controls.Add(quetQR);
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            pn_Main.Controls.Clear();
            pn_Main.Dock = DockStyle.Fill;
            QLNguonGocSP quetQR = new QLNguonGocSP();
            quetQR.Dock = DockStyle.Fill;
            pn_Main.Controls.Add(quetQR);
        }

        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            pn_Main.Controls.Clear();
            pn_Main.Dock = DockStyle.Fill;
            QLKhachHang quetQR = new QLKhachHang();
            quetQR.Dock = DockStyle.Fill;
            pn_Main.Controls.Add(quetQR);
        }

        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {
            pn_Main.Controls.Clear();
            pn_Main.Dock = DockStyle.Fill;
            TraCuuTTKH quetQR = new TraCuuTTKH();
            quetQR.Dock = DockStyle.Fill;
            pn_Main.Controls.Add(quetQR);
        }
        MqttClient client;
        string clientId;
        private void Menu_Load(object sender, EventArgs e)
        {
            //string BrokerAddress = "192.168.8.101:1883";

            //client = new MqttClient("192.168.8.101");

            //// register a callback-function (we have to implement, see below) which is called by the library when a message was received
            //client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            //// use a unique id as client id, each time we start the application
            //clientId = Guid.NewGuid().ToString();

            //client.Connect(clientId);

            //// subscribe to the topic with QoS 2
            //client.Subscribe(new string[] { "checkin" }, new byte[] { 2 });
            //client.Subscribe(new string[] { "cusPos" }, new byte[] { 2 });
            //client.Subscribe(new string[] { "checkout" }, new byte[] { 2 });
            //client.Subscribe(new string[] { "takePro" }, new byte[] { 2 });

        }
        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string ReceivedMessage = Encoding.UTF8.GetString(e.Message);
            string Topic = e.Topic;
            //if (Topic == "cusPos")
            //{
            //    string[] arrListStr = ReceivedMessage.Split('+');
            //    SetCus(ReceivedMessage);
            //}
            if (Topic == "takePro")
            {
                var mes = ReceivedMessage.Replace(" ", "");
                string[] arrListStr = ReceivedMessage.Split('-');
                SetTakePro(mes);

                //MessageBox.Show(ReceivedMessage);
                //Dispatcher.Invoke(delegate
                //{              // we need this construction because the receiving code in the library and the UI with textbox run on different threads
                //    mqtt.Text = ReceivedMessage;
                //});
            }
        }
        delegate void SetCusCallback(string text);
        delegate void SetTakeProCallback(string text);
        private void SetTakePro(string text)
        {
            if (this.tbTest.InvokeRequired)
            {
                SetTakeProCallback d = new SetTakeProCallback(SetTakePro);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                tbTest.Text = text;
            }
        }
        //private void SetCus(string text)
        //{
        //    // InvokeRequired required compares the thread ID of the
        //    // calling thread to the thread ID of the creating thread.
        //    // If these threads are different, it returns true.
        //    if (this.dgvKhachhang.InvokeRequired)
        //    {
        //        SetCusCallback d = new SetCusCallback(SetCus);
        //        this.Invoke(d, new object[] { text });
        //    }
        //    else
        //    {
        //        var cmd = db.Database.Connection.CreateCommand();
        //        cmd.CommandText = "SELECT * from [dbo].[GetCustomers]()";
        //        var table = new DataTable();
        //        cmd.Connection.Open();
        //        table.Load(cmd.ExecuteReader());
        //        dgvKhachhang.DataSource = table;
        //        cmd.Connection.Close();
        //        foreach (DataGridViewRow Myrow in dgvKhachhang.Rows)
        //        {            //Here 2 cell is target value and 1 cell is Volume
        //            if (Myrow.Cells[3].Value.ToString() == "Buon")// Or your condition 
        //            {
        //                Myrow.DefaultCellStyle.BackColor = Color.YellowGreen;
        //            }
        //            if (Myrow.Cells[3].Value.ToString() == "Ngac nhien")// Or your condition 
        //            {
        //                Myrow.DefaultCellStyle.BackColor = Color.Pink;
        //            }
        //            if (Myrow.Cells[3].Value.ToString() == "Tuc gian")// Or your condition 
        //            {
        //                Myrow.DefaultCellStyle.BackColor = Color.Red;
        //            }
        //            if (Myrow.Cells[3].Value.ToString() == "Vui")// Or your condition 
        //            {
        //                Myrow.DefaultCellStyle.BackColor = Color.Aqua;
        //            }
        //            if (Myrow.Cells[3].Value.ToString() == "Binh thuong")// Or your condition 
        //            {
        //                Myrow.DefaultCellStyle.BackColor = Color.WhiteSmoke;
        //            }
        //        }
        //    }
        //}
    }
}