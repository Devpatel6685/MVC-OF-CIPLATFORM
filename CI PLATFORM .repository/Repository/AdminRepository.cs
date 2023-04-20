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
        public Adminviewmodel getmissiondata(int pageindex, int pageSize, string SearchInputdata)
        {
            /*            var missions = _ciplatformcontext.Missions.Where(m=> (SearchInputdata==null)|| (m.Title.Contains(SearchInputdata))||(m.MissionType.Contains(SearchInputdata))).ToList();
            */
            var missions = _ciplatfromdbcontext.Missions.Where(m => (SearchInputdata == null) || (m.Title.Contains(SearchInputdata)) || (m.MissionType.Contains(SearchInputdata))).OrderByDescending(m => m.Status).ToList();

            var model = new Adminviewmodel();

            model.Missions = missions.ToPagedList(pageindex, 10);
            return model;
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
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                TotalSeats = model.TotalSeats,
                Availibility = model.Availibility,
                ThemeId = model.ThemeId,
                Status = model.Status,
                MissionType = model.MissionType
            };

            _ciplatfromdbcontext.Add(model1);
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
            var image = 2;
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
    }
}

      /*  public Adminviewmodel getuserdata(int pageindex, int pageSize)
        {
            var user = _ciplatfromdbcontext.Users.ToList();
            var model = new Adminviewmodel
            {
                *//*user = user,*//*
            };
            model.user = user.ToPagedList(pageindex, 10);
            return model;
        }
        public Adminviewmodel getmissiondata(int pageindex, int pageSize)
        {
            var misssion = _ciplatfromdbcontext.Missions.ToList();
            var model = new Adminviewmodel
            {

            };
            model.missions = misssion.ToPagedList(pageindex, 10);

            return model;
        }



        public Adminviewmodel getapplication(int pageindex, int pageSize)
        {
            var misssionapplication = _ciplatfromdbcontext.MissionApplications.ToList();
            var model = new Adminviewmodel
            {
                
            };
            model.missionApplications = misssionapplication.ToPagedList(pageindex, 10);
            return model;
        }
        public Adminviewmodel getcmspage(int pageindex, int pageSize)
        {
            var cms = _ciplatfromdbcontext.CmsPages.ToList();
            var model = new Adminviewmodel
            {
                
            };
            model.cmsPages = cms.ToPagedList(pageindex, 10);
            return model;
        }*/
