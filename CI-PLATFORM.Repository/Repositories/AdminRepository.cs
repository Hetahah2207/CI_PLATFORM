using CI_PLATFORM.Entities.Data;
using CI_PLATFORM.Entities.Models;
using CI_PLATFORM.Entities.ViewModels;
using CI_PLATFORM.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM.Repository.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        public readonly CiPlatformContext _CiPlatformContext;
        public AdminRepository(CiPlatformContext CiPlatformContext)
        {
            _CiPlatformContext = CiPlatformContext;
        }
        public AdminViewModel getData()
        {
            AdminViewModel um = new AdminViewModel();
            {
                um.users = _CiPlatformContext.Users.ToList();
                um.missions = _CiPlatformContext.Missions.ToList();
            }
            return um;
        }
        public List<User> UserFilter(string search)
        {
            List<User> users = _CiPlatformContext.Users.ToList();
            if (search != null)
            {
                search = search.ToLower();
                users = users.Where(x => x.FirstName.ToLower().Contains(search)).ToList();
            }
            return users;

        }
    }
}
