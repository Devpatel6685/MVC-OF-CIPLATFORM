using CI_PLATFORM.Entities.DataModels;
using CI_PLATFORM.Entities.ViewModels;
using CI_PLATFORM_.repository.Interface;
using CI_PLATFORM_.repository.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Security.Cryptography;

namespace MVC_OF_CI_PLATFORM.Controllers
{
    
    public class MissionController : Controller
    {
       
        private readonly ISubheaderInterface _subheaderInterface;
        private readonly IMissionInterface _missionRepository;

       
        public MissionController(ISubheaderInterface subheaderInterface,IMissionInterface missionRepository )
        {
            _subheaderInterface = subheaderInterface;
            _missionRepository = missionRepository;
        }

        [AllowAnonymous]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult platformLanding(List<long> skillids, List<long> themeids, List<long> cityids, List<long> countryids, int sortId, string? SearchInputdata = "", int pageIndex = 1)
        {
            ViewData["country"] = _subheaderInterface.GetCountryList();

            ViewData["city"] = _subheaderInterface.GetCityList();
            ViewData["theme"] = _subheaderInterface.GetMissionThemeList();
            ViewData["skill"] = _subheaderInterface.GetSkillsList();
            ViewData["Mission"] = _missionRepository.GetMissionsList();
            ViewData["missionSkill"] = _subheaderInterface.GetMissionSkillsList();
            ViewData["GoalMission"] = _subheaderInterface.GetGoalMissionList();
            var firstname_session = HttpContext.Session.GetString("username");
            var userid = HttpContext.Session.GetString("userid");
            var mission = _missionRepository.GetAll(SearchInputdata, sortId, countryids, cityids, themeids, skillids, userid, pageIndex);
            mission.currentPage = pageIndex;
            return View(mission);
           
        }
        public IActionResult volunteerpage(long Id, int pageIndex = 1)
        {
            var userId = HttpContext.Session.GetString("userid");
            var mission = new Tuple<VolunteerMissionViewmodel, relatedmissionviewmodel>(_missionRepository.GetMissionId(Id, userId, pageIndex), _missionRepository.GetRelatedMission(Id));
            return View(mission);

        }

        public IActionResult favroitemission(string userId, long missionId)
        {
            var favorite = _missionRepository.favroite(userId, missionId);
            return View(volunteerpage);
        }

        public IActionResult favbtnlandingpage(string userId, long missionId)
        {
            var favorite = _missionRepository.favroite(userId, missionId);
            return RedirectToAction("platformLanding");
        }
        public IActionResult ratingupdate(long missionid, int rating, long userId)
        {
            var rate = _missionRepository.Rating(missionid, rating, userId);
            return RedirectToAction("volunteerpage", new { id = missionid });

        }
       
        public IActionResult comments(long missionid ,long userId ,string comment)
        {
            _missionRepository.comments(missionid, userId, comment);
            return RedirectToAction("volunteerpage", new { id = missionid });
            
        }
        public string recommend(List<long> userIds, long missionid)
        {
            var userId = HttpContext.Session.GetString("userid");
            var rec = _missionRepository.recommend(userIds, missionid, userId);
            return "successfully link sent";
        }
        public IActionResult applyMission(long missionid, long userId)
        {
            _missionRepository.apply(missionid, userId);
            return RedirectToAction("volunteerpage", new { id = missionid });

        }
      
        public JsonResult Country()
        {
            var country = _subheaderInterface.GetCountries();
            return Json(country);
        }
        public JsonResult Theme()
        {
            var theme = _subheaderInterface.GetMissionThemeList();
            return Json(theme);
        }
        public JsonResult Skill()
        {
            var skills = _subheaderInterface.GetSkillsList();
            return Json(skills);
        }

        public JsonResult User()
        {
            var rec = _missionRepository.GetUsers();
            return Json(rec); 
        }
        public JsonResult City(List<int> id)

        {
            var city = _subheaderInterface.GetCities(id);
            return Json(city);    
        }
       
      
        
    }
}
