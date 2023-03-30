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
    }
}
