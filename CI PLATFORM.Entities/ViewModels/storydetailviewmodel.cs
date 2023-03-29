using CI_PLATFORM.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM.Entities.ViewModels
{
    public class storydetailviewmodel
    {
        public Story Story { get; set; }

        public long storyid { get; set; }

        public List<string> images { get; set; }
    }
}
