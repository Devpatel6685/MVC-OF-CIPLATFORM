﻿using CI_PLATFORM.Entities.DataModels;
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
        public (List<Mission>, List<Mission>) getMissions(long userid)
        {
            var timesheet =_ciplatfromdbcontext.Timesheets.Where(u => u.UserId == userid).Select(ts => ts.MissionId).ToList();
            var missionApplication = _ciplatfromdbcontext.MissionApplications.Where(u => u.UserId == userid).Select(u => u.MissionId);
            var time = _ciplatfromdbcontext.Missions.Where(u => missionApplication.Contains(u.MissionId) && u.MissionType == "TIME").OrderBy(m => m.Title).ToList();
            var goal = _ciplatfromdbcontext.Missions.Where(u => missionApplication.Contains(u.MissionId) && u.MissionType == "GOAL").OrderBy(m => m.Title).ToList();
            return (time, goal);
        }
        /*        public void addtimesheet(VolunteerTimesheetviewmodel model, string userid)
                {
                    Timesheet ts = new Timesheet()
                    {
                       UserId =uint.Parse(userid),
                        MissionId=uint.Parse(userid),
                        Time = new TimeSpan(model.hour,model.minute,0),
                        Action = model.action,
                        DateVolunteered = model.date,
                        Notes = model.message,
                        Status = "SUBMIT_FOR_APPROVAL"

                    };
                    _ciplatfromdbcontext.Timesheets.Add(ts);
                    _ciplatfromdbcontext.SaveChanges();
                }
        */
        public VolunteerTimesheetviewmodel GetAll(long userid)
        {
            var entity = _ciplatfromdbcontext.Timesheets.Include(m => m.Mission).Where(ts => ts.UserId == userid).AsQueryable();
            var time = entity.Where(ts => ts.Mission.MissionType == "TIME");
            var goal = entity.Where(ts => ts.Mission.MissionType == "GOAL");
           VolunteerTimesheetviewmodel TS = new VolunteerTimesheetviewmodel();
            TS.TimeMission = time.ToList();
            TS.GoalMission = goal.ToList(); 
            return TS;
        }

        public void addtimesheet(VolunteerTimesheetviewmodel model, string userid)
        {
            var timesheet = _ciplatfromdbcontext.Timesheets.SingleOrDefault(ts => ts.UserId.ToString() == userid && ts.MissionId == model.missionid);
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

      
    }
    
}
