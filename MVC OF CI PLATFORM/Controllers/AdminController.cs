
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
        private readonly IAdminInterface  _adminInterface;

        public AdminController(ILogger<AdminController> logger,IAdminInterface adminInterface)
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
            return PartialView("_missionedit");
        }
        public IActionResult AddMission(MissionAddViewModel model)
        {
            _adminInterface.Addmission(model);
            return RedirectToAction("Mission", new { SearchInputdata = "", pageindex = 1, pageSize = 10 });
        }
        public IActionResult DeleteMission(string missionid)
        {
            _adminInterface.deletemission(missionid);
            return RedirectToAction("Mission", new { SearchInputdata = "", pageindex = 1, pageSize = 10 });

        }
      
    }
}
