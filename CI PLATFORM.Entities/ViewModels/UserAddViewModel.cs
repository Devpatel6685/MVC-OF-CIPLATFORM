using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM.Entities.ViewModels
{
    public class UserAddViewModel
    {
        public long UserId { get; set; }
        public string? avtar { get; set; }
        [Required(ErrorMessage = "FirstName is required")]
        public string? FirstName { get; set; }
        public IFormFile Avatar { get; set; }
        [Required(ErrorMessage = "LastName is required")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "EmployeeId is required")]
        public string? EmployeeId { get; set; }
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Password must contain atleast 1 lowercase,1 uppercase, 1 digit,1 special character and must be of 8 characters")]

        public string Password { get; set; }
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please Provide Valid Email")]

        public string Email { get; set; }
        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Message is required")]
        public string Message { get; set; }
        [Required(ErrorMessage = "Text is required")]
        public string? WhyIVolunteer { get; set; }
        public string? LinkedInUrl { get; set; }
        public List<SelectListItem> countries { get; set; }
        public List<SelectListItem> cities { get; set; }
        [Required(ErrorMessage = "Department is required")]
        public string? Department { get; set; }
        public long CityId { get; set; }
        public string? ProfileText { get; set; }
        public long CountryId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string? Title { get; set; }
        public int Status { get; set; }

    }
}
