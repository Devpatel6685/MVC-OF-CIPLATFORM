using CI_PLATFORM.Entities.DataModels;
using CI_PLATFORM.Entities.ViewModels;
using CI_PLATFORM_.repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MVC_OF_CI_PLATFORM.Controllers
{
    public class VolunteerController : Controller
    {
        private readonly IVolunteerInterface _Volunteer;

        public VolunteerController(IVolunteerInterface volunteer)
        {
            _Volunteer = volunteer;
        }

        public ActionResult VolunteeringTimesheet() {
			
			return View();
        }
     
        public JsonResult missions(string type)
        {
            long userid = long.Parse(HttpContext.Session.GetString("userid"));
            var data = _Volunteer.getMissions(userid);
            return Json(new { time = data.Item1, goal = data.Item2 });
        }
       
    }
}
