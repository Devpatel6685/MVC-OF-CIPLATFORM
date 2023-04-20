using CI_PLATFORM.Entities.DataModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM.Entities.ViewModels
{
    public class EditUserViewModel
    {


        [Required(ErrorMessage = "Enter Firstname")]

        public string name { get; set; }
        [Required(ErrorMessage = "Enter Lastname")]

        public string surname { get; set; }
        [Required(ErrorMessage = "Enter EmployeeId")]

        public string? employeeId { get; set; }
        public string? managerName { get; set; }
        [Required(ErrorMessage = "Enter Title")]

        public string title { get; set; }
        [Required(ErrorMessage = "Enter Department")]
        public string department { get; set; }
        [Required(ErrorMessage = "Enter Profile text")]

        public string profile { get; set; }
        public string whyIVolunteer { get; set; }
        public List<SelectListItem> countryName { get; set; }
        public List<SelectListItem> cityName { get; set; }

        public long cityId { get; set; }
        public long countryId { get; set; }

        public string? availability { get; set; }
        [Required(ErrorMessage = "Enter Linked in url")]

        public string linkedinURL { get; set; }
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; } = null!;
        [Required(ErrorMessage = "Message is required")]
        public string Message { get; set; } = null!;
        public List<UserSkill> userSkills { get; set; }
        public List<Skill> skills { get; set; }

        [Required(ErrorMessage = "Enter Old Password")]
        public string oldpass { get; set; }

        [Required(ErrorMessage = "Enter New Password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Password must contain atleast 1 lowercase,1 uppercase, 1 digit,1 special character and must be of 8 characters")]
        public string newpass { get; set; }

        [Required(ErrorMessage = "Enter ConfirmPassword")]
        [Compare("newpass", ErrorMessage = "Confirm Password is not match with Password")]
        public string confirmpass { get; set; }

        public string Avatar { get; set; }
    }
}
