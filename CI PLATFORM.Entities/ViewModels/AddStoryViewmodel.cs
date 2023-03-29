using CI_PLATFORM.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM.Entities.ViewModels
{
    public class AddStoryViewmodel
    {
  
        public string missionTitle { get; set; }
       public long  missionId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime date { get; set; }

        public string status { get; set; }

        public string videoURL { get; set; }
         
        public long storyid { get; set; }
        public List<string> imagepaths { get; set; }
            
            

    
    }
}
