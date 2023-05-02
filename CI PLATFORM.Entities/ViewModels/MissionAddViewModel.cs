using CI_PLATFORM.Entities.DataModels;
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
    public class MissionAddViewModel
    {
        public long MissionId { get; set; }
        public string Title { get; set; } = null!;
        [Required(ErrorMessage = "Enter Start Date")]

        public DateTime? StartDate { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Enter End Date")]

        public DateTime? EndDate { get; set; }
        public long ThemeId { get; set; }
        [Required(ErrorMessage = "Enter Total Seats")]
        public int? TotalSeats { get; set; }
        [Required(ErrorMessage = "Enter Short Description")]
        public string? ShortDescription { get; set; }
        [Required(ErrorMessage = "Enter Registration Date")]
        public DateTime? RegistrationDeadline { get; set; }
        public List<SelectListItem> countries { get; set; }
        public List<SelectListItem> cities { get; set; }
        public List<SelectListItem> Themes { get; set; }
        public List<IFormFile>? Images { get; set; }
        public List<IFormFile>? Documents { get; set; }
        public long CountryId { get; set; }
        [Required(ErrorMessage = "Enter Status")]
        public int? Status { get; set; }
        public long CityId { get; set; }
        [Required(ErrorMessage = "Enter Organization Name")]
        public string OrganizationName { get; set; }
        [Required(ErrorMessage = "Enter Organization Detail")]
        public string OrganizationDetail { get; set; }
        public List<UserSkill> userSkills { get; set; }
        public List<Skill> Skills { get; set; }
        [Required(ErrorMessage = "Enter Availability")]

        public string Availibility { get; set; }
        [Required(ErrorMessage = "Enter MissonType")]

        public string MissionType { get; set; }
        [Required(ErrorMessage = "Enter Goal Objective Text")]

        public string GoalObjectiveText { get; set; }
        [Required(ErrorMessage = "Enter Goal Value")]

        public int GoalValue { get; set; }

        public List<int> skillids { get; set; }
    }
}
