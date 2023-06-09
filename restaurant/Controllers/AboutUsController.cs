using Microsoft.AspNetCore.Mvc;

namespace restaurant.Controllers
{
    public class AboutUsController : Controller
    {
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
    }
}
