using CI_PLATFORM.Entities.Data;
using CI_PLATFORM.Entities.Models;
using CI_PLATFORM.Entities.ViewModels;
using CI_PLATFORM.Repository.Interface;
using Microsoft.EntityFrameworkCore;
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
                um.CmsPages = _CiPlatformContext.CmsPages.ToList();
                um.missionapplications = _CiPlatformContext.MissionApplications.Include(x =>x.Mission).Include(x => x.User).ToList();
                um.stories = _CiPlatformContext.Stories.Include(x => x.User).ToList();
                um.missionSkills = _CiPlatformContext.MissionSkills.Include(x => x.Mission).Include(x => x.Skill).ToList();
                um.missionThemes = _CiPlatformContext.MissionThemes.ToList();
            }
            return um;
        }
        public AdminViewModel UserFilter(string search,int pg)
        {
           
           
            
            var pageSize = 5;
            AdminViewModel obj = getData();
            if (search != null)
            {
                search = search.ToLower();
                obj.users = obj.users.Where(x => x.FirstName.ToLower().Contains(search)).ToList();
                obj.missions = obj.missions.Where(x => x.Title.ToLower().Contains(search) || x.MissionType.ToLower().Contains(search)).ToList();
                obj.missionapplications = obj.missionapplications.Where(x => x.Mission.Title.ToLower().Contains(search) || x.User.FirstName.ToLower().Contains(search) || x.User.LastName.ToLower().Contains(search)).ToList();
                obj.stories = obj.stories.Where(x => x.Title.ToLower().Contains(search) || x.User.FirstName.ToLower().Contains(search) || x.User.LastName.ToLower().Contains(search)).ToList();
                obj.CmsPages = obj.CmsPages.Where(x => x.Title.ToLower().Contains(search)).ToList();
                obj.missionThemes = obj.missionThemes.Where(x => x.Title.ToLower().Contains(search)).ToList();
                obj.missionSkills = obj.missionSkills.Where(x => x.Skill.SkillName.ToLower().Contains(search)).ToList();
            }
            if (pg != 0)
            {
                obj.users = obj.users.Skip((pg - 1) * pageSize).Take(pageSize).ToList();
                obj.missions = obj.missions.Skip((pg - 1) * pageSize).Take(pageSize).ToList();
                obj.missionapplications = obj.missionapplications.Skip((pg - 1) * pageSize).Take(pageSize).ToList();
                obj.stories = obj.stories.Skip((pg - 1) * pageSize).Take(pageSize).ToList();
                obj.CmsPages = obj.CmsPages.Skip((pg - 1) * pageSize).Take(pageSize).ToList();
                obj.missionThemes = obj.missionThemes.Skip((pg - 1) * pageSize).Take(pageSize).ToList();
                obj.missionSkills = obj.missionSkills.Skip((pg - 1) * pageSize).Take(pageSize).ToList();
            }
            return obj;
        }
        public bool addcms(AdminViewModel obj,int command)
        {
            CmsPage cms = new CmsPage();
            {
                cms.Title = obj.CmsPage.Title;
                cms.Description = obj.CmsPage.Description;
                cms.Slug = obj.CmsPage.Slug;
            }
            _CiPlatformContext.Add(cms);
            _CiPlatformContext.SaveChanges();
            return true;
        }
    }
}
