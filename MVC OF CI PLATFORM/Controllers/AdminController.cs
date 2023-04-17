
using CI_PLATFORM_.repository.Interface;
using CI_PLATFORM_.repository.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MVC_OF_CI_PLATFORM.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminInterface  _adminInterface;

        public AdminController(IAdminInterface adminInterface)
        {
            _adminInterface = adminInterface;
        }

        public IActionResult UserpageInAdmin(int pageindex = 1, int pageSize = 10)
        {
           var model = _adminInterface.getuserdata(pageindex, pageSize);
            return View(model);
        }
        public IActionResult User(int pageindex = 1, int pageSize = 10)
        {
            var model = _adminInterface.getuserdata(pageindex, pageSize);

            return PartialView("_userpage", model);
        }
        public IActionResult Mission(int pageindex = 1, int pageSize = 10)
        {
            var missions = _adminInterface.getmissiondata(pageindex, pageSize);

            return PartialView("_missionpage",missions);
        }
        public IActionResult MissionApplication(int pageindex = 1, int pageSize = 10)
        {
            var application = _adminInterface.getapplication(pageindex, pageSize);

            return PartialView("_application",application);
        }
        public IActionResult cmspage(int pageindex = 1, int pageSize = 10)
        {
            var application = _adminInterface.getcmspage(pageindex, pageSize);

            return PartialView("_cmspage", application);
        }
    }
}
