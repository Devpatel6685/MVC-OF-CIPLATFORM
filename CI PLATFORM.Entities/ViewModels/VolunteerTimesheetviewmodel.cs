using CI_PLATFORM.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM.Entities.ViewModels
{
    public class VolunteerTimesheetviewmodel
    {
        public List<Timesheet> TimeMission { get; set; } = new List<Timesheet>();
        public List<Timesheet> GoalMission { get; set; } = new List<Timesheet>();
        [Required(ErrorMessage = "Select mission")]
        public long missionid { get; set; }
        [Required(ErrorMessage = "Select Date")]
        public DateTime date { get; set; } = DateTime.Now;
        public int hour { get; set; } = new int();
        public int minute { get; set; } = new int();
        [Required(ErrorMessage = "Enter message")]
        public string message { get; set; }
        public int action { get; set; } = new int();
        public string missionTitle { get; set; }
    }
}
