using CI_PLATFORM.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace CI_PLATFORM.Entities.ViewModels
{
    public class Adminviewmodel
    {
        public IPagedList<User> users { get; set; }
        public IPagedList<Mission> Missions { get; set; }

        /* public IPagedList<MissionApplication> missionApplications { get; set; }

         public IPagedList<CmsPage> cmsPages { get; set; }*/
    }
}
