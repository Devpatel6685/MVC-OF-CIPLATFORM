using CI_PLATFORM.Entities.DataModels;
using CI_PLATFORM.Entities.ViewModels;
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


        public string FORGOTPASSWORD(forgotviewmodel user);

     public string RESETPAGE(resetviemodel password,string Token);
    }
}
