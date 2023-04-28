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
        public List<Country> getcountries();
        public List<City> getcities(string countryid);
        public List<MissionTheme> getthemes();
        public void deletemission(string missionid);
        public void Addmission(MissionAddViewModel model, List<int> selectedSkills);
        public SkillAddViewModel getskill(string skillid);

        public void Addskill(SkillAddViewModel model);
        public Adminviewmodel getthemedata(int pageindex, int pageSize, string SearchInputdata);
        public void Addtheme(ThemeAddViewModel model);
        public void deletetheme(string themeid);
        public Adminviewmodel getskilldata(int pageindex, int pageSize, string SearchInputdata);
        public Adminviewmodel getmissionapplicationdata(int pageindex, int pageSize, string SearchInputdata);
        public void approveapplication(string applicationid);
        public void declineapplication(string applicationid);
        public Adminviewmodel getstorydata(int pageindex, int pageSize, string SearchInputdata);
        public void approvestory(string storyid);
        public void declinestory(string storyid);
        public MissionAddViewModel getmissionmodeldata();
        public void Adduser(UserAddViewModel model);

        public void updateuser(UserAddViewModel model);

        public UserAddViewModel edituserdata(string userid);

        public void editskilldatabase(SkillAddViewModel model);
        public bool deleteskill(string skillid);


    }
}
