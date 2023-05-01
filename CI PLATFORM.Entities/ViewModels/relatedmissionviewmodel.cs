using CI_PLATFORM.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM.Entities.ViewModels
{
    public class relatedmissionviewmodel
    {
        public List<Mission> missionList { get; set; }

        public List<MissionSkill> missionSkillList { get; set;}

        public List<GoalMission> goalMissionList { get; set; }

        public List<Country> GetCountries { get; set; }
        public List<FavouriteMission> favorite { get; set; }

        public List<MissionRating> rate { get; set; }
        public List<MissionMedium> missionMedia { get; set; }


    }
}
