using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TH10.Models;

namespace TH10.Controllers
{
    public class HomeController : Controller
    {
        private OntapEntities db = new OntapEntities();
        public ActionResult Index()
        {

            var laptops = db.Laptops;
            return View(laptops.ToList());
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
        public ActionResult ChiTietSP(int ID)
        {
            return View(db.Laptops.Where(s => s.ID == ID).FirstOrDefault());
        }
       
    }
}