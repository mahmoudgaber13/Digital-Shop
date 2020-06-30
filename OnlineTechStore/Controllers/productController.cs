using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineTechStore.Models;

namespace OnlineTechStore.Controllers
{
    
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Product
        
        /*Customer Index*/
        [Authorize]
        public ActionResult CustomerIndex(int? page, int? id)
        {
            int pagesize = 8;
            int pageNumber = (page ?? 1);
            int ID = (id ?? 1);
            var Product = db.Products.Where(p => p.CategoryID == ID).OrderByDescending(s => s.Name);
            //var Product = (db.Products).OrderByDescending(s => s.Name);
            int pcount = db.Products.Count();
            ViewBag.NumberOfPages = (pcount / pagesize)+1;
            return View(Product.ToPagedList(pageNumber, pagesize));
        }

        public ActionResult CategoryProducts(int id,int? page)
        {
            int pagesize = 8;
            int pageNumber = (page ?? 1);
            var Product = db.Products.Where(p=>p.CategoryID==id).OrderByDescending(s => s.Name);
            int pcount = Product.Count();
            ViewBag.NumberOfPages = pcount / pagesize;
            return View("CustomerIndex", Product.ToPagedList(pageNumber, pagesize));
            //return RedirectToAction("CustomerIndex", Product.ToPagedList(pageNumber, pagesize));
        }

        /*Admin Index*/
        [Authorize(Roles = "Admin")]
        public ActionResult Index(int? page)
        {
            /*int pagesize = 2;
            int pageNumber = (page ?? 1);
            var student = (db.Products).OrderByDescending(s => s.Name);
            int scount = db.Products.Count();
            ViewBag.NumberOfPages = scount/pagesize;
            return View(student.ToPagedList(pageNumber,pagesize));*/
            return View(db.Products.ToList());
        }


        public ActionResult Search(string ProductName)
        {
            ViewBag.ProductName = ProductName;
            List<Product> Products =
                db.Products.Where(P => P.Name.Contains(ProductName)).ToList();
            return View("index", Products);
        }

        // GET: Product/Details/5

        /*Customer Detailes*/
        [Authorize]
        public ActionResult CustomerDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product Product = db.Products.Find(id);
            if (Product == null)
            {
                return HttpNotFound();
            }
            return View(Product);
        }

        /*Admin Detailes*/
        [Authorize(Roles ="Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product Product = db.Products.Find(id);
            if (Product == null)
            {
                return HttpNotFound();
            }
            return View(Product);
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public ActionResult Create(Product prod, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                var filename = Path.GetFileName(Image.FileName);
                var phisicalPath = Path.Combine(Server.MapPath("~/Content/images/"), filename);
                Image.SaveAs(phisicalPath);
                prod.Image = filename;
                db.Products.Add(prod);
                db.SaveChanges();
                return RedirectToAction("Index");
                
            }
            return View(prod);
        }

        // GET: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpGet]
        [Authorize(Roles = "Admin")]

        public ActionResult Create()
        {

            ViewBag.Cat = db.Categories.ToList();
            return View();
        }

        // GET: Product/Edit/5
        [Authorize(Roles = "Admin")]

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product Product = db.Products.Find(id);
            if (Product == null)
            {
                return HttpNotFound();
            }
            return View(Product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public ActionResult Edit([Bind(Include = "ID,Stock,Name,Price,Weigth,Description,Image")] Product Product, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    var filename = Path.GetFileName(Image.FileName);
                    var phisicalPath = Path.Combine(Server.MapPath("~/Content/images/"), filename);
                    Image.SaveAs(phisicalPath);
                    Product.Image = filename;
                }
                Product.CreateDate = DateTime.Now;
                db.Entry(Product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Product);
        }

        // GET: Product/Delete/5
        [Authorize(Roles = "Admin")]

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product Product = db.Products.Find(id);
            if (Product == null)
            {
                return HttpNotFound();
            }
            return View(Product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public ActionResult DeleteConfirmed(int id)
        {
            Product Product = db.Products.Find(id);
            db.Products.Remove(Product);
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
