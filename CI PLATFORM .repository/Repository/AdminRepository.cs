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
        public Adminviewmodel getcmspagedata(int pageindex, int pageSize, string SearchInputdata)
        {
            var pages = _ciplatfromdbcontext.CmsPages.Where(c => (SearchInputdata == null) || (c.Title.Contains(SearchInputdata))).OrderByDescending(c => c.Status).ToList();
            var model = new Adminviewmodel();
            model.Cmspages = pages.ToPagedList(pageindex, 4);
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
        public Adminviewmodel getmissionapplicationdata(int pageindex, int pageSize, string SearchInputdata)
        {
            var missionapplication = _ciplatfromdbcontext.MissionApplications.Include(m => m.Mission).Include(m => m.User).Where(m => (SearchInputdata == null) || (m.Mission.Title.Contains(SearchInputdata)) || (m.User.FirstName.Contains(SearchInputdata))).ToList();
            var model = new Adminviewmodel();
            model.MissionApplications = missionapplication.ToPagedList(pageindex, 4);
            return model;
        }
        public MissionAddViewModel editmissondata(string missonid)
        {
            var mission = _ciplatfromdbcontext.Missions.FirstOrDefault(m => m.MissionId.ToString() == missonid);
            var goalmission = _ciplatfromdbcontext.GoalMissions.Include(g => g.Mission).FirstOrDefault(g => g.MissionId.ToString() == missonid);
            List<MissionMedium> missionMedia = _ciplatfromdbcontext.MissionMedia.Where(m => m.MissionId.ToString() == missonid).ToList();
            List<MissionDocument> missionDoc = _ciplatfromdbcontext.MissionDocuments.Where(m => m.MissionId.ToString() == missonid).ToList();
            List<IFormFile> imageFiles = new List<IFormFile>();
            List<IFormFile> docFiles = new List<IFormFile>();
            List<SelectListItem> list = new List<SelectListItem>();
            string wwwRootPath = _hostEnvironment.WebRootPath;
            var temp = _ciplatfromdbcontext.Countries.ToList();
            foreach (var item in temp)
            {
                list.Add(new SelectListItem() { Text = item.Name, Value = item.CountryId.ToString() });
            }
            List<SelectListItem> list1 = new List<SelectListItem>();
            var temp1 = _ciplatfromdbcontext.Cities.Where(c => c.CountryId == mission.CountryId).ToList();
            foreach (var item in temp1)
            {
                list1.Add(new SelectListItem() { Text = item.Name, Value = item.CityId.ToString() });
            }
            List<SelectListItem> list2 = new List<SelectListItem>();
            var temp2 = _ciplatfromdbcontext.MissionThemes.ToList();
            foreach (var item in temp2)
            {
                list2.Add(new SelectListItem() { Text = item.Title, Value = item.MissionThemeId.ToString() });
            }
            var skills = _ciplatfromdbcontext.Skills.ToList();
            var skillids_mission = _ciplatfromdbcontext.MissionSkills.Include(m => m.Skill).Where(m => m.MissionId == mission.MissionId).Select(m => m.SkillId).ToList();

            var model = new MissionAddViewModel
            {
                countries = list,
                cities = list1,
                Themes = list2,
                Title = mission.Title,
                CountryId = mission.CountryId,
                CityId = mission.CityId,
                ThemeId = mission.ThemeId,
                Description = mission.Description,
                ShortDescription = mission.ShortDescription,
                TotalSeats = mission.TotalSeats,
                StartDate = mission.StartDate,
                EndDate = mission.EndDate,
                Status = mission.Status,
                MissionType = mission.MissionType,
                OrganizationDetail = mission.OrganizationDetail,
                OrganizationName = mission.OrganizationName,
                Availibility = mission.Availibility,
                MissionId = mission.MissionId,
                skillids = skillids_mission,
                Skills = skills,
                RegistrationDeadline = mission.RegistrationDeadline,
            };
            foreach (var m in missionMedia)
            {
                string fullPath = wwwRootPath + m.MediaPath;
                using (var stream = new FileStream(fullPath, FileMode.Open))
                {
                    IFormFile file = new FormFile(stream, 0, new FileInfo(fullPath).Length, null, Path.GetFileName(fullPath));
                    imageFiles.Add(file);
                }
            }
            foreach (var m in missionDoc)
            {
                string fullPath = wwwRootPath + m.DocumentPath;
                using (var stream = new FileStream(fullPath, FileMode.Open))
                {
                    IFormFile file = new FormFile(stream, 0, new FileInfo(fullPath).Length, null, Path.GetFileName(fullPath));
                    docFiles.Add(file);
                }
            }
            model.Images = imageFiles;
            model.Documents = docFiles;
            if (model.MissionType == "goal")
            {
                model.GoalObjectiveText = goalmission.GoalObjectiveText;
                model.GoalValue = goalmission.GoalValue;
            };
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
                avtar = user.Avatar,
                UserId = user.UserId
            };
            return model;
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
        public void updateuser(UserAddViewModel model)
        {
            var user = _ciplatfromdbcontext.Users.FirstOrDefault(u => u.UserId == model.UserId);
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string imagesFolderPath = Path.Combine(wwwRootPath, "Images");
            string MainfolderPath = Path.Combine(imagesFolderPath, "UserProfileImages");

            String[] files = Directory.GetFiles(MainfolderPath);
            if (!Directory.Exists(MainfolderPath))
            {
                Directory.CreateDirectory(MainfolderPath);
            }
            if (model.Avatar == null)
            {
                var oldImagePath = user.Avatar;
                user.Avatar = oldImagePath;
            }
            else
            {
                string fileName_exist = model.Avatar.FileName;
                string fullPath = Path.Combine(MainfolderPath, fileName_exist);
                string uploads = Path.Combine(MainfolderPath, fileName_exist);
                if (!File.Exists(fullPath))
                {
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
                }
                else
                {
                    user.Avatar = @"\Images\UserProfileImages\" + fileName_exist;

                }
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Password = model.Password;
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
            var mission = _ciplatfromdbcontext.Missions.SingleOrDefault(m => m.MissionId == missionapplication.MissionId);
            mission.TotalSeats = mission.TotalSeats - 1;
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
            if (missionapplication.ApprovalStatus == "APPROVE")
            {
                var mission = _ciplatfromdbcontext.Missions.SingleOrDefault(m => m.MissionId == missionapplication.MissionId);
                mission.TotalSeats = mission.TotalSeats + 1;
            }
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
                Status = 0,
            };
            _ciplatfromdbcontext.Add(model1);
            _ciplatfromdbcontext.SaveChanges();
        }
        public MissionAddViewModel getmissionmodeldata()
        {
            var skills = _ciplatfromdbcontext.Skills.ToList();
            var model = new MissionAddViewModel
            {
                Skills = skills
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
            string fileName = model.Avatar.FileName;

            var uploads = Path.Combine(MainfolderPath, fileName);

            using (var fileStreams = new FileStream(uploads, FileMode.Create))
            {
                model.Avatar.CopyTo(fileStreams);
            }
            user.Avatar = @"\Images\UserProfileImages\" + fileName;
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
                Text = model.Text,
                Title = model.Title,
                Image = @"\Images\Banner\" + model.Image.FileName,
                SortOrder = model.SortOrder,
                Status = model.Status,
            };
            _ciplatfromdbcontext.Add(banner);
            _ciplatfromdbcontext.SaveChanges();
        }
        public void Editmission(MissionAddViewModel model, List<int> selectedSkills)
        {
            var mission = _ciplatfromdbcontext.Missions.SingleOrDefault(m => m.MissionId == model.MissionId);
            List<MissionMedium> missionMedia = _ciplatfromdbcontext.MissionMedia.Where(m => m.MissionId == model.MissionId).ToList();
            List<MissionDocument> missionDoc = _ciplatfromdbcontext.MissionDocuments.Where(m => m.MissionId == model.MissionId).ToList();
            var missionskills = _ciplatfromdbcontext.MissionSkills.Where(m => m.MissionId == model.MissionId);
            _ciplatfromdbcontext.MissionSkills.RemoveRange(missionskills);
            var goals = _ciplatfromdbcontext.GoalMissions.Where(g => g.MissionId == model.MissionId).ToList();
            if (model.MissionType == "goal")
            {
                _ciplatfromdbcontext.GoalMissions.RemoveRange(goals);
                var goalmodel = new GoalMission
                {
                    GoalObjectiveText = model.GoalObjectiveText,
                    GoalValue = model.GoalValue
                };
                mission.GoalMissions.Add(goalmodel);
            }
            string wwwRootPath = _hostEnvironment.WebRootPath;

            if (model.Title != mission.Title)
            {

                string imagesFolderPath = Path.Combine(wwwRootPath, "Images");
                string MainfolderPath = Path.Combine(imagesFolderPath, "Mission");
                string oldfolderpath = Path.Combine(MainfolderPath, mission.Title);
                string folderName = model.Title;
                string newfolderPath = Path.Combine(MainfolderPath, folderName);
                if (!Directory.Exists(newfolderPath))
                {
                    Directory.CreateDirectory(newfolderPath);
                }
                string[] imageFiles = Directory.GetFiles(oldfolderpath, "*.png|*.jpg");
                foreach (string imageFile in imageFiles)
                {
                    string fileName = Path.GetFileName(imageFile);
                    string destFile = Path.Combine(newfolderPath, fileName);
                    File.Copy(imageFile, destFile, true);
                }
                string documentFolderPath = Path.Combine(wwwRootPath, "Documents");
                string docmainfolderPath = Path.Combine(documentFolderPath, "Mission");
                string docoldfolderpath = Path.Combine(docmainfolderPath, mission.Title);
                string docnewfolderPath = Path.Combine(docmainfolderPath, folderName);
                if (!Directory.Exists(docnewfolderPath))
                {
                    Directory.CreateDirectory(docnewfolderPath);
                }
                string[] docFiles = Directory.GetFiles(docoldfolderpath, "*.doc|*.pdf|*.xlsx|*xls");
                foreach (string docFile in docFiles)
                {
                    string fileName = Path.GetFileName(docFile);
                    string destFile = Path.Combine(docnewfolderPath, fileName);
                    File.Copy(docFile, destFile, true);
                }



            }
            foreach (var skill in selectedSkills)
            {
                MissionSkill skillmodel = new MissionSkill
                {
                    SkillId = skill,
                };
                mission.MissionSkills.Add(skillmodel);
            }
            if (model.Images != null)
            {
                if (missionMedia.Count() != 0)
                {
                    _ciplatfromdbcontext.MissionMedia.RemoveRange(missionMedia);
                }

                string imagesFolderPath = Path.Combine(wwwRootPath, "Images");
                string MainfolderPath = Path.Combine(imagesFolderPath, "Mission");
                if (!Directory.Exists(MainfolderPath))
                {
                    Directory.CreateDirectory(MainfolderPath);
                }
                string folderName = model.Title;
                string folderPath = Path.Combine(MainfolderPath, folderName);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                foreach (var Image in model.Images)
                {
                    string fileName = Image.FileName;
                    var uploads = Path.Combine(folderPath, fileName);
                    using (var fileStreams = new FileStream(uploads, FileMode.Create))
                    {
                        Image.CopyTo(fileStreams);
                    }
                    var viewModel = new MissionMedium
                    {
                        MediaName = fileName,
                        MediaType = "Imag",
                        MediaPath = @"\Images\Mission\" + folderName + @"\" + fileName,
                    };
                    mission.MissionMedia.Add(viewModel);
                }
            }
            if (model.Documents != null)
            {
                if (missionDoc.Count() != 0)
                {
                    _ciplatfromdbcontext.MissionDocuments.RemoveRange(missionDoc);
                }

                string docFolderPath = Path.Combine(wwwRootPath, "Documents");
                string docMainfolderPath = Path.Combine(docFolderPath, "Mission");
                if (!Directory.Exists(docMainfolderPath))
                {
                    Directory.CreateDirectory(docMainfolderPath);
                }
                string folderName = model.Title;
                string docfolderPath = Path.Combine(docMainfolderPath, folderName);
                if (!Directory.Exists(docfolderPath))
                {
                    Directory.CreateDirectory(docfolderPath);
                }
                foreach (var doc in model.Documents)
                {
                    string fileName = doc.FileName;
                    var uploads = Path.Combine(docfolderPath, fileName + Path.GetExtension(doc.FileName));
                    using (var fileStreams = new FileStream(uploads, FileMode.Create))
                    {
                        doc.CopyTo(fileStreams);
                    }
                    MissionDocument docModel = new MissionDocument()
                    {
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
                    mission.MissionDocuments.Add(docModel);
                }
            }
            mission.Title = model.Title;
            mission.CityId = model.CityId;
            mission.CountryId = mission.CountryId;
            mission.OrganizationDetail = mission.OrganizationDetail;
            mission.OrganizationName = mission.OrganizationName;
            mission.ShortDescription = model.ShortDescription;
            mission.Description = model.Description;
            mission.StartDate = model.StartDate;
            mission.EndDate = model.EndDate;
            mission.TotalSeats = model.TotalSeats;
            mission.Availibility = model.Availibility;
            mission.ThemeId = model.ThemeId;
            mission.MissionType = model.MissionType;
            mission.Status = model.Status;
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
                MissionType = model.MissionType,
                RegistrationDeadline = model.RegistrationDeadline
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
                string fileName = Image.FileName;
                var uploads = Path.Combine(folderPath, fileName);
                using (var fileStreams = new FileStream(uploads, FileMode.Create))
                {
                    Image.CopyTo(fileStreams);
                }
                var viewModel = new MissionMedium
                {
                    MissionId = mission.MissionId,
                    MediaName = fileName,
                    MediaType = "Imag",
                    MediaPath = @"\Images\Mission\" + folderName + @"\" + fileName,
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
                string fileName = doc.FileName;
                var uploads = Path.Combine(missiondocfolderPath, fileName);
                using (var fileStreams = new FileStream(uploads, FileMode.Create))
                {
                    doc.CopyTo(fileStreams);
                }
                MissionDocument docModel = new MissionDocument()
                {
                    MissionId = mission.MissionId,
                    DocumentName = doc.FileName,
                    DocumentPath = @"\Documents\Mission\" + folderName + @"\" + fileName,
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
        public bool deletetheme(string themeid)
        {
            var theme = _ciplatfromdbcontext.MissionThemes.FirstOrDefault(t => t.MissionThemeId.ToString() == themeid);
            var MissionTheme = _ciplatfromdbcontext.Missions.Select(m => m.ThemeId).ToList();
            if (MissionTheme.Contains(int.Parse(themeid)))
            {
                return false;
            }
            else
            {
                theme.Status = 0;
                _ciplatfromdbcontext.SaveChanges();
                return true;
            }

        }
        public bool deleteskill(string skillid)
        {
            var skill = _ciplatfromdbcontext.Skills.FirstOrDefault(s => s.SkillId.ToString() == skillid);
            var MissionSkill = _ciplatfromdbcontext.MissionSkills.Select(mp => mp.SkillId).ToList();
            var UserSkill = _ciplatfromdbcontext.UserSkills.Select(us => us.SkillId).ToList();
            if (MissionSkill.Contains(int.Parse(skillid)) || UserSkill.Contains(int.Parse(skillid)))
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