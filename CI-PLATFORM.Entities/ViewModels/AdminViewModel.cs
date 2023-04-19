using CI_PLATFORM.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM.Entities.ViewModels
{
    public class AdminViewModel
    {
        public List<User> users { get; set; } = new List<User>();
        public List<Mission> missions { get; set; } = new List<Mission>();
        public List<Mission> allmissions { get; set; } = new List<Mission>();
        public List<CmsPage> CmsPages { get; set; } = new List<CmsPage>();
        public CmsPage CmsPage { get; set; } = new CmsPage();
        public List<MissionApplication>? missionapplications { get; set; } = new List<MissionApplication>();
        public List<Story> stories { get; set; } = new List<Story>();
        public List<MissionSkill> missionSkills { get; set; } = new List<MissionSkill>();
        public List<MissionSkill> allmissionSkills { get; set; } = new List<MissionSkill>();
        public MissionSkill missionSkill { get; set; }  = new MissionSkill();

        public List<MissionTheme> missionThemes { get; set; } = new List<MissionTheme>();
        public MissionTheme missionTheme { get; set; } = new MissionTheme();
    }
}
