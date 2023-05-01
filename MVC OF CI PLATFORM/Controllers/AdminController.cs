
using CI_PLATFORM.Entities.ViewModels;
using CI_PLATFORM_.repository.Interface;
using CI_PLATFORM_.repository.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MVC_OF_CI_PLATFORM.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IAdminInterface _adminInterface;

        public AdminController(ILogger<AdminController> logger, IAdminInterface adminInterface)
        {
            _logger = logger;
            _adminInterface = adminInterface;
        }
        public IActionResult UserpageInAdmin(string SearchInputdata = "", int pageindex = 1, int pageSize = 10)
        {
            var model = _adminInterface.getuserdata(pageindex, pageSize, SearchInputdata);
            return View(model);
        }

        public IActionResult User(string SearchInputdata = "", int pageindex = 1, int pageSize = 10)
        {
            var model = _adminInterface.getuserdata(pageindex, pageSize, SearchInputdata);
            return PartialView("_userpage", model);
        }

        public IActionResult Mission(string SearchInputdata = "", int pageindex = 1, int pageSize = 10)
        {
            var model = _adminInterface.getmissiondata(pageindex, pageSize, SearchInputdata);
            return PartialView("_missionpage", model);
        }
        public IActionResult Theme(string SearchInputdata = "", int pageindex = 1, int pageSize = 2)
        {
            var model = _adminInterface.getthemedata(pageindex, pageSize, SearchInputdata);
            return PartialView("_themepage", model);
        }
        public IActionResult Skill(string SearchInputdata = "", int pageindex = 1, int pageSize = 2)
        {
            var model = _adminInterface.getskilldata(pageindex, pageSize, SearchInputdata);
            return PartialView("_skillpage", model);
        }
        public IActionResult Story(string SearchInputdata = "", int pageindex = 1, int pageSize = 1)
        {
            var model = _adminInterface.getstorydata(pageindex, pageSize, SearchInputdata);
            return PartialView("_storypage", model);
        }
        public IActionResult MissionApplication(string SearchInputdata = "", int pageindex = 1, int pageSize = 4)
        {
            var model = _adminInterface.getmissionapplicationdata(pageindex, pageSize, SearchInputdata);
            return PartialView("_missionapplicationpage", model);
        }
        public IActionResult Banner(string SearchInputdata = "", int pageindex = 1)
        {
            var model = _adminInterface.getbannerdata(pageindex, SearchInputdata);
            return PartialView("_bannerpage", model);
        }
        public IActionResult ApproveApplication(string Applicationid)
        {
            _adminInterface.approveapplication(Applicationid);
            return RedirectToAction("MissionApplication", new { SearchInputdata = "", pageindex = 1, pageSize = 4 });
        }
        public IActionResult ApproveStory(string storyid)
        {
            _adminInterface.approvestory(storyid);
            return RedirectToAction("Story", new { SearchInputdata = "", pageindex = 1, pageSize = 1 });

        }
        public IActionResult DeclineStory(string storyid)
        {
            _adminInterface.declinestory(storyid);
            return RedirectToAction("Story", new { SearchInputdata = "", pageindex = 1, pageSize = 1 });

        }
        public IActionResult DeclineApplication(string Applicationid)
        {
            _adminInterface.declineapplication(Applicationid);
            return RedirectToAction("MissionApplication", new { SearchInputdata = "", pageindex = 1, pageSize = 4 });
        }
        public JsonResult Country()
        {
            var countries = _adminInterface.getcountries();
            return Json(countries);
        }
        public JsonResult City(string countryid)
        {
            var cities = _adminInterface.getcities(countryid);
            return Json(cities);
        }
        public JsonResult GetThemes()
        {
            var themes = _adminInterface.getthemes();
            return Json(themes);
        }
        public IActionResult missionadd()
        {
            var model = _adminInterface.getmissionmodeldata();
            return PartialView("_missionedit", model);
        }
        public IActionResult useradd()
        {
            return PartialView("_useradd");
        }
        public IActionResult themeadd()
        {
            return PartialView("_themeadd");
        }
        public IActionResult skilladd()
        {
            return PartialView("_skilladd");
        }
        public IActionResult editskilldata(string skillid)
        {
            var model = _adminInterface.getskill(skillid);
            return PartialView("_skilladd", model);
        }
        public IActionResult banneradd()
        {
            return PartialView("_banneradd");
        }
        public IActionResult AddMission(MissionAddViewModel model, List<int> selectedSkills)
        {
            if (model.MissionId == 0)
            {
                _adminInterface.Addmission(model, selectedSkills);
                TempData["success"] = "Mission is added successfully";
            }
            else
            {
                _adminInterface.Editmission(model, selectedSkills);
                TempData["success"] = "Mission is edited successfully";
            }

            return RedirectToAction("Mission", new { SearchInputdata = "", pageindex = 1, pageSize = 10 });
        }
        public IActionResult AddBanner(BannerAddViewModel model)
        {
            if (model.BannerId == 0)
            {
                _adminInterface.addBanner(model);
                TempData["success"] = "Banner added successfully";
            }
            else
            {
                _adminInterface.editBanner(model);
                TempData["success"] = "Banner edited successfully";
            }
            return RedirectToAction("Banner");
        }
        public IActionResult AddUser(UserAddViewModel model)
        {
            if (model.UserId == 0)
            {
                _adminInterface.Adduser(model);
                TempData["success"] = "User is added succesfully";

                return RedirectToAction("User", new { SearchInputdata = "", pageindex = 1, pageSize = 10 });
            }
            else
            {
                _adminInterface.updateuser(model);
                TempData["success"] = "User is updated succesfully";
                return RedirectToAction("User", new { SearchInputdata = "", pageindex = 1, pageSize = 10 });
            }

        }
        public IActionResult AddTheme(ThemeAddViewModel model)
        {
            _adminInterface.Addtheme(model);
            return RedirectToAction("Theme", new { SearchInputdata = "", pageindex = 1, pageSize = 2 });
        }
        public IActionResult AddSkill(SkillAddViewModel model)
        {
            if (model.SkillId == 0)
            {
                _adminInterface.Addskill(model);
                TempData["success"] = "skill is added";
            }
            else
            {
                _adminInterface.editskilldatabase(model);
                TempData["success"] = "skill is updated";
            }
            return RedirectToAction("Skill", new { SearchInputdata = "", pageindex = 1, pageSize = 2 });

        }
        public IActionResult EditBanner(string id)
        {
            var model = _adminInterface.getBanner(id)
;
            return PartialView("_banneradd", model);
        }
        public IActionResult edituser(string id)
        {
            var usermodel = _adminInterface.edituserdata(id);
            return PartialView("_useradd", usermodel);
        }
        public IActionResult editmission(string id)
        {
            var model = _adminInterface.editmissondata(id);
            return PartialView("_missionedit", model);
        }
        public IActionResult DeleteMission(string missionid)
        {
            _adminInterface.deletemission(missionid);
            return RedirectToAction("Mission", new { SearchInputdata = "", pageindex = 1, pageSize = 10 });

        }
        public IActionResult DeleteTheme(string themeid)
        {
            _adminInterface.deletetheme(themeid);
            return RedirectToAction("Theme", new { SearchInputdata = "", pageindex = 1, pageSize = 2 });
        }
        public bool DeleteSkill(string skillId)
        {
            var delete = _adminInterface.deleteskill(skillId);
            return delete;
        }
    }
}

