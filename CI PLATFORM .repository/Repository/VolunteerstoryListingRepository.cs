using CI_PLATFORM.Entities.DataModels;
using CI_PLATFORM.Entities.ViewModels;
using CI_PLATFORM_.repository.Interface;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
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
    {
        private readonly CIPLATFORMDbContext _ciplatfromdbcontext;

        public VolunteerstoryListingRepository(CIPLATFORMDbContext ciplatfromdbcontext)
        {
            _ciplatfromdbcontext = ciplatfromdbcontext;
        }

        public StoryViewModel Getstorylist(int pageIndex)
        {
            int pageSize = 9;
            var stories = _ciplatfromdbcontext.Stories.Include(u => u.User).Include(t => t.Mission).ToList();
            var mission = _ciplatfromdbcontext.Missions.Include(m => m.Theme).ToList();

            StoryViewModel story = new StoryViewModel()
            {
                Missions = mission,
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
        public void addstory(long missionId, string title, DateTime date, string videoURL, string description, string[] imagePaths, long userid)
        {
            Story story = new Story
            {
                UserId = userid,
                Title = title,
                MissionId = missionId,
                Description = description,
                Status = "DRAFT",

            };
            _ciplatfromdbcontext.Stories.Add(story);
            _ciplatfromdbcontext.SaveChanges();
            var Story = _ciplatfromdbcontext.Stories.FirstOrDefault(u => u.UserId == userid && u.MissionId == missionId);
            var storyID = story.StoryId;
            foreach (var s in imagePaths)
            {
                StoryMedium media = new StoryMedium()
                {
                    StoryId = storyID,
                    Type = "IMAGE",
                    Path= s,
                };
                _ciplatfromdbcontext.StoryMedia.Add(media);
            }
            StoryMedium video = new StoryMedium()
            {
                StoryId = storyID,
                Type = "Video",
                Path = videoURL,
            };
            _ciplatfromdbcontext.StoryMedia.Add(video);
            _ciplatfromdbcontext.SaveChanges();
        }

        public AddStoryViewmodel getData(long userid)
        {
            var story = _ciplatfromdbcontext.Stories.FirstOrDefault(u => u.UserId == userid && u.Status == "DRAFT");
            if (story != null)
            {
                var storyMedia = _ciplatfromdbcontext.StoryMedia.Where(u => u.StoryId == story.StoryId);
                var images = storyMedia.Where(m => m.Type == "Image").Select(s => s.Path).ToList();
                var video = storyMedia.SingleOrDefault(m => m.Type == "video");
                var missionTitle = _ciplatfromdbcontext.Missions.SingleOrDefault(m => m.MissionId == story.MissionId);
                AddStoryViewmodel model = new AddStoryViewmodel()
                {
                    missionTitle = missionTitle.Title,
                    missionId = story.MissionId,
                    title = story.Title,
                    date = story.CreatedAt,
                    description = story.Description,
                    videoURL = video.Path,
                    imagepaths = images,
                };
                return model;
            }

            return new AddStoryViewmodel();
        }

        public List<Mission> GetMissions(long userid)
        {
            var missionapplication = _ciplatfromdbcontext.MissionApplications.Where(m => m.UserId == userid).Select(u => u.MissionId);

            return _ciplatfromdbcontext.Missions.Where(u => missionapplication.Contains(u.MissionId)).OrderBy(m => m.Title).ToList();
        }



    }
}
