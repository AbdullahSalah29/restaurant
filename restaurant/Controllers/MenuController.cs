using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using restaurant.appDB;
using restaurant.Models;

namespace restaurant.Controllers
{
    public class MenuController : Controller
    {
        private AppDB _db;
        public MenuController(AppDB db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                ViewData["IsLoggedIn"] = "yes";
                ViewData["img"] = HttpContext.Session.GetString("img");
                TempData["check"] = "yes";
            }
            else
            {
                TempData["check"] = null;
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
            
            List<Food> st = _db.foods.ToList();
            
            return View(st);
        }  
        public IActionResult Add(int id)
        {
            var ordid = HttpContext.Session.GetInt32("orderId");
            var order=_db.order_Foods.Where(c=>c.food_ID == id && c.order_ID == HttpContext.Session.GetInt32("orderId")).FirstOrDefault();
            if (order != null) 
            {
                order.qty++;
                TempData[Convert.ToString(id)]=order.qty;
                _db.SaveChanges();
            }
            else
            { var ord=_db.orders.Where(c=>c.order_ID== HttpContext.Session.GetInt32("orderId")).FirstOrDefault();
                if (ord == null)
                {
                    _db.orders.Add(new Order 
                    {
                        
                        customar_name= HttpContext.Session.GetString("name"),
                         user=_db.users.Where(c=>c.username_ID== HttpContext.Session.GetString("Id")).FirstOrDefault(),
                         username_ID= HttpContext.Session.GetString("Id"),
                    });
                    _db.SaveChanges();
                }
                var neworder = new Order_Food
                {
                    food_ID = id,
                    qty = 1,
                    order_ID = (int)(HttpContext.Session.GetInt32("orderId") == null ? 0 : HttpContext.Session.GetInt32("orderId")),
                    food = _db.foods.Where(c => c.food_ID == id).First(),
                    order = _db.orders.Where(c => c.order_ID == HttpContext.Session.GetInt32("orderId")).First()

                };
                _db.order_Foods.Add(neworder);
                _db.SaveChanges();
                TempData[Convert.ToString(id)] = neworder.qty;
            }
            TempData["Last"] = id;
            return RedirectToAction("Index");
        }
        public IActionResult Remove(int id)
        {
            var ordid = HttpContext.Session.GetInt32("orderId");
            var order = _db.order_Foods.Where(c => c.food_ID == id && c.order_ID == HttpContext.Session.GetInt32("orderId")).FirstOrDefault();
            if (order != null)
            {
                if(order.qty>=1)
                order.qty--;
                else 
                {
                    _db.order_Foods.Remove(order);
                }
                TempData[Convert.ToString(id)] = order.qty;
                _db.SaveChanges();
            }
            else
            {
                var ord = _db.orders.Where(c => c.order_ID == HttpContext.Session.GetInt32("orderId")).FirstOrDefault();
                if (ord == null)
                {
                    _db.orders.Add(new Order
                    {

                        customar_name = HttpContext.Session.GetString("name"),
                        user = _db.users.Where(c => c.username_ID == HttpContext.Session.GetString("Id")).FirstOrDefault(),
                        username_ID = HttpContext.Session.GetString("Id"),
                    });
                    _db.SaveChanges();
                }
                var neworder = new Order_Food
                {
                    food_ID = id,
                    qty = 0,
                    order_ID = (int)(HttpContext.Session.GetInt32("orderId") == null ? 0 : HttpContext.Session.GetInt32("orderId")),
                    food = _db.foods.Where(c => c.food_ID == id).First(),
                    order = _db.orders.Where(c => c.order_ID == HttpContext.Session.GetInt32("orderId")).First()

                };
                _db.order_Foods.Add(neworder);
                _db.SaveChanges();
                TempData[Convert.ToString(id)] = neworder.qty;
            }
            TempData["Last"] = id;
            return RedirectToAction("Index");
        }
       
    }
}
