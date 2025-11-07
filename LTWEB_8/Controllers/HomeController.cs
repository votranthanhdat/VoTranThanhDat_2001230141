using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTWEB_8.Models;
namespace LTWEB_8.Controllers
{
    public class HomeController : Controller
    {
        BookStoreEntities data = new BookStoreEntities();
        public ActionResult DMSach()
        {
            
            List<SACH> dsSach = data.SACHes.ToList();
            return View(dsSach);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}