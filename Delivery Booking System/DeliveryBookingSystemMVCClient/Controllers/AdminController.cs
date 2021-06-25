using DeliveryBookingSystemMVCClient.Models;
using Microsoft.AspNetCore.Http;
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
        [HttpGet]
        public ActionResult LoginAdmin()
        {
            Admin admin = new Admin();
            try
            {
                admin.AdminEmail = TempData.Peek("AdminEmail").ToString();
                return View(admin);
            }
            catch (Exception e)
            {
                return View(admin);
            }
        }
        [HttpPost]
        public async Task<ActionResult> LoginAdmin(Admin admin)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(admin), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:27527/api/Admin/LoginAdmin", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<Admin>(apiResponse);
                    TempData["AdminEmail"] = obj.AdminEmail;
                    if (obj.AdminEmail == null)
                    {
                        return RedirectToAction("AdminLoginErrorPage");
                    }
                    else
                    {
                        if (obj.AdminEmail != null)
                        {
                            return RedirectToAction("AdminPage");
                        }
                        else
                        {
                            return RedirectToAction("ErrorPage", "Home");
                        }
                    }
                }
            }
        }
        public ActionResult AdminLoginErrorPage()
        {
            return View();
        }
        public async Task<ActionResult> UpdatePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> UpdatePassword(IFormCollection frm, string action)
        {
            if (action == "Submit")
            {

                int id = Convert.ToInt32(frm["txtAId"]);
                TempData["id"] = id;
                string name = frm["txtUserName"];
                TempData["AdminEmail"] = name;
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://localhost:27527/api/Admin/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var obj = JsonConvert.DeserializeObject<Admin>(apiResponse);
                        if (obj.AdminId == id && obj.AdminEmail == name)
                        {
                            return RedirectToAction("UpdatePassword2");
                        }
                        else
                        {
                            TempData["errorA"] = "Invalid User Name or ID...";
                            return RedirectToAction("LoginAdmin");
                        }
                    }
                }
            }
            return RedirectToAction("LoginAdmin");
        }
        public async Task<ActionResult> UpdatePassword2()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> UpdatePassword2(IFormCollection frm, string action)
        {
            if (action == "Submit")
            {
                Admin admin = new Admin();
                int id = Convert.ToInt32(TempData.Peek("id"));
                string password = frm["txtPass"];
                string confirmPassword = frm["txtConPass"];
                if (password == confirmPassword)
                {
                    using (var httpClient = new HttpClient())
                    {
                        StringContent content = new StringContent(password, Encoding.UTF8, "application/json");

                        using (var response = await httpClient.PutAsync("http://localhost:27527/api/Admin/UpdatePassword?id=" + id + "&password=" + password, content))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                        }
                    }
                    TempData["passA"] = "Password sucessfully updated...Login with Your new password";
                    return RedirectToAction("LoginAdmin");
                }
                TempData["passErrA"] = "Check the Password there is a Mismatch...";
                return RedirectToAction("UpdatePassword2");
            }
            return RedirectToAction("UpdatePassword");
        }
    }
}
