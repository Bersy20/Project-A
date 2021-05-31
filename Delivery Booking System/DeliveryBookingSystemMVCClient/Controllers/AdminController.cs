using DeliveryBookingSystemMVCClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryBookingSystemMVCClient.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult AdminPage()
        {
            return View();
        }
        public ActionResult LoginAdmin()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> LoginAdmin(Executive executive)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(executive), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:27527/api/Executive/LoginExecutive", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<Executive>(apiResponse);
                    TempData["ExecutiveId"] = obj.ExecutiveId;
                    TempData["Password"] = obj.Password;
                    if (obj.ExecutiveId == 100 && obj.Password == "Admin")
                    {
                        return RedirectToAction("AdminPage");
                    }

                    else
                    {
                        return RedirectToAction("AdminLoginErrorPage");
                    }

                }
            }
            return RedirectToAction("ErrorPage","Home");
        }
        public ActionResult AdminLoginErrorPage()
        {
            return View();
        }
    }
}
