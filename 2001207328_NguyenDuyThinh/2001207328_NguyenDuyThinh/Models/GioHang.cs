using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using _2001207328_NguyenDuyThinh.Models;

namespace _2001207328_NguyenDuyThinh.Models
{
    public class GioHang
    {
        Data1DataContext db = new Data1DataContext();

        public int iMa { set; get; }
        public string sTen { set; get; }
        public string sAnhbia { set; get; }
        public double dDongia { set; get; }
        public int iSoluong { set; get; }
        public Sheet1 chitiet { get; set; }

        //chi tiet san pham cho nguoi dung biet minh mua cai nao
        //public Sheet1_ chitiet { get; set; }
        public Double dThanhtien
        {
            get { return (iSoluong * dDongia) * 22000; }
        }
        public GioHang(int Masach, int Machitiet)
        {
            this.iMa = Masach;
            Device sach = db.Devices.SingleOrDefault(n => n.MaDevice == Masach);
            this.sTen = sach.TenDevice;
            this.sAnhbia = sach.AnhBia;
            this.dDongia = double.Parse(sach.GiaBan.ToString());
            this.iSoluong = 1;
            this.chitiet = db.Sheet1s.SingleOrDefault(n =>n.MaChiTiet == Machitiet);
        }
    }
}