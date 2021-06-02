using DeliveryBookingSystemMVCClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryBookingSystemMVCClient.Controllers
{
    public class ExecutiveController : Controller
    {
        [HttpGet]
        public ActionResult RegisterExecutive()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> RegisterExecutive(Executive executive)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(executive), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:27527/api/Executive/PostExecutive", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<Executive>(apiResponse);
                    //TempData["ExecutiveId"] = obj.ExecutiveId;
                    TempData["ExId"] = obj.ExecutiveId;
                }
            }
            TempData["Success"] = "You have sucessfully Registered...";
            return RedirectToAction("LoginExecutive");
        }
        [HttpGet]
        public ActionResult LoginExecutive()
        {
            Executive executive = new Executive();
            executive.ExecutiveId = Convert.ToInt32(TempData.Peek("ExId"));
            return View(executive);
        }
        [HttpPost]
        public async Task<ActionResult> LoginExecutive(Executive executive)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(executive), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:27527/api/Executive/LoginExecutive", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<Executive>(apiResponse);
                    TempData["ExecutiveId"] = obj.ExecutiveId;
                    if (obj.ExecutiveId == 0)
                    {
                        return RedirectToAction("ExecutiveLoginErrorPage");
                    }
                    else
                    {
                        int ExecutiveId = Convert.ToInt32(TempData.Peek("ExecutiveId"));
                        if (ExecutiveId != 0)
                        {
                            TempData["ExecutiveId"] = executive.ExecutiveId;
                            return RedirectToAction("ExecutivePage");
                        }
                        else
                        {
                            return RedirectToAction("ErrorPage","Home");
                        }
                    }
                }
            }
        }
        [HttpGet]
        
        public async Task<ActionResult> ListOfExecutive()
        {
            string Baseurl = "http://localhost:27527/";
            var ExecutiveInfo = new List<Executive>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Executive");
                if (Res.IsSuccessStatusCode)
                {
                    var ExecutiveResponse = Res.Content.ReadAsStringAsync().Result;

                    ExecutiveInfo = JsonConvert.DeserializeObject<List<Executive>>(ExecutiveResponse);

                }
                return View(ExecutiveInfo);
            }
        }
        public async Task<ActionResult> ListOfExecutiveAvailableForCustomerView()
        {
           
            string Baseurl = "http://localhost:27527/";
            var ExecutiveInfo = new List<Executive>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Executive/WithStatusCheck");
                if (Res.IsSuccessStatusCode)
                {
                    var ExecutiveResponse = Res.Content.ReadAsStringAsync().Result;

                    ExecutiveInfo = JsonConvert.DeserializeObject<List<Executive>>(ExecutiveResponse);

                }
                return View(ExecutiveInfo);
            }
        }
        public async Task<ActionResult> EditExecutiveStatus(int id)
        {
            id= Convert.ToInt32(TempData["ExecutiveId"]) ;
            Executive executive = new Executive();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:27527/api/Executive/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    executive = JsonConvert.DeserializeObject<Executive>(apiResponse);
                    TempData["ExecutiveId"] = executive.ExecutiveId;
                    TempData["ExecutiveName"] = executive.ExecutiveName;
                    TempData["Address"] = executive.Address;
                    TempData["Age"] = executive.Age;
                    TempData["ExecutiveStatus"] = executive.ExecutiveStatus;
                    TempData["City"] = executive.City;
                    TempData["Phone"] = executive.Phone;

                }
            }
            return View(executive);
        }
        [HttpPost]
        public async Task<ActionResult> EditExecutiveStatus(Executive b)
        {
            int ExecutiveId = Convert.ToInt32(TempData["ExecutiveId"]);
            string ExecutiveName = TempData["ExecutiveName"].ToString();
            string Address = TempData["Address"].ToString();
            int Age = Convert.ToInt32(TempData["Age"]);
            string ExecutiveStatus = TempData["ExecutiveStatus"].ToString();
            string City = TempData["City"].ToString();
            string Phone = TempData["Phone"].ToString();

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(b), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("http://localhost:27527/api/Executive/" + ExecutiveId, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<Executive>(apiResponse);
                }
            }
            return RedirectToAction("ExecutivePage");
        }
        public async Task<ActionResult> EditExecutiveDetails(int id)
        {
            TempData["ExecutiveId"] = id;
            Executive b = new Executive();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:27527/api/Executive/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    b = JsonConvert.DeserializeObject<Executive>(apiResponse);
                }
            }
            return View(b);
        }
        [HttpPost]
        public async Task<ActionResult> EditExecutiveDetails(Executive b)
        {
            int ExecutiveId = Convert.ToInt32(TempData["ExecutiveId"]);
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(b), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("http://localhost:27527/api/Executive/" + ExecutiveId, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<Executive>(apiResponse);
                }
            }
            return RedirectToAction("ListOfExecutive");
        }

        public async Task<ActionResult> DeleteExecutiveDetails(int id)
        {
            TempData["ExecutiveId"] = id;
            Executive b = new Executive();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:27527/api/Executive/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    b = JsonConvert.DeserializeObject<Executive>(apiResponse);
                }
            }
            return View(b);
        }
        [HttpPost]
        public async Task<ActionResult> DeleteExecutiveDetails(Executive b)
        {
            int ExecutiveId = Convert.ToInt32(TempData["ExecutiveId"]);
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:27527/api/Executive/" + ExecutiveId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("ListOfExecutive");
        }
        public ActionResult ExecutivePage()
        {
            return View();
        }
        public async Task<ActionResult> ListOfVerifiedExecutives()
        {
            string Baseurl = "http://localhost:27527/";
            var ExecutiveInfo = new List<Executive>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Executive/WithVerifiedStatus");
                if (Res.IsSuccessStatusCode)
                {
                    var ExecutiveResponse = Res.Content.ReadAsStringAsync().Result;
                    ExecutiveInfo = JsonConvert.DeserializeObject<List<Executive>>(ExecutiveResponse);
                }
                return View(ExecutiveInfo);
            }
        }
        public async Task<ActionResult> ListOfNotVerifiedExecutives()
        {
            string Baseurl = "http://localhost:27527/";
            var ExecutiveInfo = new List<Executive>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Executive/WithoutVerifiedStatus");
                if (Res.IsSuccessStatusCode)
                {
                    var ExecutiveResponse = Res.Content.ReadAsStringAsync().Result;
                    ExecutiveInfo = JsonConvert.DeserializeObject<List<Executive>>(ExecutiveResponse);
                }
                return View(ExecutiveInfo);
            }
        }
        public async Task<ActionResult> ListOfCompletedDeliveryRequests()
        {
            string Baseurl = "http://localhost:27527/";
            var BookingInfo = new List<Booking>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Booking/GetBookingRequestsOfExecutiveWithCompletedDeliveryStatus");
                if (Res.IsSuccessStatusCode)
                {
                    var BookingResponse = Res.Content.ReadAsStringAsync().Result;
                    BookingInfo = JsonConvert.DeserializeObject<List<Booking>>(BookingResponse);
                }
                return View(BookingInfo);
            }
        }
        public async Task<ActionResult> ListOfPendingDeliveryRequests()
        {
            string Baseurl = "http://localhost:27527/";
            var BookingInfo = new List<Booking>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Booking/GetBookingRequestsOfExecutiveWithPendingDeliveryStatus");
                if (Res.IsSuccessStatusCode)
                {
                    var BookingResponse = Res.Content.ReadAsStringAsync().Result;
                    BookingInfo = JsonConvert.DeserializeObject<List<Booking>>(BookingResponse);
                }
                return View(BookingInfo);
            }
        }
        public ActionResult ExecutiveLoginErrorPage()
        {
            return View();
        }
        public async Task<ActionResult> GetExecutivesByCity(string city)
        {
           
            city = TempData.Peek("Result").ToString();
            string Baseurl = "http://localhost:27527/";
            var ExecutiveInfo = new List<Executive>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Executive/WithCity?city="+city);
                if (Res.IsSuccessStatusCode)
                {
                    var ExecutiveResponse = Res.Content.ReadAsStringAsync().Result;
                    ExecutiveInfo = JsonConvert.DeserializeObject<List<Executive>>(ExecutiveResponse);
                }
                return View(ExecutiveInfo);
            }
        }
        [HttpPost]
        [ActionName("ListOfExecutiveAvailableForCustomerView")]
        public IActionResult ListOfExecutiveAvailableForCustomerViewPost()
        {
            string SearchCity = Request.Form["City"];
            TempData["Result"] = SearchCity;
            return RedirectToAction("GetExecutivesByCity");
        }
    }
}
