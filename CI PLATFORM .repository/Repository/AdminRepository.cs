using CI_PLATFORM.Entities.DataModels;
using CI_PLATFORM.Entities.ViewModels;
using CI_PLATFORM_.repository.Interface;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;
using Microsoft.EntityFrameworkCore;

namespace CI_PLATFORM_.repository.Repository
{
    public class AdminRepository : IAdminInterface
    {
        private readonly CIPLATFORMDbContext _ciplatfromdbcontext;
        private readonly IWebHostEnvironment _hostEnvironment;


        public AdminRepository(CIPLATFORMDbContext ciplatformcontext, IWebHostEnvironment hostEnvironment)
        {
            _ciplatfromdbcontext = ciplatformcontext;
            _hostEnvironment = hostEnvironment;
        }
        public Adminviewmodel getuserdata(int pageindex, int pageSize, string SearchInputdata)
        {

            var users = _ciplatfromdbcontext.Users.Where(u => (SearchInputdata == null) || (u.FirstName.Contains(SearchInputdata)) || (u.LastName.Contains(SearchInputdata))).ToList();
            var model = new Adminviewmodel();

            model.users = users.ToPagedList(pageindex, 10);
            return model;
        }
        public Adminviewmodel getthemedata(int pageindex, int pageSize, string SearchInputdata)
        {
            var themes = _ciplatfromdbcontext.MissionThemes.Where(t => (SearchInputdata == null) || (t.Title.Contains(SearchInputdata))).OrderByDescending(m => m.Status).ToList();
            var model = new Adminviewmodel();
            model.MissionThemes = themes.ToPagedList(pageindex, 2);
            return model;

        }
        public Adminviewmodel getskilldata(int pageindex, int pageSize, string SearchInputdata)
        {
            var skills = _ciplatfromdbcontext.Skills.Where(s => (SearchInputdata == null) || (s.SkillName.Contains(SearchInputdata))).OrderByDescending(s => s.Status).ToList();
            var model = new Adminviewmodel();
            model.Skills = skills.ToPagedList(pageindex, 2);
            return model;
        }
        public Adminviewmodel getstorydata(int pageindex, int pageSize, string SearchInputdata)
        {
            var stories = _ciplatfromdbcontext.Stories.Include(s => s.User).Include(s => s.Mission).Where(s => s.Status != "DRAFT" && ((SearchInputdata == null) || (s.Mission.Title.Contains(SearchInputdata)) || (s.User.FirstName.Contains(SearchInputdata)))).ToList();
            var model = new Adminviewmodel();
            model.Stories = stories.ToPagedList(pageindex, 1);
            return model;
        }
        public Adminviewmodel getmissiondata(int pageindex, int pageSize, string SearchInputdata)
        {

            var missions = _ciplatfromdbcontext.Missions.Where(m => (SearchInputdata == null) || (m.Title.Contains(SearchInputdata)) || (m.MissionType.Contains(SearchInputdata))).OrderByDescending(m => m.Status).ToList();

            var model = new Adminviewmodel();

            model.Missions = missions.ToPagedList(pageindex, 10);
            return model;
        }
        public Adminviewmodel getmissionapplicationdata(int pageindex, int pageSize, string SearchInputdata)
        {
            var missionapplication = _ciplatfromdbcontext.MissionApplications.Include(m => m.Mission).Include(m => m.User).Where(m => (SearchInputdata == null) || (m.Mission.Title.Contains(SearchInputdata)) || (m.User.FirstName.Contains(SearchInputdata))).ToList();
            var model = new Adminviewmodel();
            model.MissionApplications = missionapplication.ToPagedList(pageindex, 4);
            return model;
        }
        public void approveapplication(string applicationid)
        {
            var missionapplication = _ciplatfromdbcontext.MissionApplications.FirstOrDefault(m => m.MissionApplicationId.ToString() == applicationid);
            missionapplication.ApprovalStatus = "APPROVE";
            _ciplatfromdbcontext.SaveChanges();
        }
        public void approvestory(string storyid)
        {
            var story = _ciplatfromdbcontext.Stories.FirstOrDefault(s => s.StoryId.ToString() == storyid);
            story.Status = "PUBLISHED";
            _ciplatfromdbcontext.SaveChanges();
        }

