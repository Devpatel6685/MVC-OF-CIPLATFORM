using CI_PLATFORM.Entities.DataModels;
using CI_PLATFORM.Entities.ViewModels;
using CI_PLATFORM_.repository.Interface;
using JetBrains.Annotations;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MimeKit;
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
            var images = _ciplatfromdbcontext.StoryMedia.Where(m=>m.Type =="Image").Select(m=>m.Path).ToList();
            model.Story = story;
            model.images= images;
            return model;

        }
        public void Shareyourstory(long missionId, string title, DateTime date, string videoURL, string description, string[] imagePaths, long userid)
        {
            Story storymodel = new Story()
            {
                UserId = userid,
                MissionId = missionId,
                Title = title,
                Description = description,
                Status = "DRAFT",
            };
            var entity = _ciplatfromdbcontext.Stories.FirstOrDefault(m => m.UserId == userid && m.MissionId == missionId);
            if (entity == null)
            {
                _ciplatfromdbcontext.Stories.Add(storymodel);
            }
            else
            {
                entity.UserId = userid;
                entity.MissionId = missionId;
                entity.Title = title;
                entity.Description = description;
                entity.Status = "DRAFT";
            }
            _ciplatfromdbcontext.SaveChanges();
            var story = _ciplatfromdbcontext.Stories.FirstOrDefault(u => u.UserId == userid && u.MissionId == missionId);
            var storyId = story.StoryId;
            if (entity != null)
            {
                var mediaentity = _ciplatfromdbcontext.StoryMedia.Where(u => u.StoryId == storyId);
                _ciplatfromdbcontext.StoryMedia.RemoveRange(mediaentity);
            }

            foreach (var s in imagePaths)
            {
                StoryMedium storyMedia = new StoryMedium()
                {
                    StoryId = storyId,
                    Type = "Image",
                    Path = s,
                };
                _ciplatfromdbcontext.StoryMedia.Add(storyMedia);
            }
            StoryMedium storyvideo = new StoryMedium()
            {
                StoryId = storyId,
                Type = "Video",
                Path = videoURL,
            };
            _ciplatfromdbcontext.StoryMedia.Add(storyvideo);
            _ciplatfromdbcontext.SaveChanges();
        }
        public AddStoryViewmodel getData(long userid, string missionid)
        {
            if (missionid == null)
            {
                return new AddStoryViewmodel();
            }
            var story = _ciplatfromdbcontext.Stories.FirstOrDefault(u => u.UserId == userid && u.MissionId.ToString() == missionid);
            if (story != null)
            {
                var storyMedia = _ciplatfromdbcontext.StoryMedia.Where(u => u.StoryId == story.StoryId);
                var images = storyMedia.Where(m => m.Type == "Image").Select(s => s.Path).ToList();
                var video = storyMedia.FirstOrDefault(m => m.Type == "video");
                var missionTitle = _ciplatfromdbcontext.Missions.FirstOrDefault(m => m.MissionId == story.MissionId);
                AddStoryViewmodel model = new AddStoryViewmodel()
                {  status = story.Status,
                    storyid = story.StoryId,
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
            var missionTitle1 = _ciplatfromdbcontext.Missions.SingleOrDefault(m => m.MissionId == long.Parse(missionid));
            AddStoryViewmodel model1 = new AddStoryViewmodel()
            {   missionId = long.Parse(missionid),
                missionTitle = missionTitle1.Title
            };
            return model1;
        }
        public void submit(long storyId)
        {
            var story = _ciplatfromdbcontext.Stories.SingleOrDefault(m => m.StoryId == storyId);
            story.Status = "PUBLISHED";
            _ciplatfromdbcontext.Update(story);
            _ciplatfromdbcontext.SaveChanges();
        }
        public void editStory(long missionId, string title, DateTime date, string videoURL, string description, string[] imagePaths, long userid)
        {
            var entity = _ciplatfromdbcontext.Stories.SingleOrDefault(m => m.UserId == userid && m.MissionId == missionId);
            entity.UserId = userid;
            entity.MissionId = missionId;
            entity.Title = title;
            entity.Description = description;
            entity.Status = "PUBLISHED";
            var mediaentity =_ciplatfromdbcontext.StoryMedia.Where(u => u.StoryId == entity.StoryId);
            _ciplatfromdbcontext.StoryMedia.RemoveRange(mediaentity);
            foreach (var s in imagePaths)
            {
                StoryMedium storyMedia = new StoryMedium()
                {
                    StoryId = entity.StoryId,
                    Type = "Image",
                    Path = s,
                };
                _ciplatfromdbcontext.StoryMedia.Add(storyMedia);
            }
            if (videoURL != null)
            {
                StoryMedium storyvideo = new StoryMedium()
                {
                    StoryId = entity.StoryId,
                    Type = "Video",
                    Path = videoURL,
                };
                _ciplatfromdbcontext.StoryMedia.Add(storyvideo);
            }
           _ciplatfromdbcontext.SaveChanges();
        }

        public List<Mission> GetMissions(long userid)
        {
            var missionapplication = _ciplatfromdbcontext.MissionApplications.Where(m => m.UserId == userid).Select(u => u.MissionId);

            return _ciplatfromdbcontext.Missions.Where(u => missionapplication.Contains(u.MissionId)).OrderBy(m => m.Title).ToList();
        }

        public List<User> GetUsers()
        {
            return _ciplatfromdbcontext.Users.ToList();
        }
        public string recommend(List<long> userids, long storyId, string fromuserId)
        {

            var mailBody = "<h1>Click this link to view mission</h1><br><h2><a href='https://localhost:7093/VolunterrStory/storydetails/?id=" + storyId + "' >View story</h2>";
            var users = _ciplatfromdbcontext.Users.Where(u => userids.Contains(u.UserId));
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("devppatel6685@gmail.com"));

            var name = _ciplatfromdbcontext.Users.SingleOrDefault(m => m.UserId == Convert.ToInt64(fromuserId));

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("devppatel6685@gmail.com", "hpygmvoqamjvzkrl");

            foreach (var user in users)
            {
                StoryInvite model = new StoryInvite();
                model.StoryId = storyId;
                model.FromUserId = Convert.ToInt64(fromuserId);
                model.ToUserId = user.UserId;
                email.To.Add(MailboxAddress.Parse(user.Email));
                email.Subject = name.FirstName + " " + name.LastName + " has recommended story to you";
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = mailBody };
                smtp.Send(email)
;
                _ciplatfromdbcontext.StoryInvites.Add(model);
            }
            _ciplatfromdbcontext.SaveChanges();
            smtp.Disconnect(true);
            return "success";
        }

    }
}
