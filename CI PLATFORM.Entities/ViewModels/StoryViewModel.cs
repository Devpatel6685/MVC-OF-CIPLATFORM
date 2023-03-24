using CI_PLATFORM.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM.Entities.ViewModels
{
    public class StoryViewModel
    {
        public List <Mission> Missions { get; set; }
        public List<Story> Stories { get; set; }
    }
}
