using CI_PLATFORM.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace CI_PLATFORM.Entities.ViewModels
{
    public class VolunteerMissionViewmodel
    {
        public long MissionId { get; set; }

        public string Theme { get; set; }

        public string City { get; set; }

        public List<Comment> comments { get; set; }
        public List<MissionSkill> MissionSkills { get; set; }

        public IPagedList<MissionApplication> MissionApplications { get; set; }
        public long CountryId { get; set; }

        public string Title { get; set; }

        public List<MissionRating> rate { get; set; }
        public string? ShortDescription { get; set; }

        public string? Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public List<MissionDocument> Documents { get; set; }
        public string? MissionType { get; set; }

        public int? Status { get; set; }

        public string? OrganizationName { get; set; }

        public string? OrganizationDetail { get; set; }

        public int? Availability { get; set; }


        public string? days { get; set; }
        public bool favorite { get; set; }

        public bool applied { get; set; }
        public string? goalObjective { get; set; }
        public int? goalvalue { get; set; }
    }
}
