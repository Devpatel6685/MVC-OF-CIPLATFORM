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
        public IActionResult storydetails(int id)
        {

            storydetailviewmodel Story =_volunterstory.GetStory(id);
            return View(Story);
        }
    }
}
