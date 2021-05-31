using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryBookingSystemMVCClient.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
       [Required(ErrorMessage = "CustomerId cannot be empty")]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "ExecutiveId cannot be empty!!")]
        public int ExecutiveId { get; set; }
        [Required(ErrorMessage = "Select Date and Time of Pickup!!")]
        public DateTime DateTimeOfPickUp { get; set; }
        //[Required(ErrorMessage = "Enter the weight!!")]
        public string WeightOfPackage { get; set; }
        //[Required(ErrorMessage = "Address cannot be empty!!")]
        public string Address { get; set; }
        [Required(ErrorMessage = "City cannot be empty!!")]
        public string City { get; set; }
        [Required(ErrorMessage = "Pincode cannot be empty!!")]
        public int PinCode { get; set; }
        [Required(ErrorMessage = "Enter your phone number!!")]
        [MaxLength(10,ErrorMessage ="It exceeded the maximum value")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Should be a number!!")]
        public string Phone { get; set; }
        public string Price { get; set; }= "Rs 1000";
        public string DeliveryStatus { get; set; } = "Open";
    }
}
