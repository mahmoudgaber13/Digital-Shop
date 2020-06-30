using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using OnlineTechStore.Models;
using System.IO;
using Microsoft.Ajax.Utilities;

namespace OnlineTechStore.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Categories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }
        public ActionResult Search(string CategoryName)
        {
            ViewBag.CategorName = CategoryName;
            List<Category> categories =
                db.Categories.Where(C => C.Name.Contains(CategoryName)).ToList();
            return View("index", categories);
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,Thumbnail")] Category category, HttpPostedFileBase Thumbnail)
        {
            if (ModelState.IsValid)
            {
                if(Thumbnail!=null)
                {
                    var fileName = Path.GetFileName(Thumbnail.FileName);
                    var phisicalPath = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                    Thumbnail.SaveAs(phisicalPath);
                    category.Thumbnail = fileName;
                }
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string OriginalThumbnail, [Bind(Include = "ID,Name,Description,Thumbnail")] Category category,HttpPostedFileBase Thumbnail)
        {
            if (ModelState.IsValid)
            {
                //send basic thumbnail hidden in view
                if (Thumbnail!=null)
                {
                    var fileName = Path.GetFileName(Thumbnail.FileName);
                    var phisicalPath = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                    Thumbnail.SaveAs(phisicalPath);
                    category.Thumbnail = fileName;
                }
                else
                {
                    category.Thumbnail = OriginalThumbnail;
                }
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
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
