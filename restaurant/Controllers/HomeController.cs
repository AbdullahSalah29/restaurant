using Microsoft.AspNetCore.Mvc;
using restaurant.appDB;
using restaurant.Models;
using System.Diagnostics;

namespace restaurant.Controllers
{
    public class HomeController : Controller
    {
        public AppDB _db;
       
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,AppDB db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {_db.Database.EnsureCreated();
            if(TempData.Count == 0) { TempData["err"] = null; }
            if(HttpContext.Session.GetString("UserName")!=null)
            {
                ViewData["IsLoggedIn"] = "yes";
                ViewData["img"] = HttpContext.Session.GetString("img");

            }
            else
            {
                ViewData["IsLoggedIn"] = null;
            }
            if(HttpContext.Session.GetString("IsAdmin") != null)
            {
                ViewData["IsAdmin"]="yes";
            }
            else
            {
                ViewData["IsAdmin"] = null;
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Reserve(IFormCollection data)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                ViewData["IsLoggedIn"] = "yes";
                var table = new Tabel();
                table.user = _db.users.Where(u => u.username_ID == HttpContext.Session.GetString("Id")).FirstOrDefault();
                table.Time = Convert.ToDateTime(data["time"]);
                table.Date = Convert.ToDateTime(data["date"]);
                table.KindOfFood = Convert.ToString(data["kind"]);
                table.Mobile = Convert.ToString(data["mobile"]);
                _db.tabel.Add(table);
                _db.SaveChanges();
                TempData["err"] = "table resverved successfuly";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["err"] = "you must login first";
                ViewData["IsLoggedIn"] = null;
                return RedirectToAction("Index");
            }
        }
    }
}