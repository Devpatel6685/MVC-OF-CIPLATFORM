using CI_PLATFORM.Entities.DataModels;
using CI_PLATFORM.Entities.ViewModels;
using CI_PLATFORM_.repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM_.repository.Repository
{
    public class VolunteerRepository: IVolunteerInterface
    {
        private readonly CIPLATFORMDbContext _ciplatfromdbcontext;

        public VolunteerRepository(CIPLATFORMDbContext ciplatfromdbcontext)
        {
            _ciplatfromdbcontext = ciplatfromdbcontext;
        }
        public VolunteerTimesheetviewmodel GetAll(long userid)
        {
            var entity = _ciplatfromdbcontext.Timesheets.Include(m => m.Mission).Where(ts => ts.UserId == userid).AsQueryable();
            var time = entity.Where(ts => ts.Mission.MissionType == "time");
            var goal = entity.Where(ts => ts.Mission.MissionType == "goal");
            var goalMissions = _ciplatfromdbcontext.GoalMissions.ToList();
            VolunteerTimesheetviewmodel TS = new VolunteerTimesheetviewmodel();
            TS.TimeMission = time.ToList();
            TS.GoalMission = goal.ToList();
            TS.Goals = goalMissions;
            return TS;
        }

        public VolunteerTimesheetviewmodel getTimesheet(long timesheetid)
        {
            Timesheet ts = _ciplatfromdbcontext.Timesheets.SingleOrDefault(t => t.TimesheetId == timesheetid);
            VolunteerTimesheetviewmodel model = new VolunteerTimesheetviewmodel()
            {
                action = ts.Action,
                date = ts.DateVolunteered,
                hour = int.Parse(ts.Time?.ToString("hh")),
                minute = int.Parse(ts.Time?.ToString("mm")),
                message = ts.Notes
            };
            return model;
        }
        public void addTimesheet(VolunteerTimesheetviewmodel model, string userid)
        {
            var timesheet = _ciplatfromdbcontext.Timesheets.SingleOrDefault(ts => ts.TimesheetId.ToString() == model.timesheetid);
            if (timesheet == null)
            {
                Timesheet ts = new Timesheet()
                {
                    UserId = uint.Parse(userid),
                    MissionId = model.missionid,
                    Time = new TimeSpan(model.hour, model.minute, 0),
                    Action = model.action,
                    DateVolunteered = model.date,
                    Notes = model.message,
                    Status = "SUBMIT_FOR_APPROVAL"
                };
                _ciplatfromdbcontext.Timesheets.Add(ts);
            }
            else
            {
                timesheet.Time = new TimeSpan(model.hour, model.minute, 0);
                timesheet.Action = model.action;
                timesheet.DateVolunteered = model.date;
                timesheet.Notes = model.message;
            }
            _ciplatfromdbcontext.SaveChanges();
        }

        public void deleteTimesheet(int id)
        {
            var timesheet = _ciplatfromdbcontext.Timesheets.SingleOrDefault(ts => ts.TimesheetId == id);
            _ciplatfromdbcontext.Timesheets.Remove(timesheet);
            _ciplatfromdbcontext.SaveChanges();
        }
        public (List<Mission>, List<Mission>) getMissions(long userid)
        {
            var missionApplication = _ciplatfromdbcontext.MissionApplications.Where(u => u.UserId == userid).Select(u => u.MissionId);
            var time = _ciplatfromdbcontext.Missions.Where(u => missionApplication.Contains(u.MissionId) && u.MissionType == "time" && u.EndDate > DateTime.Now && u.Status == 1).OrderBy(m => m.Title).ToList();
            var goal = _ciplatfromdbcontext.Missions.Where(u => missionApplication.Contains(u.MissionId) && u.MissionType == "goal" && u.Status == 1).OrderBy(m => m.Title).ToList();
            return (time, goal);
        }
        public Tuple<int, int> getGoal(int id)
        {
            int goal = _ciplatfromdbcontext.GoalMissions.SingleOrDefault(m => m.MissionId == id).GoalValue;
            var timesheet = _ciplatfromdbcontext.Timesheets.Where(m => m.MissionId == id).ToList();
            int sum = 0;
            if (timesheet.Count() != 0)
            {
                sum = (int)timesheet.Select(s => s.Action).Sum();
            }
            return new Tuple<int, int>(goal, sum);
        }


    }
    
}

