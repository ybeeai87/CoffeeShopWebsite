using CoffeeShopRegistration.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShopRegistration.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<User> result = null;
            using (CoffeeShopContext context = new CoffeeShopContext())
            {
                result = context.Users.ToList();
            }
            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult NewUser()
        {
            return View();
        }
        public IActionResult SaveCustomer(User user)
        {
            using (CoffeeShopContext context = new CoffeeShopContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
            return RedirectToAction("Result", user);
        }

        public IActionResult Result(User U) //models better than ViewData etc... due to being much faster and works smoother.
        {
            if (U.Password.ToLower() == "password")
            {
              //  ViewData["ErrorMsg"] = "Please enter a different password.";
                return RedirectToAction("NewUser");
            }
           
            return View(U);

            //ViewData["Result"] = $"Welcome {FirstName} {LastName}";
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
