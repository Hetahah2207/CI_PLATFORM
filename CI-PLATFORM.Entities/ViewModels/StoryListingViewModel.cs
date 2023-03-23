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
        public Story story { get; set; }
        public List<Story> stories { get; set; }
        public List<StoryMedium> storymedias { get; set; }
    }
}
