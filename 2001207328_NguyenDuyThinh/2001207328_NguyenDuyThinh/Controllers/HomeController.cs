using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;
using _2001207328_NguyenDuyThinh.Areas.admin.Models;
using _2001207328_NguyenDuyThinh.Models;

namespace _2001207328_NguyenDuyThinh.Controllers
{
    public class HomeController : Controller
    {
        Data1DataContext _data = new Data1DataContext();
        public ActionResult Index()
        {
            New_Hot ret = new New_Hot();
            ret.moi = new List<Device>();
            ret.banChay = new List<CT_DonHang>();
            var moi = _data.Devices.OrderBy(n => n.NgayCapNhat).Take(16);
            ret.moi = moi;
            var query = _data.CT_DonHangs.Take(3);
            ret.banChay = query;
            return View(ret);
        }

        public ActionResult SPChuDe(int id)
        {
            //{id = 4})">Mỹ nghệ trang trí</a></li>
            // {id = 2})" > Lục bình mỹ nghệ 
            //  {id = 3})" > Tượng con vật phong thủy
            //  {id = 5})" > Mỹ nghệ gia dụng 
            //  {id = 1})" > Tượng gỗ mỹ nghệ 
            // {id = 6})" > Quà tặng mỹ nghệ 
            //  {id = 7})" > Mỹ nghệ trị liệu 
            // {id = 8})" > Đồ thờ bằng gỗ 
            device_head ret = new device_head();
            ret.icon = new List<ICON>();
            ret.device = new List<Device>();
            var data = (from de in _data.Devices select de).ToList();
            var icon = (from ic in _data.ICONs select ic).ToList();
            foreach (var i in data)
            {
                if (i.MaCD == id)
                {
                    ret.device.Add(i);
                }
            }
            foreach (var i in icon)
            {
                if (i.MACD == id)
                {
                    ret.icon.Add(i);
                }
            }

            return View(ret);
        }
        public ActionResult ChiTietSp(int id)
        {
            var chitiet = _data.ChiTiets.ToList();
            List<ChiTiet> rect = new List<ChiTiet>();
            foreach (var i in chitiet)
            {
                if (i.MaDev == id)
                {
                    rect.Add(i);
                }
            }
            return View(rect);
        }
        public ActionResult ChiTietSp1(int id)
        {
            deviceDetail devD = new deviceDetail();
            devD.detailS = new Sheet1();
            devD.detailD = new Device();
            var dev = _data.Sheet1s.ToList();
            //foreach (var  i in dev)
            //{
            //    if (i.MaDev ==id)
            //    {
            //        devD.sheet.Add(i);
            //    }
            //}
            devD.detailD = _data.Devices.SingleOrDefault(n => n.MaDevice == id);
            var temp = _data.Sheet1s.ToList();
            //foreach (var i in temp)
            //{
            //    if (i.MaDev ==id)
            //    {
            //        devD.detailD = i;
            //        break;
            //    }
            //}

            if(devD == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(devD);
        }
        public ActionResult Contact()
        { 
            return View();
        }
    }
}