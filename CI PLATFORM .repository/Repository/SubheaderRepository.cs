using CI_PLATFORM.Entities.DataModels;
using CI_PLATFORM.Entities.ViewModels;
using CI_PLATFORM_.repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM_.repository.Repository
{
    public class SubHeaderRepository : ISubheaderInterface
    { private readonly CIPLATFORMDbContext _cIPLATFORMDbContext;
     public  SubHeaderRepository(CIPLATFORMDbContext cIPLATFORMDbContext)
        {
            _cIPLATFORMDbContext= cIPLATFORMDbContext;
        }
        

       

        public List<MissionTheme> GetMissionThemeList()
        {
            return _cIPLATFORMDbContext.MissionThemes.ToList();
        }

        public List<Skill> GetSkillsList()
        {
            return _cIPLATFORMDbContext.Skills.ToList();
        }
        public List<Country> GetCountries()
        {
            var Country = _cIPLATFORMDbContext.Countries.ToList();
            return Country;
        }
        public List<City> GetCities(List<int> id)
        {
            var cities = new List<City>();
            foreach (var cityid in id)
            {
                List<City> city = _cIPLATFORMDbContext.Cities.Where(m => m.CountryId == cityid).ToList();
                foreach (var c in city)
                {
                    cities.Add(c);
                }
            }
            return cities;
        }
        public List<MissionSkill> GetMissionSkillsList()
        {
            return _cIPLATFORMDbContext.MissionSkills.ToList();
        }

        public List<Country> GetCountryList()
        {
            return _cIPLATFORMDbContext.Countries.ToList();
        }

        public List<City> GetCityList()
        {
            return _cIPLATFORMDbContext.Cities.ToList();
        }

        public List<GoalMission> GetGoalMissionList()
        {
            return _cIPLATFORMDbContext.GoalMissions.ToList();
        }
    }
}