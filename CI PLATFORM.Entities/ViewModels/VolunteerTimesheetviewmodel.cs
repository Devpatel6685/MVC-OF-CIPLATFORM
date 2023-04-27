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
        public string timesheetid { get; set; }
        public List<Timesheet> TimeMission { get; set; } = new List<Timesheet>();
        public List<Timesheet> GoalMission { get; set; } = new List<Timesheet>();
        [Required(ErrorMessage = "Select mission")]
        public long missionid { get; set; }
        [Required(ErrorMessage = "Select Date")]
        public DateTime date { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Enter hour")]
        public int hour { get; set; } = new int();
        [Required(ErrorMessage = "Enter minutes")]
        [RegularExpression("^([0-5][0-9]|[0-9])$", ErrorMessage = "Please Enter proper minutes")]
        public int minute { get; set; } = new int();
        [Required(ErrorMessage = "Enter message")]
        public string message { get; set; }
        [Required(ErrorMessage = "enter action value")]
        public int? action { get; set; }
        public string missionTitle { get; set; }
    }
}
