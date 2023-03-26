using CI_PLATFORM.Entities.DataModels;
using CI_PLATFORM.Entities.ViewModels;
using CI_PLATFORM_.repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace CI_PLATFORM_.repository.Repository
{
    public class VolunteerstoryListingRepository : IVolunterstoryInterface
    { private readonly CIPLATFORMDbContext _ciplatfromdbcontext;

        public VolunteerstoryListingRepository(CIPLATFORMDbContext ciplatfromdbcontext)
        {
            _ciplatfromdbcontext= ciplatfromdbcontext;
        }

        public StoryViewModel Getstorylist(int pageIndex)
        {
            int pageSize = 9;
            var stories = _ciplatfromdbcontext.Stories.Include(u => u.User).Include(t => t.Mission).ToList();
            var mission = _ciplatfromdbcontext.Missions.Include(m=>m.Theme).ToList();
            
            StoryViewModel story = new StoryViewModel()
            {
               Missions= mission,
               Stories = stories.ToPagedList(pageIndex, pageSize),
            };
            return story;

        }
        public storydetailviewmodel GetStory(int storyid)
        {
            storydetailviewmodel model = new storydetailviewmodel();
            Story story = _ciplatfromdbcontext.Stories.Include(m => m.User).Include(m => m.Mission).SingleOrDefault(s => s.StoryId == storyid);
            model.Story = story;
            return model;



        }

    }
}
