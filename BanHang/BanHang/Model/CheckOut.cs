﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanHang.Model
{
    public class CheckOut
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public int? SoLuong { get; set; }
        public double ThanhTien { get; set; }
        public double? DonGia { get; set; }
    }
}
