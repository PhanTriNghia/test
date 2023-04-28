using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TH10.Models;

namespace TH10.Controllers
{
    public class LaptopsController : Controller
    {
        private OntapEntities db = new OntapEntities();

        // GET: Laptops
        public ActionResult Index()
        {
            var laptops = db.Laptops.Include(l => l.Factory);
            return View(laptops.ToList());
        }

        // GET: Laptops/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laptop laptop = db.Laptops.Find(id);
            if (laptop == null)
            {
                return HttpNotFound();
            }
            return View(laptop);
        }

        // GET: Laptops/Create
        public ActionResult Create()
        {
            ViewBag.FactoryID = new SelectList(db.Factories, "FactoryID", "FactoryName");
            return View();
        }

        // POST: Laptops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LaptopName,ImagePath,Price,FactoryID")] Laptop laptop, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                db.Laptops.Add(laptop);
                db.SaveChanges();
                if (uploadImage != null && uploadImage.ContentLength > 0)
                {
                    string id = db.Laptops.ToList().Last().ID.ToString();
                    int _id = int.Parse(id);
                    string _FileName = "";
                    int index = uploadImage.FileName.IndexOf('.');
                    _FileName = "laptop" + id.ToString() + "." + uploadImage.FileName.Substring(index + 1);
                    string _path = Path.Combine(Server.MapPath("~/Image/"), _FileName);
                    uploadImage.SaveAs(_path);

                    Laptop items = db.Laptops.FirstOrDefault(x => x.ID == _id);
                    items.ImagePath = _FileName;
                    db.SaveChanges();

                }
                return RedirectToAction("Index");
            }

            ViewBag.FactoryID = new SelectList(db.Factories, "FactoryID", "FactoryName", laptop.FactoryID);
            return View(laptop);
        }

        // GET: Laptops/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laptop laptop = db.Laptops.Find(id);
            if (laptop == null)
            {
                return HttpNotFound();
            }
            ViewBag.FactoryID = new SelectList(db.Factories, "FactoryID", "FactoryName", laptop.FactoryID);
            return View(laptop);
        }

        // POST: Laptops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,LaptopName,ImagePath,Price,FactoryID")] Laptop laptop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(laptop).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FactoryID = new SelectList(db.Factories, "FactoryID", "FactoryName", laptop.FactoryID);
            return View(laptop);
        }

        // GET: Laptops/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laptop laptop = db.Laptops.Find(id);
            if (laptop == null)
            {
                return HttpNotFound();
            }
            return View(laptop);
        }

        // POST: Laptops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Laptop laptop = db.Laptops.Find(id);
            db.Laptops.Remove(laptop);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
