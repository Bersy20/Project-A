using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryBookingServiceSystemAPI.Models
{
    public class Executive
    {
        [Key]
        public int ExecutiveId { get; set; }
        public string ExecutiveName { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int PinCode { get; set; }
        public bool IsVerified { get; set; }
        public string ExecutiveStatus { get; set; } = "Available";
        public List<Booking> Bookings { get; set; }
    }
}
