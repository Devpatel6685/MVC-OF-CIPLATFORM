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
        public IPagedList<MissionTheme> MissionThemes { get; set; }
        public IPagedList<Skill> Skills { get; set; }
        public IPagedList<MissionApplication> MissionApplications { get; set; }
        public IPagedList<Story> Stories { get; set; }
        public IPagedList<Banner> Banners { get; set; }
        public List<MissionSkill> MissionSkills { get; set; }
        public IPagedList<CmsPage> Cmspages { get; set; }
    }
}