        public void declineapplication(string applicationid)
        {
            var missionapplication = _ciplatfromdbcontext.MissionApplications.FirstOrDefault(m => m.MissionApplicationId.ToString() == applicationid);
            missionapplication.ApprovalStatus = "DECLINE";
            _ciplatfromdbcontext.SaveChanges();
        }
        public void declinestory(string storyid)
        {
            var story = _ciplatfromdbcontext.Stories.FirstOrDefault(s => s.StoryId.ToString() == storyid);
            story.Status = "DECLINED";
            _ciplatfromdbcontext.SaveChanges();

        }
        public List<Country> getcountries()
        {
            var countries = _ciplatfromdbcontext.Countries.ToList();
            return countries;
        }
        public List<City> getcities(string countryid)
        {
            var cities = _ciplatfromdbcontext.Cities.Where(c => c.CountryId.ToString() == countryid).ToList();
            return cities;
        }
        public List<MissionTheme> getthemes()
        {
            var themes = _ciplatfromdbcontext.MissionThemes.ToList();
            return themes;
        }
        public void Addtheme(ThemeAddViewModel model)
        {
            var model1 = new MissionTheme
            {
                Title = model.Title,
                Status = model.Status,
            };
            _ciplatfromdbcontext.Add(model1);
            _ciplatfromdbcontext.SaveChanges();
        }
        public void Addskill(SkillAddViewModel model)
        {
            var model1 = new Skill
            {
                SkillName = model.SkillName,
                Status = model.Status,
            };
            _ciplatfromdbcontext.Add(model1);
            _ciplatfromdbcontext.SaveChanges();
        }
        public void Addmission(MissionAddViewModel model)
        {
            var model1 = new Mission
            {
                Title = model.Title,
                CityId = model.CityId,
                CountryId = model.CountryId,
                OrganizationDetail = model.OrganizationDetail,
                OrganizationName = model.OrganizationName,
                Description = model.Description,
                ShortDescription = model.ShortDescription,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                TotalSeats = model.TotalSeats,
                Availibility = model.Availibility,
                ThemeId = model.ThemeId,
                Status = model.Status,
                MissionType = model.MissionType
            };

            _ciplatfromdbcontext.Add(model1);
            if (model.MissionType == "goal")
            {
                var model2 = new GoalMission
                {
                    GoalObjectiveText = model.GoalObjectiveText
                };
                model1.GoalMissions.Add(model2);
            }
            _ciplatfromdbcontext.SaveChanges();
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string imagesFolderPath = Path.Combine(wwwRootPath, "Images");
            string MainfolderPath = Path.Combine(imagesFolderPath, "Mission");
            if (!Directory.Exists(MainfolderPath))
            {
                Directory.CreateDirectory(MainfolderPath);
            }
            string folderName = model.Title;
            string folderPath = Path.Combine(MainfolderPath, folderName);
            var mission = _ciplatfromdbcontext.Missions.FirstOrDefault(m => m.MissionId == model1.MissionId);
            // Create a new directory if it doesn't exist
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            foreach (var Image in model.Images)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(folderPath, fileName + Path.GetExtension(Image.FileName));
                using (var fileStreams = new FileStream(uploads, FileMode.Create))
                {
                    Image.CopyTo(fileStreams);
                }
                var viewModel = new MissionMedium
                {
                    MissionId = mission.MissionId,
                    MediaName = fileName,
                    MediaType = "Imag",
                    MediaPath = @"\Images\Mission\" + folderName + @"\" + fileName + Path.GetExtension(Image.FileName),
                };
                _ciplatfromdbcontext.Add(viewModel);
                _ciplatfromdbcontext.SaveChanges();
            }
        }
        public void deletemission(string missionid)
        {
            var mission = _ciplatfromdbcontext.Missions.FirstOrDefault(m => m.MissionId.ToString() == missionid);
            mission.Status = 0;
            _ciplatfromdbcontext.SaveChanges();
        }
        public void deletetheme(string themeid)
        {
            var theme = _ciplatfromdbcontext.MissionThemes.FirstOrDefault(t => t.MissionThemeId.ToString() == themeid);
            theme.Status = 0;
            _ciplatfromdbcontext.SaveChanges();
        }
    }
}