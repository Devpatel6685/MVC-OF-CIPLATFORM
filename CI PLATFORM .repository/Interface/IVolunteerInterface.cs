using CI_PLATFORM.Entities.DataModels;
using CI_PLATFORM.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM_.repository.Interface
{
    public interface IVolunteerInterface
    {
        public (List<Mission>, List<Mission>) getMissions(long userid);
        public VolunteerTimesheetviewmodel getTimesheet(long timesheetid);
        public void addTimesheet(VolunteerTimesheetviewmodel model, string userid);
        public void deleteTimesheet(int id);
        public VolunteerTimesheetviewmodel GetAll(long userid);

    }
}
