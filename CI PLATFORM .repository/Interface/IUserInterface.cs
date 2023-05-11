using CI_PLATFORM.Entities.DataModels;
using CI_PLATFORM.Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM_.repository.Interface
{
    public interface IUserInterface
    {
        public string login(LoginViewModel user);

        public string REGISTRATIONPAGE(registerviewmodel user);
        public Tuple<List<NotificationTitle>, List<long>> gettitles(string userId);
        public List<Tuple<string, long, string, string, int, int>> getnotification(string userId);

        public void setstatus(string userid, List<string> titles);

        public string FORGOTPASSWORD(forgotviewmodel user);

        public string RESETPAGE(resetviemodel password,string Token);
        public List<Banner> GetBanners();

        public List<City> GetCities(int id);
        public EditUserViewModel getUserDetails(long userid);
        public void editUserDetails(EditUserViewModel model, long userid);
        public void addskill(List<int> skillids, string userid);
        public string changepass(EditUserViewModel model, string userid);
        public Contactusmodel addcontact(string userid);
        public void editcontact(string subject, string message, long userid);

        public String editimage(IFormFile Image, long userid);

    }
}
