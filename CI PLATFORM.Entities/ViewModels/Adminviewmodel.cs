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
    {   public IPagedList<User> user { get; set; }

        public IPagedList<Mission> missions { get; set; }

        public IPagedList<MissionApplication> missionApplications { get; set; }

        public IPagedList<CmsPage> cmsPages { get; set; }
    }
}
