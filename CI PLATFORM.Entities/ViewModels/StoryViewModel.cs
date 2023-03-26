using CI_PLATFORM.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace CI_PLATFORM.Entities.ViewModels
{
    public class StoryViewModel
    {
        public List <Mission> Missions { get; set; }
        public IPagedList<Story> Stories { get; set; }
    }
}
