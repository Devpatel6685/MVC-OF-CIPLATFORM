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
        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult VolunteeringTimesheet()
        {
            var userid = HttpContext.Session.GetString("userid");
            if (userid == null)
            {
                return RedirectToAction("Login", "home");
            }
            var entity = _Volunteer.GetAll(long.Parse(userid));
            return View(entity);
        }

        [HttpGet]
        public VolunteerTimesheetviewmodel GetTimesheet(long timesheetid)
        {
            var model = _Volunteer.getTimesheet(timesheetid);
            return model;
        }

        [HttpPost]
        public IActionResult addTimesheet(VolunteerTimesheetviewmodel model)
        {
            var userid = HttpContext.Session.GetString("userid");
            _Volunteer.addTimesheet(model, userid);
            if (model.timesheetid == null)
            {
                TempData["timesheet"] = "Timesheet Added Successfully";
            }
            else
            {
                TempData["timesheet"] = "Timesheet Edited Successfully";
            }
            return RedirectToAction("VolunteeringTimesheet");
        }

        [HttpPost]
        public IActionResult deleteTimesheet(int id)
        {
            _Volunteer.deleteTimesheet(id);
            return RedirectToAction("VolunteeringTimesheet");
        }
        public JsonResult missions(string type)
        {
            long userid = long.Parse(HttpContext.Session.GetString("userid"));
            var data = _Volunteer.getMissions(userid);
            return Json(new { time = data.Item1, goal = data.Item2 });
        }

        public JsonResult getGoal(int id)
        {
            var data = _Volunteer.getGoal(id);
            return Json(new { goal = data.Item1, sum = data.Item2 });
        }

    }
}
