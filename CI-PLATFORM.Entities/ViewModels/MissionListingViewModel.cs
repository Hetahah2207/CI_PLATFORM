using CI_PLATFORM.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM.Entities.ViewModels
{
    public class MissionListingViewModel
    {
        public Mission? missions { get; set; } = new Mission();

        public List<MissionMedium>? missionmedias { get; set; } = new List<MissionMedium>();

        public List<MissionDocument>? missiondocuments { get; set; }

        public List<Mission>? relatedmissions { get; set; }
        public List<MissionSkill>? missionskills { get; set; }
        public List<MissionApplication>? missionapplications { get; set; }

        public List<Comment>? comments { get; set; } = new List<Comment>();
        public List<FavoriteMission>? favoriteMissions { get; set; }
        public string? commentDescription { get; set; }
        public List<User>? coworkers { get; set; } = new List<User>();
        public int UserRating { get; set; }
        public long MissionId { get; set; }        public string? Avatar { get; set; }         public string? UserName { get; set; }
        public List<MissionInvite> alreadyinvite { get; set; } = new List<MissionInvite>();

    }
}
