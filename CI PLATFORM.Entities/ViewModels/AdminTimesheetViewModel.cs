using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM.Entities.ViewModels
{
    public class AdminTimesheetViewModel
    {
        public long timesheetId { get; set; }
        public long? userId { get; set; }
        public String userName { get; set; }
        public string missionName { get; set; }
        public string type { get; set; }
        public DateTime dateVolunteered { get; set; }
        public int? action { get; set; }
        public TimeSpan? time { get; set; }
        public string status { get; set; }
    }
}
