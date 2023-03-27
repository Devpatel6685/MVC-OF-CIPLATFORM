using CI_PLATFORM.Entities.DataModels;
using CI_PLATFORM.Entities.ViewModels;
using CI_PLATFORM_.repository.Interface;
using CI_PLATFORM_.repository.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MVC_OF_CI_PLATFORM.Controllers
{
    public class VolunterrStoryController : Controller
    {
        private readonly IVolunterstoryInterface _volunterstory;

        public VolunterrStoryController(IVolunterstoryInterface volunterstory)
        {
            _volunterstory = volunterstory;       
        }

        public IActionResult volunteerstory(int pageIndex = 1)
        {    
            var stories = _volunterstory.Getstorylist(pageIndex);
            return View(stories);
        }

        public IActionResult Shareyourstory()
        {

            var stories = _volunterstory.Getstorylist(1);
            return View(stories);
        }
        public IActionResult addstory()
        {
            long userid = long.Parse(HttpContext.Session.GetString("userid"));
            if(userid == null){
                return RedirectToAction("LOGIN","Home");
            }
            var data = _volunterstory.getData(userid);
            return View(data);
        }
        [HttpPost]
        public IActionResult addstory(long missionId, string title, DateTime date, string videoURL, string description, string[] imagePaths)
        {
            long userid = long.Parse(HttpContext.Session.GetString("userid"));
             _volunterstory.addstory(missionId,title,date,videoURL,description,imagePaths,userid);
            return View();
                
        }

      

        public IActionResult storydetails(int id)
        {

            storydetailviewmodel Story =_volunterstory.GetStory(id);
            return View(Story);
        }
     
        public JsonResult missions()
        {
            long userid = long.Parse(HttpContext.Session.GetString("userid"));
            List<Mission> missions = _volunterstory.GetMissions(userid);
            return Json(missions);
        }
    }
}
