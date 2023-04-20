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
        public void Addmission(MissionAddViewModel model);
        /*public Adminviewmodel getuserdata(int pageindex, int pageSize);
        public Adminviewmodel getmissiondata(int pageindex, int pageSize);
        public Adminviewmodel getapplication(int pageindex, int pageSize);

        public Adminviewmodel getcmspage(int pageindex, int pageSize);*/

    }
}
