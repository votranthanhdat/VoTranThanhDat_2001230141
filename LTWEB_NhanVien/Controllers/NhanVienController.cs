using System;
using System.Collections.Generic;
using System.Data.Entity; 
using System.IO;        
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTWEB_NhanVien.Models; 

namespace LTWEB_NhanVien.Controllers 
{
    public class NhanVienController : Controller
    {
        
        NhanSuEntities db = new NhanSuEntities();

        
        public void SetUpViewBags(object selectedDept = null, object selectedGender = null)
        {
            
            ViewBag.DeptId = new SelectList(db.tbl_Deparment.ToList(), "DeptId", "Name", selectedDept);

            
            var genders = new List<SelectListItem>
            {
                new SelectListItem { Text = "Nam", Value = "Nam" },
                new SelectListItem { Text = "Nữ", Value = "Nữ" }
            };
            ViewBag.Gender = new SelectList(genders, "Value", "Text", selectedGender);
        }

       
        public ActionResult Index(int? id)
        {
            
            ViewBag.Departments = db.tbl_Deparment.ToList();

            
            var employees = db.tbl_Employee.Include(e => e.tbl_Deparment);

            if (id != null)
            {
               
                employees = employees.Where(e => e.DeptId == id);
                ViewBag.SelectedDeptId = id; 
            }

            return View(employees.ToList());
        }

        public ActionResult Details(int id)
        {
            var employee = db.tbl_Employee
                             .Include(e => e.tbl_Deparment)
                             .FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

      
        [HttpGet]
        public ActionResult Create()
        {
            SetUpViewBags();
            return View();
        }

        // --- THÊM MỚI [POST] ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_Employee employee, HttpPostedFileBase Image)
        {
       
            SetUpViewBags(employee.DeptId, employee.Gender);

            if (Image != null && Image.ContentLength > 0)
            {
                var fileName = Path.GetFileName(Image.FileName);
                var path = Path.Combine(Server.MapPath("~/Images"), fileName);

                if (!System.IO.File.Exists(path))
                {
                    Image.SaveAs(path);
                }
                employee.Image = fileName;
            }
            else
            {
                employee.Image = "default-avatar.jpg"; // (Tên ảnh mặc định nếu không upload)
            }

            if (ModelState.IsValid)
            {
               
                int newId = (db.tbl_Employee.Any() ? db.tbl_Employee.Max(e => e.Id) : 0) + 1;
                employee.Id = newId;

                db.tbl_Employee.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // --- SỬA [GET] ---
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var employee = db.tbl_Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            
            SetUpViewBags(employee.DeptId, employee.Gender);
            return View(employee);
        }

        // --- SỬA [POST] ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_Employee employee, HttpPostedFileBase ImageUpload)
        {
            SetUpViewBags(employee.DeptId, employee.Gender);

            if (ModelState.IsValid)
            {
               
                var employeeToUpdate = db.tbl_Employee.Find(employee.Id);
                if (employeeToUpdate == null)
                {
                    return HttpNotFound();
                }

              
                employeeToUpdate.Name = employee.Name;
                employeeToUpdate.Gender = employee.Gender;
                employeeToUpdate.City = employee.City;
                employeeToUpdate.DeptId = employee.DeptId;

              
                if (ImageUpload != null && ImageUpload.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(ImageUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName); 

                    if (!System.IO.File.Exists(path))
                    {
                        ImageUpload.SaveAs(path);
                    }
                    employeeToUpdate.Image = fileName; 
                }
               
                db.SaveChanges(); 
                return RedirectToAction("Index");
            }
            return View(employee);
        }
    }
}