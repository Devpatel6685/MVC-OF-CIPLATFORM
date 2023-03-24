using CI_PLATFORM.Entities.DataModels;
using CI_PLATFORM.Entities.ViewModels;
using CI_PLATFORM_.repository.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM_.repository.Interface
{
    
    public interface IMissionInterface
    {
        public List<Mission> GetMissionsList();
        public MissionViewmodel GetAll(string keyword, int sortId, List<long> countryids, List<long> cityids, List<long> themeids, List<long> skillids,string userid, int pageIndex);

        public VolunteerMissionViewmodel GetMissionId(long Id ,string userId, int pageIndex);

        public relatedmissionviewmodel GetRelatedMission(long Id);

        public string recommend(List<long> userids, long misssionid, string fromuserId);
        public List<User> GetUsers();

        /*public string comments(long missionid, long userId);*/
        public void comments(long missionid, long userId, string comment);
        public void apply(long missionid, long userId);
        public string Rating(long missionid, int rating, long userId);
        public string favroite (string userId, long missionid);
        public string recomand(List<long> userids);

    }
}
