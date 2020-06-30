using OnlineTechStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTechStore.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }
        //public ActionResult Search(string Name)
        //{
        //    ViewBag.Name = Name;
        //    List<Category> categories =
        //        db.Categories.Where(C => C.Name.Contains(Name)).ToList();
        //    List<Product> Products =
        //        db.Products.Where(P => P.Name.Contains(Name)).ToList();
        //    if (categories.Count >= 1)
        //    {
        //        return RedirectToAction("Search", "Category",new { CategoryName = Name } );
        //    }
        //    else if (Products.Count >= 1)
        //    {
        //        return RedirectToAction("Search", "Product", new { ProductName = Name });
        //    }
        //    else
        //    {
        //        return RedirectToAction("No Thing With This Name");
        //    }

        //}
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