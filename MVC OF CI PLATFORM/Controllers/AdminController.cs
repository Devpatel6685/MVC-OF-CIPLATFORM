using Microsoft.AspNetCore.Mvc;

namespace MVC_OF_CI_PLATFORM.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult UserpageInAdmin()
        {
            return View();
        }
        public IActionResult User()
        {
            return PartialView("_userpage");
        }
        public IActionResult Mission()
        {
            return PartialView("_missionpage");
        }
    }
}
