using CI_PLATFORM.Entities.Models;
using Microsoft.AspNetCore.Http;
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
        public User user { get; set; } = new User();
        public List<City> cities { get; set; } = new List<City>();
        public List<Country> countries { get; set; } = new List<Country>();
       
        public IFormFile? Avatarfile { get; set; }
        
        public List<Mission> missions { get; set; } = new List<Mission>();
        public Mission mission { get; set; } = new Mission();
        public List<MissionMedium> missionMedia = new List<MissionMedium>();
        public List<IFormFile>? missionDocuments { get; set; }
        public List<IFormFile>? missionDs { get; set; }
        public string? url { get; set; }
        public List<long> editmissionSkills { get; set; } = new List<long>();
        public List<MissionSkill> missionSkills = new List<MissionSkill>();
        public List<CmsPage> CmsPages { get; set; } = new List<CmsPage>();
        public CmsPage CmsPage { get; set; } = new CmsPage();
        public List<MissionApplication>? missionapplications { get; set; } = new List<MissionApplication>();
        public List<Story> stories { get; set; } = new List<Story>();
        public List<Skill> skills { get; set; } = new List<Skill>();
        public List<Skill> newskills { get; set; } = new List<Skill>();
        public Skill Skill { get; set; }  = new Skill();
        public List<MissionTheme> missionThemes { get; set; } = new List<MissionTheme>();
        public List<MissionTheme> newmissionThemes { get; set; } = new List<MissionTheme>();
        public MissionTheme missionTheme { get; set; } = new MissionTheme();
        public List<Banner> banners = new List<Banner>();
        public Banner banner { get; set; } = new Banner();

    }
}
