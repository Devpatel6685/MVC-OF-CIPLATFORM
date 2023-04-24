using CI_PLATFORM.Entities.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM.Entities.ViewModels
{
    public class MissionAddViewModel
    {
        public string Title { get; set; } = null!;
        public DateTime? StartDate { get; set; }
        public string? Description { get; set; }
        public DateTime? EndDate { get; set; }
        public long ThemeId { get; set; }
        public int? TotalSeats { get; set; }
        public string? ShortDescription { get; set; }
        public List<SelectListItem> countries { get; set; }
        public List<SelectListItem> cities { get; set; }
        public List<SelectListItem> Themes { get; set; }
        public List<IFormFile> Images { get; set; }
        public long CountryId { get; set; }
        public int? Status { get; set; }
        public long CityId { get; set; }
        public string? OrganizationName { get; set; }
        public string? OrganizationDetail { get; set; }
        public List<UserSkill> userSkills { get; set; }
        public List<Skill> skills { get; set; }
        public string? Availibility { get; set; }
        public string? MissionType { get; set; }
        public string? GoalObjectiveText { get; set; }
    }
}
