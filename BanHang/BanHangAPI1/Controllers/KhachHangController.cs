using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using BanHangAPI1.API;
using BanHang.Model;
using System.Web.Http.Results;
using System.Net.Http;
using System.Web;
using System.IO;
using System.Threading.Tasks;

namespace BanHangAPI1.Controllers
{
    public class KhachHangController : ApiController
    {
        KhachHangService khachHang = new KhachHangService();
        [HttpPost]
        public DangNhapModel DangNhap(string tendn, string matkhau)
        {
            return khachHang.DangNhap(tendn, matkhau);
        }
        [Route("api/KhachHang/thongtin/{maKH}")]
        [HttpGet]
        public ThongTinKHModel ThongTinKh(int maKH)
        {
            return khachHang.ThongTinKH(maKH);
        }
        [Route("api/KhachHang/giohang/{maKH}")]
        [HttpGet]
        public List<GioHangModel> giohang(int maKH)
        {
            return khachHang.gioHang(maKH);
        }
        SanPhamService SanPhamService = new SanPhamService();

        [Route("api/KhachHang/thongtinSP/{maSP}")]
        [HttpGet]
        public ThongTinSanPhamModel thongtin(int maSP)
        {
            return SanPhamService.thongTinSP(maSP);
        }
        [Route("api/KhachHang/CheckIn/{maKH}")]
        [HttpGet]
        public HttpResponseMessage Checkin(int maKH)
        {

            bool result = khachHang.Checkin(maKH);
            if (result == false)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Có lỗi xảy ra!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Đăng ký thành công");

            }
        }
        [Route("api/ImageAPI/UploadFiles")]
        [HttpPost]
        public HttpResponseMessage UploadFiles()
        {
            //Create the Directory.
            string path = HttpContext.Current.Server.MapPath("~/Uploads/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //Fetch the File.
            HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];

            //Fetch the File Name.
            string fileName = Path.GetFileName(postedFile.FileName);

            //Save the File.
            postedFile.SaveAs(path + fileName);

            //Send OK Response to Client.
            return Request.CreateResponse(HttpStatusCode.OK, fileName);
        }
        [HttpPost]
        public HttpResponseMessage DangKy(DangKyModel dangky)
        {
            bool result= khachHang.LuuKhachHang(dangky);
            if(result==false)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Có lỗi xảy ra!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Đăng ký thành công");

            }

        }
        [HttpPost]
        public HttpResponseMessage luuAnh(string path, int MaKhachHang)
        {
            bool result = khachHang.LuuAnhKH(path, MaKhachHang);
            if (result == false)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Có lỗi xảy ra!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Lưu thành công");

            }

        }
        [HttpPost]
        public HttpResponseMessage OkCheckOut(int maKHCheckOut)
        {

            bool result = khachHang.OKcheckOut(maKHCheckOut);
            if (result == false)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Có lỗi xảy ra!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Thanh toán thành công");

            }
        }

        [Route("api/KhachHang/checkOut/{maKH}")]
        [HttpGet]
        public List<CheckOut> checkOut(int maKH)
        {
            return khachHang.checkOut(maKH);
        }
    }
}  
    
