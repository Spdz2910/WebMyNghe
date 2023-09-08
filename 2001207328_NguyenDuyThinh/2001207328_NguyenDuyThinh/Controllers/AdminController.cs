using _2001207328_NguyenDuyThinh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.IO;
using System.Web.Services.Description;
using _2001207328_NguyenDuyThinh.Areas.admin.Models;

namespace _2001207328_NguyenDuyThinh.Controllers
{
    public class AdminController : Controller
    {
        Data1DataContext db = new Data1DataContext();

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection f)
        {
            var tenDn = f["email"];
            var mk = f["password"];
            if (String.IsNullOrEmpty(tenDn) || String.IsNullOrEmpty(mk))
            {
                ViewData["loi"] = "Email hoặc mất khẩu không đúng";
            }
            else
            {
                ADMIN ad = db.ADMINs.SingleOrDefault(n => n.User_Admin == tenDn && n.password == mk);
                if (ad != null)
                {
                    Session["email"] = tenDn;
                    return RedirectToAction("admin_home", "Admin");
                }
            }
            return this.Index();
        }
        public ActionResult admin_home()
        {
            if (!string.IsNullOrEmpty(Session["email"] as string))
            {
                return RedirectToAction("device", "Admin");
            }
            else
            {
                ViewData["loi"] = "bạn không có quyền truy cập";
            }

            return Content("bạn không có quyền truy cập");
        }
        public bool check()
        {
            //neu chua dang nhap
            if (!string.IsNullOrEmpty(Session["email"] as string))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public ActionResult device(int? page)
        {
            if (check())
            {
                return RedirectToAction("admin_home");
            }
            int pageSize = 5;
            int pageNum = (page ?? 1);


            var deviceL = db.Devices.ToList().OrderBy(n => n.MaDevice);

            return View(deviceL.ToPagedList(pageNum, pageSize));

        }
        [HttpGet]
        public ActionResult CreatNew()
        {
            ViewBag.MaCD = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDE), "MaCD", "TenChuDe");
            ViewBag.MaNSX = new SelectList(db.Nha_San_Xuats.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult CreatNew(Device dev, HttpPostedFileBase fileUpload)
        {


            ViewBag.MaCD = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDE), "MaCD", "TenChuDe");
            ViewBag.MaNSX = new SelectList(db.Nha_San_Xuats.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");

            if (fileUpload == null)
            {
                ViewBag.thongbao = "vui lòng chọn hình ảnh";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    //luu ten file
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    //luu duong dan file
                    var path = Path.Combine(Server.MapPath("~/image/DEVICE"), fileName);
                    //kiem tra file ton tai chu
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.thongbao = "hình ảnh đã tồn tại";
                    }
                    else
                    {
                        fileUpload.SaveAs(path);
                    }

                    dev.AnhBia = fileName;
                    db.Devices.InsertOnSubmit(dev);
                    db.SubmitChanges();
                }

                return View();
            }
        }

        public ActionResult Detail(int id)
        {
            deviceDetail devD = new deviceDetail();
            devD.sheet = new List<Sheet1>();

            devD.detailS = new Sheet1();
            devD.detailD = new Device();
            //get color
            var dev = db.Sheet1s.ToList();
            foreach (var i in dev)
            {
                if (i.MaDev == id)
                {
                    //
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
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Device dev = db.Devices.SingleOrDefault(n => n.MaDevice == id);
            if (dev == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Devices.DeleteOnSubmit(dev);
            db.SubmitChanges();
            return RedirectToAction("device");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Device dev = db.Devices.SingleOrDefault(n => n.MaDevice == id);
            if (dev == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaCD = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDE), "MaCD", "TenChuDe");
            ViewBag.MaNSX = new SelectList(db.Nha_San_Xuats.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");
            return View(dev);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Device dev, HttpPostedFileBase fileUpload)
        {

            ViewBag.MaCD = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDE), "MaCD", "TenChuDe");
            ViewBag.MaNSX = new SelectList(db.Nha_San_Xuats.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");
            Device devS = db.Devices.Single(n => n.MaDevice == dev.MaDevice);

            if (ModelState.IsValid)
            {
                if (fileUpload != null)
                {
                    //luu ten file
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    //luu duong dan file
                    var path = Path.Combine(Server.MapPath("~/image/DEVICE"), fileName);
                    //kiem tra file ton tai chu
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.thongbao = "hình ảnh đã tồn tại";
                    }
                    else
                    {
                        fileUpload.SaveAs(path);
                    }
                    dev.AnhBia = fileName;
                }
                else
                {
                    dev.AnhBia = devS.AnhBia;
                }
                db.Devices.DeleteOnSubmit(devS);
                db.Devices.InsertOnSubmit(dev);
                db.SubmitChanges();
            }

            return RedirectToAction("device");
        }
        public ActionResult user()
        {
            if (check())
            {
                return RedirectToAction("admin_home");
            }
            var kh = db.Khach_Hangs.ToList();
            return View(kh);
        }
        //user
        [HttpGet]
        public ActionResult DeleteUser(int id)
        {
            Khach_Hang dev = db.Khach_Hangs.SingleOrDefault(n => n.MaKH == id);
            if (dev == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Khach_Hangs.DeleteOnSubmit(dev);
            db.SubmitChanges();
            return RedirectToAction("user");
        }
        //
        public ActionResult donHang()
        {
            if (check())
            {
                return RedirectToAction("admin_home");
            }
            var dh = db.Don_Dat_Hangs.ToList();
            return View(dh);
        }
        [HttpGet]
        public ActionResult deleteDonHang(int id)
        {
            CT_DonHang dev = db.CT_DonHangs.SingleOrDefault(n => n.MaDH == id);
            Don_Dat_Hang don = db.Don_Dat_Hangs.SingleOrDefault(n => n.MaDH == id);
            if (dev == null || don == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            try
            {
                db.CT_DonHangs.DeleteOnSubmit(dev);
                db.Don_Dat_Hangs.DeleteOnSubmit(don);
            }
            catch (Exception e)
            {
                Response.Write(e.Message);
            }
            db.SubmitChanges();
            return RedirectToAction("donHang");
        }

        public ActionResult chitiet()
        {
            var chitiet = db.Don_Dat_Hangs.SingleOrDefault(n => n.MaDH == 14);
            return View(chitiet);
        }
        public ActionResult dangxuat()
        {
            Session.RemoveAll();
            return RedirectToAction("Index");
        }
    }

}