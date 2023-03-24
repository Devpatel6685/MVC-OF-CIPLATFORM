using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM.Entities.ViewModels
{
   public class registerviewmodel
    {
        
        [Required(ErrorMessage = "Enter Firstname")]
        [MinLength(2, ErrorMessage = "Invalid First Name")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Enter Lastname")]
        [MinLength(2, ErrorMessage = "Invalid Last Name")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Enter Email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please Provide Valid Email")]
        public string Email { get; set; } 

        [Required(ErrorMessage = "Enter Password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Password must contain atleast 1 lowercase,1 uppercase, 1 digit,1 special character and must be of 8 characters")]
        public string Password { get; set; } 

        [Required(ErrorMessage = "Enter ConfirmPassword")]
        [Compare("Password", ErrorMessage = "Confirm Password is not same as Password")]
        public string ConfirmPassword { get; set; } 

        [Required(ErrorMessage = "Enter Phone number")]

        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Invalid Mobile no.")]
        public long? PhoneNumber { get; set; }

        public long CityId = 1;

        public long CountryId = 1;
    }
}
