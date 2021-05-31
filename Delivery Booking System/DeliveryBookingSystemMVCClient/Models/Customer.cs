using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryBookingSystemMVCClient.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Enter your Name!!")]
        [RegularExpression("[a-zA-Z ]*",ErrorMessage ="Special Characters are not allowed")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage ="Password cannot be empty!!")]
        public string Password { get; set; }
        public int Age { get; set; }
        [Required(ErrorMessage = "Enter your phone number!!")]
        [MaxLength(10, ErrorMessage = "It exceeded the maximum value")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Should be a number!!")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Address cannot be empty!!")]
        public string Address { get; set; }
        [Required(ErrorMessage = "City cannot be empty!!")]
        public string City { get; set; }
        [Required(ErrorMessage = "Enter Pincode!!")]
        public int PinCode { get; set; }
        public bool IsVerified { get; set; }
    }
}
