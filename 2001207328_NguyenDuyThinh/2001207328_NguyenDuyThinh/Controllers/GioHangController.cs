using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _2001207328_NguyenDuyThinh.Areas.admin.Models;
using System.Web.Services.Description;
using _2001207328_NguyenDuyThinh.Models;
namespace _2001207328_NguyenDuyThinh.Controllers
{
    public class GioHangController : Controller
    {

        Data1DataContext db = new Data1DataContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Buy(int id)
        {
            deviceDetail devD = new deviceDetail();
            devD.sheet = new List<Sheet1>();

            devD.detailS = new Sheet1();
            devD.detailD = new Device();
            var dev = db.Sheet1s.ToList();
            foreach (var i in dev)
            {
                if (i.MaDev == id)
                {
                    devD.sheet.Add(i);
                }
            }
            devD.detailD = db.Devices.SingleOrDefault(n => n.MaDevice == id);
            var temp = db.Sheet1s.ToList();
            foreach (var i in temp)
            {
                if (i.MaDev == id)
                {
                    devD.detailS = i;
                    break;
                }
            }
            if (devD == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(devD);
        }
        public ActionResult GioHang()
        {
            List<GioHang> lstGiohang = Laygiohang();
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);

        }
        public List<GioHang> Laygiohang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["Giohang"] = lstGioHang;
            }
            return lstGioHang;
        }
        public ActionResult ThemGiohang(int iMaDevice, int MaChiTiet, string strURL)
        {
            List<GioHang> lstGiohang = Laygiohang();
            GioHang sanpham = lstGiohang.Find(n => n.chitiet.MaChiTiet == MaChiTiet);
            if (sanpham == null)
            {
                sanpham = new GioHang(iMaDevice, MaChiTiet);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoluong++;
                return Redirect(strURL);
            }
        }
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                iTongSoLuong = lstGiohang.Sum(n => n.iSoluong);

            }
            return iTongSoLuong;
        }

        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                dTongTien = lstGiohang.Sum(n => n.dThanhtien);

            }
            return dTongTien;
        }
        public ActionResult GiohangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView();
        }

        public ActionResult XoaGiohang(int iMasp)
        {
            List<GioHang> lstGiohang = Laygiohang();
            GioHang sanpham = lstGiohang.SingleOrDefault(n => n.chitiet.MaChiTiet == iMasp);
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.chitiet.MaChiTiet == iMasp);
            }
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("GioHang", "GioHang");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaAll()
        {
            List<GioHang> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult CapnhatGiohang(int iMasp, FormCollection f)
        {
            List<GioHang> lstGiohang = Laygiohang();
            GioHang sanpham = lstGiohang.SingleOrDefault(n => n.chitiet.MaChiTiet == iMasp);
            if (sanpham != null)
            {
                sanpham.iSoluong = int.Parse(f["txtSoluong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        [HttpGet]
        public ActionResult Dathang()
        {
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("thongTinGiaoHang", "Giohang");
            }

            if (Session["Giohang"] == null)
            {
                return RedirectToAction("index", "BookS");
            }
            List<GioHang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }
        [HttpPost]
        public ActionResult Dathang(FormCollection collection)
        {
            Khach_Hang kh = (Khach_Hang)Session["Taikhoan"];

            //khach hang da dang nhap
            //luu cac thong tin 
            //don dat hang
            //chi tiet don hang
            //thong tin nguoi nhan
            Don_Dat_Hang ddh = new Don_Dat_Hang();
            if (kh != null)
            {
                //luu tru don dat hang

                List<GioHang> gh = Laygiohang();
                ddh.MaKH = kh.MaKH;
                ddh.NgayDH = DateTime.Now;
                var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["Ngaygiao"]);
                ddh.NgayGiao = DateTime.Parse(ngaygiao);
                ddh.TinhTrangGiaoHang = false.ToString();
                ddh.TinhTrangGiaoHang = "false";
                ddh.DaThanhToan = false;
                ddh.Name = collection["name"];
                ddh.Phone = collection["phone"];
                ddh.Address = collection["address"];
                ddh.Email = collection["email"];
                db.Don_Dat_Hangs.InsertOnSubmit(ddh);
                db.SubmitChanges();
                int temp = db.Don_Dat_Hangs.ToList().Last().MaDH;
                //Them chi tiet don hang
                foreach (var item in gh)
                {
                    CT_DonHang ctdh = new CT_DonHang();

                    ctdh.MaDH = temp;
                    ctdh.MaDevice = item.iMa;
                    ctdh.SoLuong = item.iSoluong;
                    ctdh.DonGia = (decimal)item.dDongia;
                    db.CT_DonHangs.InsertOnSubmit(ctdh);
                }

                db.SubmitChanges();
                //ke ca co dang nhap va khong dang nhap
            }
            //neu khong dang nhap
            //luu thong tin dia chi nguoi nhan
            //don dat hang
            else
            {
                Response.Write("<script>alert('that bai')</script>");
            }


            return RedirectToAction("Xacnhandonhang", "Giohang", new { sdt = ddh.Phone });
        }

        public ActionResult Xacnhandonhang(string sdt)
        {

            ViewBag.sdt = sdt;

            return View();
        }

        public ActionResult thongTinGiaoHang()
        {

            return View();
        }
        public ActionResult checkOut()
        {
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }

        [HttpPost]
        public ActionResult checkOut(FormCollection collection)
        {


            //khach hang da dang nhap
            //luu cac thong tin 
            //don dat hang
            //chi tiet don hang
            //thong tin nguoi nhan
            Don_Dat_Hang ddh = new Don_Dat_Hang();


            List<GioHang> gh = Laygiohang();
            ddh.MaKH = 2;
            ddh.NgayDH = DateTime.Now;
            var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["Ngaygiao"]);
            ddh.NgayGiao = DateTime.Parse(ngaygiao);
            ddh.TinhTrangGiaoHang = false.ToString();
            ddh.TinhTrangGiaoHang = "false";
            ddh.DaThanhToan = false;
            ddh.Name = collection["name"];
            ddh.Phone = collection["phone"];
            ddh.Address = collection["address"];
            ddh.Email = collection["email"];
            db.Don_Dat_Hangs.InsertOnSubmit(ddh);
            db.SubmitChanges();
            //Them chi tiet don hang
            foreach (var item in gh)
            {
                CT_DonHang ctdh = new CT_DonHang();
                int temp = db.Don_Dat_Hangs.ToList().Last().MaDH;
                ctdh.MaDH = ddh.MaDH;
                ctdh.MaDevice = item.iMa;
                ctdh.SoLuong = item.iSoluong;
                ctdh.DonGia = (decimal)item.dDongia;
                db.CT_DonHangs.InsertOnSubmit(ctdh);
            }
            //ke ca co dang nhap va khong dang nhap
            try
            {
                db.SubmitChanges();
                Session["Giohang"] = null;
            }
            catch (Exception)
            {

            }
            return RedirectToAction("Xacnhandonhang", "Giohang", new { email = ddh.Email });
        }
    }
}