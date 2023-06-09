using Microsoft.AspNetCore.Mvc;
using restaurant.appDB;
using restaurant.Models;

namespace restaurant.Controllers
{
    public class CartController : Controller
    {
        AppDB _db;
        public CartController(AppDB db) 
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var ordid = HttpContext.Session.GetInt32("orderId");
            if (HttpContext.Session.GetString("UserName") != null)
            {
                ViewData["IsLoggedIn"] = "yes";
                ViewData["img"] = HttpContext.Session.GetString("img");

            }
            else
            {
                ViewData["IsLoggedIn"] = null;
            }
            if (HttpContext.Session.GetString("IsAdmin") != null)
            {
                ViewData["IsAdmin"] = "yes";
            }
            else
            {
                ViewData["IsAdmin"] = null;
            }
           
            HttpContext.Session.SetInt32("Cart", 3);
            var cart = _db.order_Foods.Where(c => c.order_ID == HttpContext.Session.GetInt32("orderId")).ToList();
            var total = 0;
            foreach (var item in cart) 
            {
                total += _db.foods.Where(c=>c.food_ID==item.food_ID).FirstOrDefault().price * item.qty;
            }
            TempData["total"] = total;
            return View(cart);
            
        }
        
        public IActionResult increase(int id)
        {
      
            var cart=_db.order_Foods.Where(c=>c.id == id).FirstOrDefault();
            if(cart!=null)
            cart.qty++;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult decrease(int id)
        {
            var cart = _db.order_Foods.Where(c => c.id == id).FirstOrDefault();
            if (cart != null)
                cart.qty--;
            if(cart.qty==0)
            {
                _db.order_Foods.Remove(cart);
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
