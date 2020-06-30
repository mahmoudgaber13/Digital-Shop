using OnlineTechStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OnlineTechStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private string strCart = "Cart";
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }

        /*Add To Cart*/
        public ActionResult OrderNow(int? id)
        {
            if(id ==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if(Session[strCart] ==null)
            {
                List<Cart> lsCart = new List<Cart>
                {
                    new Cart(db.Products.Find(id),1)
                };
                Session[strCart] = lsCart;
        }
            else
            {
                List<Cart> lsCart = (List<Cart>)Session[strCart];
                int check = IsExistingcheck(id);
                if (check == -1)
                    lsCart.Add(new Cart(db.Products.Find(id), 1));
                else
                    lsCart[check].Quantity++;
                Session[strCart] = lsCart;
            }
            return View("Index");
        }
        /*Check if the Product Exist in the cart befor*/
        private int IsExistingcheck(int? id)
        {
            List<Cart> lsCart = (List<Cart>)Session[strCart];
            for(int i=0;i<lsCart.Count;i++)
            {
                if (lsCart[i].Product.ID == id) return i;
            }
            return -1;
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int check = IsExistingcheck(id);
            List<Cart> lsCart = (List<Cart>)Session[strCart];
            lsCart.RemoveAt(check);
            return View("Index");
        }


        public ActionResult MakeOrder(string name)
        {
            ApplicationUser user = db.Users.Where(u =>u.UserName == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();
            List<Cart> lsCart = (List<Cart>)Session[strCart];
            List<Product> products = new List<Product>();
            int Q = 0;
            foreach (Cart cart in lsCart)
            {
                products.Add(cart.Product);
                Q = cart.Quantity;
            }

            Order order = new Order
            {
                //Products=products,
                CustomerId = user.Id,
                ShippingAddress = user.Address,
                OrderEmail = user.Email,
                OrderAddress = user.Address,
                OrderStatus = "Proccessing",
                OrderDate = System.DateTime.Now
                , Amount = Q
            };
            
            db.Orders.Add(order);
            db.SaveChanges();
            foreach (Cart cart in lsCart)
            {
                OrderDetails orderDetailes = new OrderDetails
                {
                    OrderId = order.ID,
                    //Product = cart.Product,
                    Quantity = cart.Quantity,
                    Price = cart.Product.Price
                };
                db.OrderDetailes.Add(orderDetailes);
                db.SaveChanges();
            }
            return View("MakeOrder");
        }
    }
}