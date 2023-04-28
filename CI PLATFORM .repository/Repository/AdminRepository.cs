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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System.Net;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace CI_PLATFORM_.repository.Repository
{
    public class AdminRepository : IAdminInterface
    {
        private readonly CIPLATFORMDbContext _ciplatfromdbcontext;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AdminRepository(CIPLATFORMDbContext ciplatfromdbcontext, IWebHostEnvironment hostEnvironment)
        {
            _ciplatfromdbcontext = ciplatfromdbcontext;
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
            var missionskill = _ciplatfromdbcontext.MissionSkills.ToList();
            var model = new Adminviewmodel();
            model.MissionSkills = missionskill;
            model.Skills = skills.ToPagedList(pageindex, 2);
            return model;
        }
        public SkillAddViewModel getskill(string skillid)
        {
            var skill = _ciplatfromdbcontext.Skills.FirstOrDefault(s => s.SkillId.ToString() == skillid);
            var model = new SkillAddViewModel
            {
                SkillId = skill.SkillId,
                SkillName = skill.SkillName,
                Status = skill.Status
            };
            return model;
        }
        public void editskilldatabase(SkillAddViewModel model)
        {
            var skill = _ciplatfromdbcontext.Skills.FirstOrDefault(s => s.SkillId == model.SkillId);
            skill.SkillName = model.SkillName;
            skill.Status = model.Status;
            _ciplatfromdbcontext.SaveChanges();
        }
        public Adminviewmodel getstorydata(int pageindex, int pageSize, string SearchInputdata)
        {
            var stories = _ciplatfromdbcontext.Stories.Include(s => s.User).Include(s => s.Mission).Where(s => s.Status != "DRAFT" && ((SearchInputdata == null) || (s.Mission.Title.Contains(SearchInputdata)) || (s.User.FirstName.Contains(SearchInputdata)))).ToList();
            var model = new Adminviewmodel();
            model.Stories = stories.ToPagedList(pageindex, 5);
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
        public UserAddViewModel edituserdata(string userid)
        {
            var user = _ciplatfromdbcontext.Users.FirstOrDefault(u => u.UserId.ToString() == userid);
            List<SelectListItem> list = new List<SelectListItem>();
            var temp = _ciplatfromdbcontext.Countries.ToList();
            foreach (var item in temp)
            {
                list.Add(new SelectListItem() { Text = item.Name, Value = item.CountryId.ToString() });
            }
            List<SelectListItem> list1 = new List<SelectListItem>();
            var temp1 = _ciplatfromdbcontext.Cities.Where(c => c.CountryId == user.CountryId).ToList();
            foreach (var item in temp1)
            {
                list1.Add(new SelectListItem() { Text = item.Name, Value = item.CityId.ToString() });
            }
            var model = new UserAddViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfileText = user.ProfileText,
                Password = user.Password,
                Department = user.Department,
                LinkedInUrl = user.LinkedInUrl,
                EmployeeId = user.EmployeeId,
                CityId = user.CityId,
                CountryId = user.CountryId,
                cities = list1,
                countries = list,
                Email = user.Email,
                Status = user.Status,
                avtar = user.Avatar
            };
            
            return model;

        }
        public Adminviewmodel getbannerdata(int pageindex, string SearchInputdata)
        {
            var banners = _ciplatfromdbcontext.Banners.Where(b => (SearchInputdata == null) || EF.Functions.Like(b.Text, $"%{SearchInputdata}%") || b.Title.Contains(SearchInputdata)).OrderByDescending(m => m.Status).ToList();
            var model = new Adminviewmodel();
            model.Banners = banners.ToPagedList(pageindex, 5);
            return model;
        }
        public BannerAddViewModel getBanner(string bannerid)
        {
            Banner banner = _ciplatfromdbcontext.Banners.SingleOrDefault(b => b.BannerId.ToString() == bannerid);
            string wwwRootPath = _hostEnvironment.WebRootPath;

            string fullPath = wwwRootPath + banner.Image;
            using (var stream = new FileStream(fullPath, FileMode.Open))
            {
                IFormFile file = new FormFile(stream, 0, new FileInfo(fullPath).Length, null, Path.GetFileName(fullPath));

                BannerAddViewModel model = new BannerAddViewModel()
                {
                    BannerId = banner.BannerId,
                    Text = banner.Text,
                    Title = banner.Title,
                    Image = file,
                    SortOrder = banner.SortOrder,
                    Status = banner.Status
                };
                return model;
            }
        }
        public void editBanner(BannerAddViewModel model)
        {
            Banner banner = _ciplatfromdbcontext.Banners.SingleOrDefault(b => b.BannerId == model.BannerId);
            if (model.Image != null)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string imageFolderPath = Path.Combine(wwwRootPath, "Images");
                string mainFolderPath = Path.Combine(imageFolderPath, "Banner");
                String[] files = Directory.GetFiles(mainFolderPath);
                if (!Directory.Exists(mainFolderPath))
                {
                    Directory.CreateDirectory(mainFolderPath);
                }
                string fullPath = Path.Combine(mainFolderPath, model.Image.FileName);
                if (!File.Exists(fullPath))
                {
                    using (var fileStreams = new FileStream(Path.Combine(mainFolderPath, model.Image.FileName), FileMode.Create))
                    {
                        model.Image.CopyTo(fileStreams);
                    }
                }
                banner.Image = @"\Images\Banner\" + model.Image.FileName;
            }
            banner.SortOrder = model.SortOrder;
            banner.Text = WebUtility.HtmlDecode(model.Text);
            banner.Title = model.Title;
            banner.Status = model.Status;
           _ciplatfromdbcontext.Update(banner);
           _ciplatfromdbcontext.SaveChanges();
        }
        public void addBanner(BannerAddViewModel model)
        {

            string wwwRootPath = _hostEnvironment.WebRootPath;
            string imageFolderPath = Path.Combine(wwwRootPath, "Images");
            string mainFolderPath = Path.Combine(imageFolderPath, "Banner");
            if (!Directory.Exists(mainFolderPath))
            {
                Directory.CreateDirectory(mainFolderPath);
            }
            string fullPath = Path.Combine(mainFolderPath, model.Image.FileName);
            if (!File.Exists(fullPath))
            {
                using (var fileStreams = new FileStream(Path.Combine(mainFolderPath, model.Image.FileName), FileMode.Create))
                {
                    model.Image.CopyTo(fileStreams);
                }
            }
            Banner banner = new Banner
            {
                Text = WebUtility.HtmlDecode(model.Text),
                Title = model.Title,
                Image = @"\Images\Banner\" + model.Image.FileName,
                SortOrder = model.SortOrder,
                Status = model.Status,
            };
            _ciplatfromdbcontext.Add(banner);
            _ciplatfromdbcontext.SaveChanges();
        }
        public void updateuser(UserAddViewModel model)
        {
            var user =_ciplatfromdbcontext.Users.FirstOrDefault(u => u.UserId == model.UserId);
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string imagesFolderPath = Path.Combine(wwwRootPath, "Images");
            string MainfolderPath = Path.Combine(imagesFolderPath, "UserProfileImages");

            String[] files = Directory.GetFiles(MainfolderPath);
            if (!Directory.Exists(MainfolderPath))
            {
                Directory.CreateDirectory(MainfolderPath);
            }
            string fileName_exist = model.Avatar.FileName;
            string fullPath = Path.Combine(MainfolderPath, fileName_exist);
            string uploads = Path.Combine(MainfolderPath, fileName_exist);
            if (!File.Exists(fullPath))
            {
                // string fileName = Guid.NewGuid().ToString();
                string fileName = fileName_exist;
                string filePath = Path.Combine(MainfolderPath, fileName);
                using (var inputStream = model.Avatar.OpenReadStream())
                {
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        inputStream.CopyTo(fileStream);
                    }
                }
                user.Avatar = @"\Images\UserProfileImages\" + fileName;
               _ciplatfromdbcontext.SaveChanges();
            }
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Password= model.Password;
            user.LinkedInUrl = model.LinkedInUrl;
            user.EmployeeId = model.EmployeeId;
            user.Department = model.Department;
            user.Email = model.Email;
            user.CountryId = model.CountryId;
            user.CityId = model.CityId;
            user.ProfileText = model.ProfileText;
            user.Status = model.Status;
            user.Title = model.Title;
            _ciplatfromdbcontext.SaveChanges();
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
        public MissionAddViewModel getmissionmodeldata()
        {
            var skills = _ciplatfromdbcontext.Skills.ToList();
            var model = new MissionAddViewModel
            {
                Skills = skills,
            };
            return model;
        }

        public void Adduser(UserAddViewModel model)
        {
            var model1 = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                EmployeeId = model.EmployeeId,
                Department = model.Department,
                CityId = model.CityId,
                CountryId = model.CountryId,
                ProfileText = model.ProfileText,
                Status = model.Status
            };
            _ciplatfromdbcontext.Add(model1);
            _ciplatfromdbcontext.SaveChanges();
            var user = _ciplatfromdbcontext.Users.FirstOrDefault(u => u.UserId == model1.UserId);
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string imagesFolderPath = Path.Combine(wwwRootPath, "Images");
            string MainfolderPath = Path.Combine(imagesFolderPath, "UserProfileImages");
            if (!Directory.Exists(MainfolderPath))
            {
                Directory.CreateDirectory(MainfolderPath);
            }
            string fileName = Guid.NewGuid().ToString();

            var uploads = Path.Combine(MainfolderPath, fileName + Path.GetExtension(model.Avatar.FileName));

            using (var fileStreams = new FileStream(uploads, FileMode.Create))
            {
                model.Avatar.CopyTo(fileStreams);
            }
            user.Avatar = @"\Images\UserProfileImages\" + fileName + Path.GetExtension(model.Avatar.FileName);
            _ciplatfromdbcontext.SaveChanges();

        }
        public void Addmission(MissionAddViewModel model, List<int> selectedSkills)
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
            foreach (var skill in selectedSkills)
            {
                var model3 = new MissionSkill
                {
                    SkillId = skill,
                };
                model1.MissionSkills.Add(model3);
            }
            if (model.MissionType == "goal")
            {
                var model2 = new GoalMission
                {
                    GoalObjectiveText = model.GoalObjectiveText,
                    GoalValue = model.GoalValue,

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

            string documentFolderPath = Path.Combine(wwwRootPath, "Documents");
            string missiondocPath = Path.Combine(documentFolderPath, "Mission");
            string missiondocfolderPath = Path.Combine(missiondocPath, folderName);
            if (!Directory.Exists(missiondocfolderPath))
            {
                Directory.CreateDirectory(missiondocfolderPath);
            }
            foreach (var doc in model.Documents)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(folderPath, fileName + Path.GetExtension(doc.FileName));
                using (var fileStreams = new FileStream(uploads, FileMode.Create))
                {
                    doc.CopyTo(fileStreams);
                }
                MissionDocument docModel = new MissionDocument()
                {
                    MissionId = mission.MissionId,
                    DocumentName = doc.FileName,
                    DocumentPath = @"\Documents\Mission\" + folderName + @"\" + fileName + Path.GetExtension(doc.FileName),
                };

                switch (Path.GetExtension(doc.FileName))
                {
                    case ".doc":
                    case ".docx":
                        docModel.DocumentType = "DOCX";
                        break;
                    case ".xls":
                    case ".xlsx":
                        docModel.DocumentType = "XLSX";
                        break;
                    case ".pdf":
                        docModel.DocumentType = "PDF";
                        break;
                    default:
                        // Handle other types of documents here
                        break;
                }
                _ciplatfromdbcontext.MissionDocuments.Add(docModel);
            }
            _ciplatfromdbcontext.SaveChanges();

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
        public bool deleteskill(string skillid)
        {
            var skill = _ciplatfromdbcontext.Skills.FirstOrDefault(s => s.SkillId.ToString() == skillid);
            var MissionSkill = _ciplatfromdbcontext.MissionSkills.Select(mp => mp.SkillId).ToList();
            var UserSkill = _ciplatfromdbcontext.UserSkills.Select(us => us.SkillId).ToList();
            if (MissionSkill.Contains(int.Parse(skillid)) && UserSkill.Contains(int.Parse(skillid)))
            {
                return false;
            }
            else
            {
                skill.Status = 0;
                _ciplatfromdbcontext.SaveChanges();
                return true;
            }

        }
    }
}