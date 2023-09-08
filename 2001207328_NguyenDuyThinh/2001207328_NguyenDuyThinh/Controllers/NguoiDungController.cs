using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _2001207328_NguyenDuyThinh.Models;

namespace _2001207328_NguyenDuyThinh.Controllers
{
    public class NguoiDungController : Controller
    {
        Data1DataContext _data = new Data1DataContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection c, Khach_Hang kh)
        {
            var hoten = c["HoTen"];
            var tendn = c["TaiKhoan"];
            var matkhau = c["MatKhau"];
            var diachi = c["DiaChi"];
            var email = c["Email"];
            var dienthoai = c["DienThoai"];
            var matkhaunhaplai = c["MatKhauNhapLai"];
            var ngaysinh = String.Format("{0:MM/dd/yyyy}", c["NgaySinh"]);
            
             if (String.IsNullOrEmpty(tendn))
            {
                ViewData["loi1"] = "Tên đăng nhập không được để trống";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["loi2"] = "Mật khẩu không được để trống";
            }
            else if (String.IsNullOrEmpty(matkhaunhaplai))
            {
                ViewData["loi3"] = "Vui lòng nhập lại mk";
            }
            else if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["loi4"] = "Vui lòng nhập lại email";
            }
            else if (String.IsNullOrEmpty(email))
            {
                ViewData["loi5"] = "Điện thoại không được để trống";
            }
            else
            {
                kh.TaiKhoan = tendn;
                kh.MatKhau = matkhau;
                kh.Email = email;
                kh.DienThoai = dienthoai;
                kh.NgaySinh =  DateTime.Parse( ngaysinh);
                _data.Khach_Hangs.InsertOnSubmit(kh);
                _data.SubmitChanges();
                RedirectToAction("Dangnhap");
            }
            return this.DangKy();
        }
        public ActionResult Dangnhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangnhap(FormCollection collection, string returnUrl)
        {
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            if (String.IsNullOrEmpty(tendn) || String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập!";
                ViewData["Loi2"] = "Phải nhập mật khẩu!";
                return View();
            }
            else
            {
                Khach_Hang kh = _data.Khach_Hangs.SingleOrDefault(n => n.Email == tendn && n.MatKhau == matkhau);
                if (kh != null)
                {
                    ViewBag.ThongBao = "Chúc mừng đã dang nhap thanh cong";
                    Session["TaiKhoan"] = kh;
                }
                else
                {
                    ViewBag.Thong = "Tên đăng nhập hoặc mật khẩu không đúng";
                    return View();
                }
            }
            if (String.IsNullOrEmpty(returnUrl))
            {
                return View();
            }
            else 
            {
                return Redirect(returnUrl);
            } 
        }

        public ActionResult Dangxuat()
        {
            Khach_Hang kh = new Khach_Hang();
            kh = Session["TaiKhoan"] as Khach_Hang;
            //co nguoi dang nhap
            if (kh != null)
            {
                Session.RemoveAll();
            }
            return RedirectToAction("Index","Home");
            
        }
        public ActionResult loginP()
        {
            return PartialView();
        }
        public ActionResult loginPart()
        {
            Khach_Hang kh = new Khach_Hang();
            kh = Session["TaiKhoan"] as Khach_Hang;
            return PartialView(kh); 
        }
    }
}