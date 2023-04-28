using CI_PLATFORM.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace CI_PLATFORM.Entities.ViewModels
{
    public class MissionViewmodel
    { public int totalrecords { get; set; }
        public int currentPage { get; set; }
     
        public List<Country> Country { get; set; }
        public List<City> City { get; set; }
        public IPagedList<Mission> Missions { get; set; }
        public List<Skill> Skill { get; set; }
        public List<MissionTheme> MissionThemes { get; set; }
        public List<Timesheet> timeSheetEntryList { get; set; }

        public List<GoalMission> goalMissionList { get; set; }

        public List<MissionRating> rate { get; set; }
        public List<MissionSkill> MissionSkills { get; set; }

        public List<FavouriteMission> favorite { get; set; }

        public List<MissionApplication> MissionApplications { get; set; }

    }
}
