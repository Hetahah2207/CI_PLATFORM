using CI_PLATFORM.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM.Entities.ViewModels
{
    public class StoryListingViewModel
    {
        public Story story { get; set; } = new Story();
        public List<Story> stories { get; set; } = new List<Story>();
        public List<StoryMedium> storymedias { get; set; } = new List<StoryMedium>();
        public List<User> coworkers { get; set; } = new List<User>();
        public List<MissionApplication> missions { get; set; } = new List<MissionApplication>();
        public List<string> simg { get; set; } = new List<string>();
        public string? url { get; set; }
        public List<StoryInvite> alreadyinvite { get; set; } = new List<StoryInvite>();
    }

}
