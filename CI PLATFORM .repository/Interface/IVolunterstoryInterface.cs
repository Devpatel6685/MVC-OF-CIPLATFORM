using CI_PLATFORM.Entities.DataModels;
using CI_PLATFORM.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM_.repository.Interface
{
    public interface IVolunterstoryInterface
    {
        public StoryViewModel Getstorylist();

        public storydetailviewmodel GetStory(int id);

/*        public string Shareyourstory(AddStoryViewmodel story);
*/
      }
}
