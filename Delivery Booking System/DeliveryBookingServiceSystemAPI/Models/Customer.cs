using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryBookingServiceSystemAPI.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int PinCode { get; set; }
        public bool IsVerified { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
