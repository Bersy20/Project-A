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
    public class CustomerController : Controller
    {
        public ActionResult CustomerPage()
        {
            return View();
        }
        [HttpGet]
        public ActionResult RegisterCustomer()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> RegisterCustomer(Customer customer)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:27527/api/Customer/PostCustomer", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<Customer>(apiResponse);
                    //TempData["CustomerId"] = obj.CustomerId;
                    TempData["CustId"] = obj.CustomerId;


                }
            }
            return RedirectToAction("LoginCustomer");
        }
        [HttpGet]
        public ActionResult LoginCustomer()
        {
            Customer customer = new Customer();
            customer.CustomerId = Convert.ToInt32(TempData.Peek("CustId"));
            return View(customer);
        }
        [HttpPost]
        public async Task<ActionResult> LoginCustomer(Customer customer)
        {
            using (var httpClient = new HttpClient())
            {

                StringContent content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:27527/api/Customer/LoginCustomer", content))
                {
                    //customer.CustomerId = Convert.ToInt32(TempData.Peek("CustomerId"));
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<Customer>(apiResponse);
                    TempData["CustomerId"] = obj.CustomerId;
                    if (obj.CustomerId == 0)
                    {
                        return RedirectToAction("CustomerLoginErrorPage");
                    }
                    else
                    {
                        int CustomerId = Convert.ToInt32(TempData.Peek("CustomerId"));
                        if (CustomerId != 0)
                        {
                            TempData["CustomerId"] = customer.CustomerId;

                            return RedirectToAction("CustomerPage");
                        }
                        else
                        {
                            return RedirectToAction("ErrorPage","Home");
                        }
                    }
                }
            }
            return RedirectToAction("ErrorPage");
        }
        public async Task<ActionResult> ListOfCustomers()
        {
            string Baseurl = "http://localhost:27527/";
            var CustomerInfo = new List<Customer>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Customer/AllCustomers");
                if (Res.IsSuccessStatusCode)
                {
                    var CustomerResponse = Res.Content.ReadAsStringAsync().Result;

                    CustomerInfo = JsonConvert.DeserializeObject<List<Customer>>(CustomerResponse);

                }
                return View(CustomerInfo);
            }
        }      
        public async Task<ActionResult> EditCustomerDetails(int id)
        {
            TempData["CustomerId"] = id;
            Customer b = new Customer();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:27527/api/Customer/CustomerById?id=" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    b = JsonConvert.DeserializeObject<Customer>(apiResponse);
                }
            }
            return View(b);
        }
        [HttpPost]
        public async Task<ActionResult> EditCustomerDetails(Customer b)
        {
            int CustomerId = Convert.ToInt32(TempData["CustomerId"]);
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(b), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("http://localhost:27527/api/Customer/" + CustomerId, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<Customer>(apiResponse);
                }
            }
            return RedirectToAction("ListOfCustomers");
        }
        public async Task<ActionResult> DeleteCustomerDetails(int id)
        {
            TempData["CustomerId"] = id;
            Customer b = new Customer();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:27527/api/Customer/CustomerById?id=" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    b = JsonConvert.DeserializeObject<Customer>(apiResponse);
                }
            }
            return View(b);
        }
        [HttpPost]
        public async Task<ActionResult> DeleteCustomerDetails(Customer b)
        {
            int CustomerId = Convert.ToInt32(TempData["CustomerId"]);
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:27527/api/Customer/" + CustomerId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("listOfCustomers");
        }
        public async Task<ActionResult> ListOfVerifiedCustomers()
        {
            string Baseurl = "http://localhost:27527/";
            var CustomerInfo = new List<Customer>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Customer/WithVerifiedStatus");
                if (Res.IsSuccessStatusCode)
                {
                    var CustomerResponse = Res.Content.ReadAsStringAsync().Result;
                    CustomerInfo = JsonConvert.DeserializeObject<List<Customer>>(CustomerResponse);
                }
                return View(CustomerInfo);
            }
        }
        public async Task<ActionResult> ListOfNotVerifiedCustomers()
        {
            string Baseurl = "http://localhost:27527/";
            var CustomerInfo = new List<Customer>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Customer/WithoutVerifiedStatus");
                if (Res.IsSuccessStatusCode)
                {
                    var CustomerResponse = Res.Content.ReadAsStringAsync().Result;
                    CustomerInfo = JsonConvert.DeserializeObject<List<Customer>>(CustomerResponse);
                }
                return View(CustomerInfo);
            }
        }
        public ActionResult CustomerLoginErrorPage()
        {
            return View();
        }
    }
}
