using DeliveryBookingSystemMVCClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryBookingProjectMVC.Controllers
{
    public class BookingController : Controller
    {
        public async Task<ActionResult> ListOfBookings()
        {
            string Baseurl = "http://localhost:27527/";
            var BookingInfo = new List<Booking>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Booking/GetAllBookings");
                if (Res.IsSuccessStatusCode)
                {
                    var BookingResponse = Res.Content.ReadAsStringAsync().Result;

                    BookingInfo = JsonConvert.DeserializeObject<List<Booking>>(BookingResponse);

                }
                return View(BookingInfo);
            }
        }
        [HttpGet]
        public ActionResult AddBooking(int id)
        {
            Booking booking = new Booking();
            booking.CustomerId = Convert.ToInt32(TempData.Peek("CustomerId"));
            TempData["ExecId"] = id;
            booking.ExecutiveId = Convert.ToInt32(TempData.Peek("ExecId"));
            return View(booking);
        }
        [HttpPost]
        public async Task<ActionResult> AddBooking(Booking booking)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(booking), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("http://localhost:27527/api/Booking/AddBooking", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<Booking>(apiResponse);
                    TempData["BookingId"] = obj.BookingId;
                }
            }
            return RedirectToAction("ViewBookingDetails");
        }
        public async Task<ActionResult> ViewBookingDetails(Booking booking)
        {
            int BookingId = Convert.ToInt32(TempData.Peek("BookingId"));
            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync("http://localhost:27527/api/Booking/BookingId?id=" + BookingId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    booking = JsonConvert.DeserializeObject<Booking>(apiResponse);
                }
            }
            return View(booking);
        }
        public async Task<ActionResult> CancelBooking(int id)
        {
            Booking booking = new Booking();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:27527/api/Booking/BookingId?id=" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    booking = JsonConvert.DeserializeObject<Booking>(apiResponse);
                    TempData["BookingId"] = booking.BookingId;
                }
            }
            return View(booking);
        }

        [HttpPost]
        public async Task<ActionResult> CancelBooking(Booking booking)
        {
            int BookingId = Convert.ToInt32(TempData.Peek("BookingId"));
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:27527/api/Booking/" + BookingId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("ListOfBookingsByCustomer");
        }
        public async Task<ActionResult> EditBookingDetails(int id)
        {
            Booking booking = new Booking();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:27527/api/Booking/BookingId?id=" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    booking = JsonConvert.DeserializeObject<Booking>(apiResponse);
                    TempData["BookingId"] = booking.BookingId;
                }
            }
            return View(booking);
        }
        [HttpPost]
        public async Task<ActionResult> EditBookingDetails(Booking booking)
        {
            int BookingId = Convert.ToInt32(TempData.Peek("BookingId"));
            int CustomerId = Convert.ToInt32(TempData.Peek("CustomerId"));
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(booking), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("http://localhost:27527/api/Booking/PutBooking?id=" + BookingId, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<Booking>(apiResponse);
                }
            }
            return RedirectToAction("ListOfBookings");
        }
        public async Task<ActionResult> ListOfRequestsForExecutive()
        {
            Executive executive = new Executive();
            var BookingInfo = new List<Booking>();
            int ExecutiveId = Convert.ToInt32(TempData.Peek("ExecutiveId"));
            //int BookingId = Convert.ToInt32(TempData["BookingId"]);
            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync("http://localhost:27527/api/Booking/executiveId?executiveId=" + ExecutiveId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    BookingInfo = JsonConvert.DeserializeObject<List<Booking>>(apiResponse);
                }
            }
            return View(BookingInfo);
        }
        public async Task<ActionResult> ListOfBookingsByCustomer()
        {
            Customer customer = new Customer();
            var BookingInfo = new List<Booking>();
            int CustomerId = Convert.ToInt32(TempData.Peek("CustomerId"));
            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync("http://localhost:27527/api/Booking/customerId?customerId=" + CustomerId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    BookingInfo = JsonConvert.DeserializeObject<List<Booking>>(apiResponse);
                }
            }
            return View(BookingInfo);
        }
        public async Task<ActionResult> DeliveryStatusUpdate(int id)
        {

            TempData["BookingId"] = id;
            Booking booking = new Booking();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:27527/api/Booking/BookingId?id=" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    booking = JsonConvert.DeserializeObject<Booking>(apiResponse);
                    TempData["BookingId"] = booking.BookingId;
                }
            }
            return View(booking);
        }
        [HttpPost]

        public async Task<ActionResult> DeliveryStatusUpdate(Booking booking)
        {
            int BookingId = Convert.ToInt32(TempData.Peek("BookingId"));
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(booking), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("http://localhost:27527/api/Booking/PutBooking?id=" + BookingId, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<Booking>(apiResponse);
                }
            }
            return RedirectToAction("ExecutivePage", "Executive");
        }
        public async Task<ActionResult> ListOfCompletedDelivery()
        {
            string Baseurl = "http://localhost:27527/";
            var BookingInfo = new List<Booking>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Booking/WithCompletedDelivery");
                if (Res.IsSuccessStatusCode)
                {
                    var BookingResponse = Res.Content.ReadAsStringAsync().Result;
                    BookingInfo = JsonConvert.DeserializeObject<List<Booking>>(BookingResponse);
                }
                return View(BookingInfo);
            }
        }
        public async Task<ActionResult> ListOfUnCompletedDelivery()
        {
            string Baseurl = "http://localhost:27527/";
            var BookingInfo = new List<Booking>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Booking/WithUnCompletedDelivery");
                if (Res.IsSuccessStatusCode)
                {
                    var BookingResponse = Res.Content.ReadAsStringAsync().Result;
                    BookingInfo = JsonConvert.DeserializeObject<List<Booking>>(BookingResponse);
                }
                return View(BookingInfo);
            }
        }
        public async Task<ActionResult> ListOfBookingsWithPendingRequestsOfEachExecutive()
        {
            Executive executive = new Executive();
            var BookingInfo = new List<Booking>();
            int ExecutiveId = Convert.ToInt32(TempData.Peek("ExecutiveId"));
            //int BookingId = Convert.ToInt32(TempData["BookingId"]);
            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync("http://localhost:27527/api/Booking/WithPendingRequestsOfEachExecutive?executiveId=" + ExecutiveId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    BookingInfo = JsonConvert.DeserializeObject<List<Booking>>(apiResponse);
                }
            }
            return View(BookingInfo);
        }
        public async Task<ActionResult> ListOfBookingsWithCompletedRequestsOfEachExecutive()
        {
            Executive executive = new Executive();
            var BookingInfo = new List<Booking>();
            int ExecutiveId = Convert.ToInt32(TempData.Peek("ExecutiveId"));
            //int BookingId = Convert.ToInt32(TempData["BookingId"]);
            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync("http://localhost:27527/api/Booking/WithCompletedRequestsOfEachExecutive?executiveId=" + ExecutiveId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    BookingInfo = JsonConvert.DeserializeObject<List<Booking>>(apiResponse);
                }
            }
            return View(BookingInfo);
        }
    }
}
