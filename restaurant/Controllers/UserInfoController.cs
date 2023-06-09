using Microsoft.AspNetCore.Mvc;
using restaurant.appDB;
using restaurant.Models;

namespace restaurant.Controllers
{
    public class UserInfoController : Controller
    {
        AppDB _db;
        public UserInfoController(AppDB db)
        {
            _db = db;
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
            ViewUserOrdersTabel viewModel = new ViewUserOrdersTabel();

            viewModel.User=_db.users.Where(u=>u.username_ID==HttpContext.Session.GetString("Id")).FirstOrDefault();
            viewModel.Tabel = _db.tabel.Where(u => u.user.username_ID == viewModel.User.username_ID).ToList();
            viewModel.Order= _db.orders.Where(u => u.username_ID == viewModel.User.username_ID).ToList();
            viewModel.Food = new List<List<Order_Food>>();
            foreach (var order in viewModel.Order)
            {
                if (order.payment == true)
                {
                    List<Order_Food> list = _db.order_Foods.Where(o => o.order_ID == order.order_ID).ToList();
                    viewModel.Food.Add(list);
                
                } }
            ViewData["no"] = "readonly";
            return View(viewModel);
        }
    }
}
