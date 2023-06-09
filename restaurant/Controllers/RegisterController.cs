using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restaurant.appDB;
using restaurant.Models;
using System.Security.Cryptography.X509Certificates;

namespace restaurant.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly AppDB _db;

        public RegisterController(IWebHostEnvironment environment, AppDB db)
        {
            _environment = environment;
            _db = db;
        }
        public IActionResult Login()
        {
            return View();
        }
        private readonly IWebHostEnvironment _webHostEnvironment;
        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("IsAdmin") != null)
            {
                ViewData["IsAdmin"] = "yes";
                ViewData["img"] = HttpContext.Session.GetString("img");

            }
            else
            {
                ViewData["IsAdmin"] = null;
            }
            User user1 = new User();
            TempData["repass"] = "";
            return View(user1);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("Password", "name", "email")] User data, IFormFile img_file,string repass)
        {

            ViewBag.error = true;
            ViewBag.errors = new List<string>();
            var newuser = _db.users.Where(u => u.email == data.email).FirstOrDefault();
            if (newuser != null) { ViewBag.errors.Add("* this email was used befor try another");
                ViewBag.error = false;
            }
            if (data.Password != repass)
            {
                ViewBag.error = false;
              
                ViewBag.errors.Add("* Password and the Confirm Doesnt Match");
                
                if (newuser != null) { ViewBag.errors.Add("* this email was used befor try another"); }
            
            }
            else if(ViewBag.error)
            {
                // Validate and process other user data

                string path = Path.Combine(_environment.WebRootPath, "imgs"); // wwwroot/Img/
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (img_file != null)
                {
                    path = Path.Combine(path, img_file.FileName); // for exmple : /Img/Photoname.png
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        img_file.CopyTo(stream);
                        ViewBag.Message = string.Format("<b>{0}</b> uploaded.</br>", img_file.FileName.ToString());
                    }
                    data.img = img_file.FileName;
                }
                else
                {
                    data.img = "default.jpg"; // to save the default image path in database.
                }
                try
                {
                    _db.users.Add(data);
                    _db.SaveChanges();
                    return RedirectToAction("Login");
                }
                catch (Exception ex) { ViewBag.exc = ex.Message; }
            }
            if (HttpContext.Session.GetString("IsAdmin") != null)
            {
                ViewData["IsAdmin"] = "yes";
            }
            else
            {
                ViewData["IsAdmin"] = null;
            }
            return View(data);
        
        }

    
        
       
      
        public IActionResult succsess() 
        {
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]  // to more security
        
        [HttpPost]
        public IActionResult Login(IFormCollection data)
        {
            AppDB db = new AppDB();

            User valid = db.users.FirstOrDefault(c => c.email == Convert.ToString(data["userName"]) && c.Password == Convert.ToString(data["Password"]));
            if (data["userName"] == "Admin@Admin.com" && data["Password"] == "12345678")
            {
                HttpContext.Session.SetString("UserName", "Admin");
                HttpContext.Session.SetString("Password", "Admin");
                HttpContext.Session.SetInt32("IsAdmin", 1);

                var img = "default.jpg" ;
                HttpContext.Session.SetString("img", img);

                return RedirectToAction("Index", "Home");

            }
            else if (valid != null)
            {
                HttpContext.Session.SetString("UserName", valid.email);
                HttpContext.Session.SetString("Password", valid.Password);
                HttpContext.Session.SetString("Id", valid.username_ID);
                var img = valid.img == null ? "imgs/default.jpg" : valid.img;
                HttpContext.Session.SetString("img", img);
                Order ord = _db.orders.Where(c=>c.username_ID==valid.username_ID).OrderBy(c=>c.order_ID).LastOrDefault();
                var or = _db.orders.OrderBy(c => c.order_ID).LastOrDefault() == null ? 0 : _db.orders.OrderBy(c => c.order_ID).LastOrDefault().order_ID;
                var num = ord == null ? or + 1 : ord.order_ID;
                if (ord!=null&&ord.payment!=null)
                {
                    num++;
                }
                HttpContext.Session.SetInt32("orderId", num );
              //  HttpContext.Session.Set("IsAdmin", null);
                return RedirectToAction("Index", "Home");
            }
            else ViewBag.Message = string.Format("<b></b> Invaild User Name or password.</br>");
            if (HttpContext.Session.GetString("IsAdmin") != null)
            {
                ViewData["IsAdmin"] = "yes";
            }
            else
            {
                ViewData["IsAdmin"] = null;
            }
            return View();

        }
        public IActionResult Logout() {
            HttpContext.Session.Clear();
           
            return RedirectToAction("Index", "Home");
        }
    }
}
