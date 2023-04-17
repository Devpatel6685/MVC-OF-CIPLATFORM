using CI_PLATFORM.Entities.DataModels;
using CI_PLATFORM.Entities.ViewModels;
using CI_PLATFORM_.repository.Interface;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace CI_PLATFORM_.repository.Repository
{
    public class AdminRepository : IAdminInterface
    {
        private readonly CIPLATFORMDbContext _ciplatfromdbcontext;

        public AdminRepository(CIPLATFORMDbContext ciplatfromdbcontext)
        {
            _ciplatfromdbcontext = ciplatfromdbcontext;
        }
        public Adminviewmodel getuserdata(int pageindex, int pageSize)
        {
            var user = _ciplatfromdbcontext.Users.ToList();
            var model = new Adminviewmodel
            {
                /*user = user,*/
            };
            model.user = user.ToPagedList(pageindex, 10);
            return model;
        }
        public Adminviewmodel getmissiondata(int pageindex, int pageSize)
        {
            var misssion = _ciplatfromdbcontext.Missions.ToList();
            var model = new Adminviewmodel
            {

            };
            model.missions = misssion.ToPagedList(pageindex, 10);

            return model;
        }



        public Adminviewmodel getapplication(int pageindex, int pageSize)
        {
            var misssionapplication = _ciplatfromdbcontext.MissionApplications.ToList();
            var model = new Adminviewmodel
            {
                
            };
            model.missionApplications = misssionapplication.ToPagedList(pageindex, 10);
            return model;
        }
        public Adminviewmodel getcmspage(int pageindex, int pageSize)
        {
            var cms = _ciplatfromdbcontext.CmsPages.ToList();
            var model = new Adminviewmodel
            {
                
            };
            model.cmsPages = cms.ToPagedList(pageindex, 10);
            return model;
        }
    }
}
