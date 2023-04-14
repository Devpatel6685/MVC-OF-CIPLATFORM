using CI_PLATFORM.Entities.DataModels;
using CI_PLATFORM_.repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM_.repository.Repository
{
    public class AdminRepository : IAdminInterface
    {
        private readonly CIPLATFORMDbContext _ciplatfromdbcontext;

        public AdminRepository(CIPLATFORMDbContext ciplatfromdbcontext)
        {
            _ciplatfromdbcontext = ciplatfromdbcontext;
        }
    }
}
