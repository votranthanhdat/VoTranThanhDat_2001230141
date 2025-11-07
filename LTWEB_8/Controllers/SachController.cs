using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTWEB_8.Models;
using System.IO; 
using System.Data.Entity; 

namespace LTWEB_8.Controllers
{
    public class SachController : Controller
    {
        // GET: Sach
        BookStoreEntities data = new BookStoreEntities();

        // Hiển thị danh mục sách
        public ActionResult DMSach()
        {
            var dsSach = data.SACHes.ToList();
            return View(dsSach);
        }

        // Hiển thị chi tiết
        public ActionResult Details(int id)
        {
            var sach = data.SACHes.FirstOrDefault(s => s.MaSach == id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }

        // Thêm mới [GET]
        [HttpGet]
        public ActionResult Create()
        {
           
            SetUpViewBags();
            return View();
        }

        // Thêm mới [POST]
        [HttpPost]
        [ValidateInput(false)] 
        public ActionResult Create(SACH s, HttpPostedFileBase fileUpload) 
        {
           
            SetUpViewBags(s.MaCD, s.MaTG, s.MaNXB);

          
            if (fileUpload == null)
            {
                ViewBag.ThongBao = "Vui lòng chọn ảnh bìa";
                return View(s); 
            }

            if (ModelState.IsValid)
            {
                
                var fileName = Path.GetFileName(fileUpload.FileName);
                var path = Path.Combine(Server.MapPath("~/Hinhsanpham"), fileName);

                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                    return View(s);
                }
                else
                {
                    fileUpload.SaveAs(path);
                }

               
                s.AnhBia = fileName;

                
                data.SACHes.Add(s);
                data.SaveChanges();
                return RedirectToAction("DMSach");
            }

           
            return View(s);
        }

        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var sach = data.SACHes.FirstOrDefault(s => s.MaSach == id);
            if (sach == null)
            {
                return HttpNotFound();
            }

            SetUpViewBags(sach.MaCD, sach.MaTG, sach.MaNXB);
            return View(sach);
        }

        
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(SACH s, HttpPostedFileBase fileUpload) 
        {
      
            SetUpViewBags(s.MaCD, s.MaTG, s.MaNXB);

            if (ModelState.IsValid)
            {
              
                var sachToUpdate = data.SACHes.FirstOrDefault(x => x.MaSach == s.MaSach);

                if (sachToUpdate != null)
                {
                   
                    sachToUpdate.TenSach = s.TenSach;
                    sachToUpdate.DonGia = s.DonGia;
                    sachToUpdate.SoTrang = s.SoTrang;

                
                    sachToUpdate.MaCD = s.MaCD;
                    sachToUpdate.MaTG = s.MaTG;
                    sachToUpdate.MaNXB = s.MaNXB;
                    sachToUpdate.Mota = s.Mota;

                    if (fileUpload != null)
                    {
                        var fileName = Path.GetFileName(fileUpload.FileName);
                        var path = Path.Combine(Server.MapPath("~/Hinhsanpham"), fileName);

                        if (!System.IO.File.Exists(path))
                        {
                            fileUpload.SaveAs(path);
                        }
                        sachToUpdate.AnhBia = fileName;
                    }
                   

                    data.SaveChanges();
                    return RedirectToAction("DMSach");
                }
            }
            return View(s);
        }


    
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var sach = data.SACHes.FirstOrDefault(s => s.MaSach == id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }


     
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var sach = data.SACHes.FirstOrDefault(s => s.MaSach == id);
            if (sach == null)
            {
                return HttpNotFound();
            }

            data.SACHes.Remove(sach);
            data.SaveChanges();
            return RedirectToAction("DMSach");
        }

    
        public void SetUpViewBags(object selectedCD = null, object selectedTG = null, object selectedNXB = null)
        {
            ViewBag.MaCD = new SelectList(data.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe", selectedCD);
            ViewBag.MaTG = new SelectList(data.TACGIAs.ToList().OrderBy(n => n.TenTG), "MaTG", "TenTG", selectedTG);
            ViewBag.MaNXB = new SelectList(data.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB", selectedNXB);
        }
    }
}