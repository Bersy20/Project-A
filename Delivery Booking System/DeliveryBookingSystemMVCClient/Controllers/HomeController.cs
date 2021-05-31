using DeliveryBookingSystemMVCClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryBookingSystemMVCClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }
        public IActionResult LogOutPage()
        {
            return RedirectToAction("Home");
        }
        [HttpGet]
        public ActionResult ErrorPage()
        {
            return View();
        }
        public ActionResult EditSuccessPage()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }

    }
}
