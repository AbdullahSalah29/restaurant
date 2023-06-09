using Microsoft.AspNetCore.Mvc;
using restaurant.appDB;
using restaurant.Models;

namespace restaurant.Controllers
{
    public class OrderController : Controller
    {
        public AppDB _db;
        public OrderController(AppDB appDB)
        {
            _db = appDB;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                ViewData["IsLoggedIn"] = "yes";
                ViewData["img"] = HttpContext.Session.GetString("img");
            }
            else
            {
                ViewData["IsLoggedIn"] = null;
            }
            if (HttpContext.Session.GetString("UserName")==null) {
            return RedirectToAction("Index","Register");
            }
            if (HttpContext.Session.GetString("IsAdmin") != null)
            {
                ViewData["IsAdmin"] = "yes";
            }
            else
            {
                ViewData["IsAdmin"] = null;
            }
            var order = _db.orders.Where(o => o.order_ID == HttpContext.Session.GetInt32("orderId")).FirstOrDefault();
            if (order == null) { return RedirectToAction("Index", "Menu"); }
            var total = 0;
            var cart = _db.order_Foods.Where(c => c.order_ID == HttpContext.Session.GetInt32("orderId")).ToList();

            foreach (var item in cart)
            {
                total += _db.foods.Where(c => c.food_ID == item.food_ID).FirstOrDefault().price * item.qty;
            }
            order.price = total==0?null:total;
            
            order.user = _db.users.Where(u => u.username_ID == HttpContext.Session.GetString("Id")).FirstOrDefault();
            _db.SaveChanges();  
            return View(order);
        }
        public IActionResult MakeOrder(IFormCollection data) 
        {
            var cart=  _db.order_Foods.Where(x=>x.order_ID==HttpContext.Session.GetInt32("orderId")).ToList();
            if (cart.Count > 0)
            {


                var orderId = HttpContext.Session.Get("orderId");
                var order = _db.orders.Where(o => o.order_ID == HttpContext.Session.GetInt32("orderId")).FirstOrDefault();
                order.phone_number = Convert.ToString(data["number"]);
                order.customar_name = HttpContext.Session.GetString("UserName");
                order.address = Convert.ToString(data["address"]);
                order.DateTime = DateTime.Now;
                order.username_ID = HttpContext.Session.GetString("Id");
                order.price = 0;
                foreach (var item in cart)
                {
                    var p = _db.foods.FirstOrDefault(x => x.food_ID == item.food_ID);
                    order.price += item.qty * p.price;
                    
                }
                _db.SaveChanges();
            }else 
            {
                TempData["error"] = "you have nothing in your cart";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Payment");
        }
        public IActionResult Payment() 
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                ViewData["IsLoggedIn"] = "yes";
                ViewData["img"] = HttpContext.Session.GetString("img");

            }
            else
            {
                ViewData["IsLoggedIn"] = null;
            }
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("Index", "Register");
            }
            if (HttpContext.Session.GetString("IsAdmin") != null)
            {
                ViewData["IsAdmin"] = "yes";
            }
            else
            {
                ViewData["IsAdmin"] = null;
            }
            var orders= _db.order_Foods.Where(o => o.order_ID == HttpContext.Session.GetInt32("orderId")).ToList();
            var price = 0;
            foreach (var item in orders)
            {
                var p = _db.foods.FirstOrDefault(x => x.food_ID == item.food_ID);
                price += item.qty * p.price;

            }
            TempData["total"] = price;
            return View(orders);
        }
        [HttpPost]
        public IActionResult final(IFormCollection data)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                ViewData["IsLoggedIn"] = "yes";
                ViewData["img"] = HttpContext.Session.GetString("img");

            }
            else
            {
                ViewData["IsLoggedIn"] = null;
            }
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("Index", "Register");
            }
            if (HttpContext.Session.GetString("IsAdmin") != null)
            {
                ViewData["IsAdmin"] = "yes";
            }
            else
            {
                ViewData["IsAdmin"] = null;
            }
            string number = Convert.ToString(data["card_number"]);
            string cvv = Convert.ToString(data["cvv"]);
            DateTime date = Convert.ToDateTime(data["date"]);
            if (number.Length >= 16 && cvv.Length==3 && DateTime.Today <date) 
            {
                TempData["ok"] = true;
                var order = _db.orders.Where(o => o.order_ID == HttpContext.Session.GetInt32("orderId")).FirstOrDefault();
                order.payment = true;
                order.messege = "Done";
                _db.payment.Add(new Payment
                {
                    csv = data["cvv"],
                    Master_card = data["card_number"],
                    Name = data["name"],
                    Email=HttpContext.Session.GetString("UserName"),
                    user=_db.users.Where(u=>u.username_ID== HttpContext.Session.GetString("Id")).FirstOrDefault()
                }) ;
                _db.SaveChanges();
                return View();
                

            }
            return RedirectToAction("Payment");
        }
    }
}
