﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryBookingSystemMVCClient.Models
{
    public class Booking
    {
        [Key]
        [Display(Name ="Booking Id")]
        public int BookingId { get; set; }
        [Required(ErrorMessage = "CustomerId cannot be empty")]
        [Display(Name = "Customer Id")]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "ExecutiveId cannot be empty!!")]
        [Display(Name = "Executive Id")]
        public int ExecutiveId { get; set; }
        [Required(ErrorMessage = "Select Date and Time of Pickup!!")]
        [Display(Name = "Date and Time of PickUp")]

        public DateTime DateTimeOfPickUp { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Enter the weight!!")]
        [Display(Name = "Weight Of Package(in Kg)")]
        public float WeightOfPackage { get; set; }
        [Required(ErrorMessage = "Address cannot be empty!!")]
        public string Address { get; set; }
        [Required(ErrorMessage = "City cannot be empty!!")]
        public string City { get; set; }
        [Required(ErrorMessage = "Pincode cannot be empty!!")]
        public int PinCode { get; set; }
        [Required(ErrorMessage = "Enter your phone number!!")]
        [MaxLength(10,ErrorMessage ="It exceeded the maximum value")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Should be a number!!")]
        public string Phone { get; set; }
        [Display(Name = "Price(in Rs)")]
        public float Price { get; set; }
        [Display(Name = "Delivery Status")]
        public string DeliveryStatus { get; set; } = "Open";
    }
}
