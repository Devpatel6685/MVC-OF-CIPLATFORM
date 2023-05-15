
using CI_PLATFORM.Entities.ViewModels;
using CI_PLATFORM_.repository.Interface;
using CI_PLATFORM_.repository.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data;

namespace MVC_OF_CI_PLATFORM.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IAdminInterface _adminInterface;

        public IActionResult UserpageInAdmin(string SearchInputdata = "", int pageindex = 1, int pageSize = 10)
        {
            var model = _adminInterface.getuserdata(pageindex, pageSize, SearchInputdata);
            return View(model);
        }
        public AdminController(ILogger<AdminController> logger, IAdminInterface adminRepository)
        {
            _logger = logger;
            _adminInterface= adminRepository;
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
        public IActionResult Story(string SearchInputdata = "", int pageindex = 1, int pageSize = 10)
        {
            var model = _adminInterface.getstorydata(pageindex, pageSize, SearchInputdata);
            return PartialView("_storypage", model);
        }
        public IActionResult Cmspage(string SearchInputdata = "", int pageindex = 1, int pageSize = 4)
        {
            var model = _adminInterface.getcmspagedata(pageindex, pageSize, SearchInputdata);
            return PartialView("_cmspage", model);
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
            var userid = HttpContext.Session.GetString("userid");
            _adminInterface.approveapplication(Applicationid, userid);
            return RedirectToAction("MissionApplication", new { SearchInputdata = "", pageindex = 1, pageSize = 4 });
        }
        public IActionResult ApproveStory(string storyid)
        {
            var userid = HttpContext.Session.GetString("userid");
           _adminInterface.approvestory(storyid, userid);
            return RedirectToAction("Story", new { SearchInputdata = "", pageindex = 1, pageSize = 1 });

        }
        public IActionResult DeclineStory(string storyid)
        {
            var userid = HttpContext.Session.GetString("userid");
            _adminInterface.declinestory(storyid, userid);
            return RedirectToAction("Story", new { SearchInputdata = "", pageindex = 1, pageSize = 1 });

        }
        public IActionResult DeclineApplication(string Applicationid)
        {
            var userid = HttpContext.Session.GetString("userid");
           _adminInterface.declineapplication(Applicationid, userid);
            return RedirectToAction("MissionApplication", new { SearchInputdata = "", pageindex = 1, pageSize = 4 });
        }
        public IActionResult pendingStory(string storyid)
        {
            _adminInterface.pendingstory(storyid);
            return RedirectToAction("Story", new { SearchInputdata = "", pageindex = 1, pageSize = 1 });

        }
     /*   public IActionResult DeclineApplication(string Applicationid, string userid)
        {
            _adminInterface.declineapplication(Applicationid, userid);
            return RedirectToAction("MissionApplication", new { SearchInputdata = "", pageindex = 1, pageSize = 4 });
        }*/
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
        public IActionResult cmsadd()
        {
            return PartialView("_cmsadd");
        }
        public IActionResult editskilldata(string skillid)
        {
            var model = _adminInterface.getskill(skillid);
            return PartialView("_skilladd", model);
        }
        public IActionResult editthemedata(string themeid)
        {
            var model = _adminInterface.gettheme(themeid);
            return PartialView("_themeadd", model);
        }
        public IActionResult editcmsdata(string cmsid)
        {
            var model = _adminInterface.getcmsdata(cmsid);
            return PartialView("_cmsadd", model);
        }
        public IActionResult banneradd()
        {
            return PartialView("_banneradd");
        }
        public IActionResult AddMission(MissionAddViewModel model, List<int> selectedSkills)
        {
            var userid = HttpContext.Session.GetString("userid");
            if (model.MissionId == 0)
            {
                _adminInterface.Addmission(model, selectedSkills,userid);
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
            if (model.themeid == 0)
            {
                var check = _adminInterface.Addtheme(model);
                if (check)
                {
                    TempData["success"] = "Theme is added";
                }
                else
                {
                    ModelState.AddModelError("Title", "Theme already exist");
                    return PartialView("_themeadd");
                }
            }
            else
            {
                _adminInterface.editthemedatabase(model);
                TempData["success"] = "theme is updated";
            }
            return RedirectToAction("Theme", new { SearchInputdata = "", pageindex = 1, pageSize = 2 });

        }
        public IActionResult AddSkill(SkillAddViewModel model)
        {
            if (model.SkillId == 0)
            {
                var check = _adminInterface.Addskill(model);
                if (check)
                {
                    TempData["success"] = "skill is added";
                }
                else
                {
                    ModelState.AddModelError("SkillName", "Skillname already exist");
                    return PartialView("_skilladd");
                }
            }
            else
            {
                _adminInterface.editskilldatabase(model);
                TempData["success"] = "skill is updated";
            }
            return RedirectToAction("Skill", new { SearchInputdata = "", pageindex = 1, pageSize = 2 });

        }
        public IActionResult AddCms(CmsAddViewModel model)
        {
            if (model.CmsPageId == 0)
            {
                _adminInterface.Addcms(model);
                TempData["success"] = "CMS page is added";

            }
            else
            {
                _adminInterface.editcmspage(model);
                TempData["success"] = "CMS page is edited";

            }
            return RedirectToAction("Cmspage", new { SearchInputdata = "", pageindex = 1 });
        }
        public IActionResult EditBanner(string id)
        {
            var model = _adminInterface.getBanner(id)
;
            return PartialView("_banneradd", model);
        }
        public IActionResult DeleteBanner(int id)
        {
            _adminInterface.DeleteBanner(id);
            return RedirectToAction("Banner", new { SearchInputdata = "", pageindex = 1 });
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
        public IActionResult Deleteuser(string userid)
        {
            _adminInterface.deleteuser(userid);
            return RedirectToAction("User", new { SearchInputdata = "", pageindex = 1, pageSize = 10 });

        }
        public IActionResult DeleteCMSPage(string cmspageId)
        {
            _adminInterface.deletecmspage(cmspageId);
            return RedirectToAction("Cmspage", new { SearchInputdata = "", pageindex = 1 });
        }
        public bool DeleteTheme(string themeid)
        {
            var delete = _adminInterface.deletetheme(themeid);
            return delete;
        }
        public bool DeleteSkill(string skillId)
        {
            var delete = _adminInterface.deleteskill(skillId);
            return delete;
        }
        public IActionResult DeleteStory(string storyid)
        {
            _adminInterface.deletestory(storyid);
            return RedirectToAction("Story", new { SearchInputdata = "", pageindex = 1, pageSize = 1 });

        }
        public IActionResult Timesheet(int pageIndex = 1, string keyword = "")
        {
            var data = _adminInterface.GetTimesheets(pageIndex, keyword);
            return PartialView("_TimesheetPage", data);
        }

        [HttpPost]
        public string EditTimesheet(int id, int status)
        {
            _adminInterface.EditTimesheet(id, status);
            if (status == 0)
            {
                TempData["admin"] = "Request Rejected";
            }
            else
            {
                TempData["admin"] = "Request Accepted";
            }
            return "success";
        }

        [HttpPost]
        public IActionResult DeleteTimesheet(long id)
        {
            _adminInterface.DeleteTimesheet(id);
            return RedirectToAction("Timesheet");
        }

    }
}

