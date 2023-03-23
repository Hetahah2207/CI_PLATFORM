using CI_PLATFORM.Entities.Models;using CI_PLATFORM.Entities.ViewModels;
using System;using System.Collections.Generic;using System.Linq;using System.Text;using System.Threading.Tasks;namespace CI_PLATFORM.Repository.Interface{
    public interface IPlatformRepository
    {
        public List<Country> GetCountryData();
        public List<City> GetCitys();
        public List<City> GetCityData(int countryId);
        public List<MissionTheme> GetMissionThemes();
        //public List<Skill> GetSkills();
        //public List<Mission> GetMissions();
        public List<MissionSkill> GetSkills();
        public List<Mission> GetMissionDetails();
        public int GetMissionCount();
        public CardsViewModel getCards();
        //public List<MissionSkill> GetMissionSkills();
        public List<Mission> Filter(List<int>? cityId, List<int>? countryId, List<int>? themeId, List<int>? skillId, string? search, int? sort);
        public MissionListingViewModel GetCardDetail(int mid);
        public List<MissionMedium> media(int mid);
        public bool addToFav(int missionId, int userId);
        public List<MissionDocument> document(int mid);
        public void addComment(int mid, int uid, string comnt);
        public bool applyMission(int mid, int uid);
        public void RecommandToCoWorker(int FromUserId, List<int> ToUserId, int mid);
        public StoryListingViewModel GetStoryDetail();
        public List<StoryMedium> smedia(int sid);
        public StoryListingViewModel GetStory(int sid);
        public void RecommandStory(int FromUserId, List<int> ToUserId, int sid);

    }}