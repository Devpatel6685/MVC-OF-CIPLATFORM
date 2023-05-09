using CI_PLATFORM.Entities.DataModels;
using CI_PLATFORM.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM_.repository.Interface
{
    public interface IAdminInterface
    {


        public Adminviewmodel getuserdata(int pageindex, int pageSize, string SearchInputdata);
        public Adminviewmodel getmissiondata(int pageindex, int pageSize, string SearchInputdata);
        public Adminviewmodel getbannerdata(int pageindex, string SearchInputdata);
        public void addBanner(BannerAddViewModel model);
        public void editBanner(BannerAddViewModel model);
        public BannerAddViewModel getBanner(string bannerid);
        public MissionAddViewModel editmissondata(string missonid);
        public void Editmission(MissionAddViewModel model, List<int> selectedSkills);
        public Adminviewmodel getcmspagedata(int pageindex, int pageSize, string SearchInputdata);
        public List<Country> getcountries();
        public List<City> getcities(string countryid);
        public List<MissionTheme> getthemes();
        public void deletemission(string missionid);
        public void Addmission(MissionAddViewModel model, List<int> selectedSkills);
        public SkillAddViewModel getskill(string skillid);
        public CmsAddViewModel getcmsdata(string cmspageid);
        public void Addcms(CmsAddViewModel model);
        public void deletestory(string storyid);
        public void editcmspage(CmsAddViewModel model);

        public bool Addskill(SkillAddViewModel model);
        public Adminviewmodel getthemedata(int pageindex, int pageSize, string SearchInputdata);
        public bool Addtheme(ThemeAddViewModel model);

        public void deleteuser(string userid);

        public bool deletetheme(string themeid);
        public Adminviewmodel getskilldata(int pageindex, int pageSize, string SearchInputdata);
        public Adminviewmodel getmissionapplicationdata(int pageindex, int pageSize, string SearchInputdata);
        public void approveapplication(string applicationid);
        public void declineapplication(string applicationid);
        public void editthemedatabase(ThemeAddViewModel model);
        public void pendingstory(string storyid);

        public ThemeAddViewModel gettheme(string themeid);

        public Adminviewmodel getstorydata(int pageindex, int pageSize, string SearchInputdata);
        public void approvestory(string storyid);
        public void DeleteBanner(long id);
        public void declinestory(string storyid);
        public MissionAddViewModel getmissionmodeldata();
        public void Adduser(UserAddViewModel model);
        public void updateuser(UserAddViewModel model);
        public void deletecmspage(string cmspageid);
        public UserAddViewModel edituserdata(string userid);
        public void editskilldatabase(SkillAddViewModel model);
        public bool deleteskill(string skillid);


    }
}
