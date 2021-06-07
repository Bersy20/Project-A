using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryBookingSystemMVCClient.Models
{
    public class Admin
    {       
        [Key]
        [Display(Name = "Admin Id")]
        public int AdminId { get; set; }
        [Required(ErrorMessage = "Enter your Name!!")]
        [RegularExpression("[a-zA-Z ]*", ErrorMessage = "Special Characters are not allowed")]
        [Display(Name = "Admin Name")]
        public string AdminName { get; set; }
        [Display(Name = "Username (Email ID)")]
        public string AdminEmail { get; set; }
        [Required(ErrorMessage = "Password cannot be empty!!")]
        [MinLength(4, ErrorMessage = "It should have atleast 4 characters")]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password is not matching")]
        public string ConfirmPassword { get; set; }
        public int Age { get; set; }
        [Required(ErrorMessage = "Enter your phone number!!")]
        [MaxLength(10, ErrorMessage = "It should be 10 digits")]
        [MinLength(10, ErrorMessage = "It should be 10 digits")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Should be a number!!")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Address cannot be empty!!")]
        public string Address { get; set; }
        [Required(ErrorMessage = "City cannot be empty!!")]
        public string City { get; set; }
        [Required(ErrorMessage = "Enter Pincode!!")]
        [MaxLength(6, ErrorMessage = "It exceeded the maximum value")]
        public string PinCode { get; set; }
    }
}
