using CI_PLATFORM.Entities.DataModels;
using CI_PLATFORM.Entities.ViewModels;
using CI_PLATFORM_.repository.Interface;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace CI_PLATFORM_.repository.Repository
{
    public class MissionRepository : IMissionInterface
    {
        private readonly CIPLATFORMDbContext _cIPLATFORMDbContext;


        public MissionRepository(CIPLATFORMDbContext cIPLATFORMDbContext)
        {
            _cIPLATFORMDbContext = cIPLATFORMDbContext;
        }

        public MissionViewmodel GetAll(string keyword, int sortId, List<long> countryids, List<long> cityids, List<long> themeids, List<long> skillids, string userid, int pageIndex)
        {
            int pagesize = 9;
            var mission = _cIPLATFORMDbContext.Missions.Include(m => m.City).Include(m => m.Theme).Where(model => (keyword == null || model.Title.Contains(keyword) || model.Theme.Title.Contains(keyword) || model.City.Name.Contains(keyword)) && ((countryids.Contains(model.CountryId)) || countryids.Count() == 0) && ((cityids.Contains(model.CityId)) || cityids.Count() == 0) && ((themeids.Contains(model.ThemeId)) || themeids.Count() == 0)).AsQueryable();
            var rates = _cIPLATFORMDbContext.MissionRatings.Include(m => m.User).Include(m => m.Mission).ToList();
            var addfavorite = _cIPLATFORMDbContext.FavouriteMissions.ToList();
            var addfavrouitebyuserid = _cIPLATFORMDbContext.FavouriteMissions.Where(u => u.UserId.ToString() == userid).ToList();



            MissionViewmodel listOfMission = new MissionViewmodel()
            {
                totalrecords = mission.Count(),
                rate = rates,
                favorite = addfavorite

            };
            //SORT BY
            var sortmission = mission.ToList();
            if (sortId == 1)
            {
                sortmission = sortmission.OrderByDescending(p => p.CreatedAt).ToList();
            }
            else if (sortId == 2)
            {
                sortmission = sortmission.OrderBy(p => p.CreatedAt).ToList();
            }
            else if (sortId == 3)
            {
                sortmission = sortmission.OrderBy(p => p.Availability).ToList();
            }
            else if (sortId == 4)
            {
                sortmission = sortmission.OrderByDescending(p => p.Availability).ToList();
            }
            else if (sortId == 5)
            {
                sortmission = sortmission.OrderByDescending(p => addfavrouitebyuserid.Any(f => f.MissionId == p.MissionId)).ToList();
            }
            listOfMission.Missions = sortmission.ToPagedList(pageIndex, pagesize);
            return listOfMission;
        }
        public VolunteerMissionViewmodel GetMissionId(long Id, string userId, int pageIndex)
        {
            List<string> skills = _cIPLATFORMDbContext.MissionSkills
                        .Where(m => m.MissionId == Id)
                        .Include(m => m.Mission)
                        .Include(m => m.Skill)
                        .Select(m => m.Skill.SkillName)
                        .ToList();

            Mission mission = _cIPLATFORMDbContext.Missions.Include(m => m.City).Include(x => x.Theme).SingleOrDefault(m => m.MissionId == Id);
            var rate = _cIPLATFORMDbContext.MissionRatings.Include(m => m.User).ToList();
            var mission_skills = _cIPLATFORMDbContext.MissionSkills.Include(m => m.Skill).ToList();
            var addfavorite = _cIPLATFORMDbContext.FavouriteMissions.SingleOrDefault(m => m.MissionId == Id && m.UserId.ToString() == userId) == null ? false : true;
            var applied = _cIPLATFORMDbContext.MissionApplications.SingleOrDefault(m => m.MissionId == Id && m.UserId.ToString() == userId) == null ? false : true;
            var comments = _cIPLATFORMDbContext.Comments.Include(m => m.User).Where(m => m.MissionId == mission.MissionId).OrderByDescending(c => c.CreatedAt).ToList();
            var missionapplication = _cIPLATFORMDbContext.MissionApplications.Include(m => m.User).Where(m => m.MissionId == mission.MissionId && m.UserId.ToString() != userId).ToList();
            var missiondocuments = _cIPLATFORMDbContext.MissionDocuments.Where(m => m.MissionId == mission.MissionId).ToList();

            VolunteerMissionViewmodel volunteerMission = new VolunteerMissionViewmodel()

            {
                MissionId = mission.MissionId,
                Theme = mission.Theme.Title,
                City = mission.City.Name,
                CountryId = mission.CountryId,
                Title = mission.Title,
                ShortDescription = mission.ShortDescription,
                Description = mission.Description,
                StartDate = mission.StartDate,
                EndDate = mission.EndDate,
                MissionType = mission.MissionType,
                Status = mission.Status,
                OrganizationName = mission.OrganizationName,
                OrganizationDetail = mission.OrganizationDetail,
                Availability = mission.Availability,
                MissionSkills = mission_skills,
                goalvalue = 0,
                goalObjective = "",
                favorite = addfavorite,
                comments = comments,
                rate = rate,
                applied = applied,
                MissionApplications = missionapplication.ToPagedList(pageIndex, 9),
                Documents = missiondocuments,


            };
            if (mission.MissionType == "GOAL")
            {
                var goal = _cIPLATFORMDbContext.GoalMissions.SingleOrDefault(m => m.MissionId == Id);
                volunteerMission.goalObjective = goal.GoalObjectiveText;
                volunteerMission.goalvalue = goal.GoalValue;
            }
            return volunteerMission;
        }
        public string Rating(long missionid, int rating, long userId)
        {
            var mission_rating = _cIPLATFORMDbContext.MissionRatings.Include(m => m.Mission).Include(m => m.User).ToList();
            var rate1update = mission_rating.SingleOrDefault(m => m.User.UserId == userId && m.MissionId == missionid);

            if (rate1update != null)
            {
                rate1update.Rating = rating;
                _cIPLATFORMDbContext.Update(rate1update);
                _cIPLATFORMDbContext.SaveChanges();
            }
            else
            {
                var missionRating = new MissionRating
                {
                    MissionId = missionid,
                    UserId = userId,
                    Rating = rating,
                };
                _cIPLATFORMDbContext.MissionRatings.Add(missionRating);
                _cIPLATFORMDbContext.SaveChanges();
            }
            return "successfull";
        }

        public void apply(long missionid, long userId)
        {
            var appliedmission = _cIPLATFORMDbContext.MissionApplications.Where(x => x.MissionId == missionid && x.UserId == userId).SingleOrDefault();
            MissionApplication application = new MissionApplication();
            application.UserId = userId;
            application.MissionId = missionid;
            application.ApprovalStatus = "APPROVE";
            application.AppliedAt = DateTime.Now;
            _cIPLATFORMDbContext.MissionApplications.Add(application)
;
            _cIPLATFORMDbContext.SaveChanges();
        }
        public string favroite(string userId, long missionid)
        {
            var Favroite1 = _cIPLATFORMDbContext.FavouriteMissions.SingleOrDefault(m => m.UserId.ToString() == userId && m.MissionId == missionid);
            if (Favroite1 == null)
            {
                FavouriteMission favroite = new FavouriteMission
                {
                    MissionId = missionid,
                    UserId = Convert.ToInt64(userId),
                };
                _cIPLATFORMDbContext.FavouriteMissions.Add(favroite);
                _cIPLATFORMDbContext.SaveChanges();
            }
            else
            {
                _cIPLATFORMDbContext.FavouriteMissions.Remove(Favroite1);
                _cIPLATFORMDbContext.SaveChanges();
            }

            return "Favourite";
        }
        public List<Mission> GetMissionsList()
        {

            return _cIPLATFORMDbContext.Missions.ToList();
        }
        public void comments(long missionid, long userId, string comment)
        {
            Comment cmt = new Comment
            {
                MissionId = missionid,
                UserId = userId,
                CmtDescription = comment,
                ApprovalStatus = "PUBLISHED",

            };
            _cIPLATFORMDbContext.Comments.Add(cmt);
            _cIPLATFORMDbContext.SaveChanges();


        }

        public relatedmissionviewmodel GetRelatedMission(long Id)
        {
            var missions = _cIPLATFORMDbContext.Missions.Include(m => m.City).Include(m => m.Theme).Include(m => m.Country).Where(ms => ms.MissionId == Id).FirstOrDefault();
            var RelatedMission = _cIPLATFORMDbContext.Missions.Include(m => m.City).Include(m => m.Theme).Include(m => m.Country).Where(ms => ms.MissionId != missions.MissionId && (ms.City.Name == missions.City.Name || ms.Theme.Title == missions.Theme.Title || ms.Country.Name == missions.Country.Name)).ToList();
            var goalmission = _cIPLATFORMDbContext.GoalMissions.ToList();
            var addfavorite = _cIPLATFORMDbContext.FavouriteMissions.ToList();
            var rates = _cIPLATFORMDbContext.MissionRatings.Include(m => m.User).Include(m => m.Mission).ToList();


            if (RelatedMission.Any(rml => rml.City.Name == missions.City.Name))
            {
                RelatedMission = RelatedMission.Where(rml => rml.City.Name == missions.City.Name).ToList();
            }
            if (RelatedMission.Any(rml => rml.Country.Name == missions.Country.Name))
            {
                RelatedMission = RelatedMission.Where(rml => rml.Country.Name == missions.Country.Name).ToList();
            }
            if (RelatedMission.Any(rml => rml.Theme.Title == missions.Theme.Title))
            {
                RelatedMission = RelatedMission.Where(rml => rml.Theme.Title == missions.Theme.Title).ToList();
            }

            var resultRelatedMission = new relatedmissionviewmodel
            {
                missionList = RelatedMission,
                goalMissionList = goalmission,
                favorite = addfavorite,
                rate = rates,


            };
            return resultRelatedMission;

        }

        public List<User> GetUsers()
        {
            return _cIPLATFORMDbContext.Users.ToList();
        }
        public string recommend(List<long> userids, long misssionid, string fromuserId)
        {

            var mailBody = "<h1>Click this link to view mission</h1><br><h2><a href='https://localhost:7093/Mission/volunteerpage/?id=" + misssionid + "' >View Mission</h2>";
            var users = _cIPLATFORMDbContext.Users.Where(u => userids.Contains(u.UserId));
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("devppatel6685@gmail.com"));

            var name = _cIPLATFORMDbContext.Users.SingleOrDefault(m => m.UserId == Convert.ToInt64(fromuserId));

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("devppatel6685@gmail.com", "uvqekuakbgseptda");

            foreach (var user in users)
            {
                MissionInvite model = new MissionInvite();
                model.MissionId = misssionid;
                model.FromUserId = Convert.ToInt64(fromuserId);
                model.ToUserId = user.UserId;
                email.To.Add(MailboxAddress.Parse(user.Email));
                email.Subject = name.FirstName + " " + name.LastName + " has recommended mission to you";
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = mailBody };
                smtp.Send(email)
;
                _cIPLATFORMDbContext.MissionInvites.Add(model);
            }
            _cIPLATFORMDbContext.SaveChanges();
            smtp.Disconnect(true);
            return "success";
        }





    }
}



