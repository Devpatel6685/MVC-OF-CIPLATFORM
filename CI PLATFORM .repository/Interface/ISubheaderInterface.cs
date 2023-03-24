using CI_PLATFORM.Entities.DataModels;
using CI_PLATFORM.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM_.repository.Interface
{
    public interface ISubheaderInterface
    {
        public List<Country> GetCountryList();
        public List<City> GetCityList();
        public List<MissionTheme> GetMissionThemeList();
        public  List<Skill>GetSkillsList();

        public List<MissionSkill>GetMissionSkillsList();
         
        public List<Country> GetCountries();
        public List<City> GetCities(List<int> id);
        public List<GoalMission> GetGoalMissionList();
    }
}
