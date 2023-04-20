﻿using CI_PLATFORM.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM.Entities.ViewModels
{
    public class MissionListingViewModel
    {
        public Mission? missions { get; set; }

        public List<MissionMedium>? missionmedias { get; set; }

        public List<MissionDocument>? missiondocuments { get; set; }

        public List<Mission>? relatedmissions { get; set; }
        //public List<MissionRating>? missionratings { get; set; }
   
        public List<MissionSkill>? missionskills { get; set; }
        public List<MissionApplication>? missionapplications { get; set; }
    
        public List<Comment>? comments { get; set; }
        public List<FavoriteMission>? favoriteMissions { get; set; }
        public string? commentDescription { get; set; }
        public List<User>? coworkers { get; set; }
        public int UserRating { get; set; }

    }
}
