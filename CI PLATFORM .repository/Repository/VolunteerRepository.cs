using CI_PLATFORM.Entities.DataModels;
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
            var missionApplication = _ciplatfromdbcontext.MissionApplications.Where(u => u.UserId == userid).Select(u => u.MissionId);
            var time = _ciplatfromdbcontext.Missions.Where(u => missionApplication.Contains(u.MissionId) && u.MissionType == "TIME").OrderBy(m => m.Title).ToList();
            var goal = _ciplatfromdbcontext.Missions.Where(u => missionApplication.Contains(u.MissionId) && u.MissionType == "GOAL").OrderBy(m => m.Title).ToList();
            return (time, goal);
        }
    }
    
}

