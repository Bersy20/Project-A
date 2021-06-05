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
        public double WeightOfPackage { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public string Phone { get; set; }
        public decimal Price { get; set; }
        public string DeliveryStatus { get; set; } = "Open";
       
    }
}
