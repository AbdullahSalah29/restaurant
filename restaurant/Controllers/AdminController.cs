using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restaurant.appDB;
using restaurant.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace restaurant.Controllers
{
    public class adminController : Controller
    {


        private AppDB db;
        public adminController(AppDB db)
        {
            this.db = db;
        }

        public IActionResult show()
        {
            if (HttpContext.Session.GetString("IsAdmin") != null)
            {
                ViewData["IsAdmin"] = "yes";
                ViewData["img"] = HttpContext.Session.GetString("img");

            }
            else
            {
                ViewData["IsAdmin"] = null;
              return  RedirectToAction("Index","Home");
            }
            if (HttpContext.Session.GetString("UserName") != null)
            {
                ViewData["IsLoggedIn"] = "yes";
            }
            else
            {
                ViewData["IsLoggedIn"] = null;
            }
            List<Food> st = db.foods.ToList();
            return View(st);
        }


        public IActionResult Create() {
            if (HttpContext.Session.GetString("IsAdmin") != null)
            {
                ViewData["IsAdmin"] = "yes";
            }
            else
            {
                ViewData["IsAdmin"] = null;
                return RedirectToAction("Index", "Home");
            }
            if (HttpContext.Session.GetString("UserName") != null)
            {
                ViewData["IsLoggedIn"] = "yes";
            }
            else
            {
                ViewData["IsLoggedIn"] = null;
            }

            return View();
        }

        [HttpPost]
        public IActionResult Create(Food fd, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                // Generate a unique file name for the image
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

                // Set the relative path to the image in the wwwroot/images folder
                string imagePath = Path.Combine("imgs", fileName);

                // Save the image to the wwwroot/images folder
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagePath);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }

                // Update the ImagePath property of the model
                fd.img = imagePath;
                db.foods.Add(fd);
                db.SaveChanges();
                return RedirectToAction("show");
            }
            return View();
            // Save the model to the database

        }

        /*[HttpPost]
        public IActionResult Create(food fd)
        {
           
            db.foods.Add(fd);
            db.SaveChanges();
            return RedirectToAction("show");
        }
        */
        



        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("IsAdmin") != null)
            {
                ViewData["IsAdmin"] = "yes";
            }
            else
            {
                ViewData["IsAdmin"] = null;
                return RedirectToAction("Index", "Home");
            }
            if (HttpContext.Session.GetString("UserName") != null)
            {
                ViewData["IsLoggedIn"] = "yes";
            }
            else
            {
                ViewData["IsLoggedIn"] = null;
            }

            var fd = db.foods.Where(x => x.food_ID == id).FirstOrDefault();
            if (fd == null)
            {
                return new NotFoundResult();
            }
            return View(fd);
        }

        [HttpPost]
        public IActionResult Edit(Food fd,int id,IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                // Generate a unique file name for the image
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

                // Set the relative path to the image in the wwwroot/images folder
                string imagePath = Path.Combine("imgs", fileName);

                // Save the image to the wwwroot/images folder
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagePath);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }

                // Update the ImagePath property of the model
                fd.img = imagePath;
         
            var existingFood = db.foods.FirstOrDefault(x => x.food_ID == id);
            

            existingFood.food_name = fd.food_name;
            existingFood.type = fd.type;
            existingFood.price = fd.price;
            existingFood.food_description = fd.food_description;
                existingFood.img = imagePath;
            //db.foods.Update(fd);
                db.SaveChanges();
                return RedirectToAction("show");
            }
            return RedirectToAction("Edit");
        }


        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("IsAdmin") != null)
            {
                ViewData["IsAdmin"] = "yes";
            }
            else
            {
                ViewData["IsAdmin"] = null;
                return RedirectToAction("Index", "Home");
            }
            if (HttpContext.Session.GetString("UserName") != null)
            {
                ViewData["IsLoggedIn"] = "yes";
            }
            else
            {
                ViewData["IsLoggedIn"] = null;
            }
            Food fd = db.foods.Find(id);
            return View(fd);
        }
        [HttpPost]
        public IActionResult Delete(Food fd,int id)
        {
            fd = db.foods.Find(id);
            if (fd != null)
            {
                db.foods.Remove(fd);
                db.SaveChanges();
            }
            return RedirectToAction("show");
        }

    }
}
