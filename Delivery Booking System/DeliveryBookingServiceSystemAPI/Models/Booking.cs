using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryBookingServiceSystemAPI.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get;  set; }        
        [ForeignKey("Executive")]
        public int ExecutiveId { get; set; }       
        public  DateTime DateTimeOfPickUp { get; set; }
        public string WeightOfPackage { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int PinCode { get; set; }
        public string Phone { get; set; }
        public string Price { get; set; }= "Rs 1000";
        public string DeliveryStatus { get; set; } = "Open";
       
    }
}
