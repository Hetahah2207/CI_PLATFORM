using CI_PLATFORM.Entities.Models;using CI_PLATFORM.Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using System;using System.Collections.Generic;using System.Linq;using System.Text;using System.Threading.Tasks;namespace CI_PLATFORM.Repository.Interface{
    public interface IPlatformRepository
    {
        public List<Country> GetCountryData();
        public List<City> GetCitys();
        public List<City> GetCityData(List<int>? countryId);
        //public List<City> GetCityData(int countryId);
        public List<MissionTheme> GetMissionThemes();
        //public List<Skill> GetSkills();
        //public List<Mission> GetMissions();
        public List<MissionSkill> GetSkills();
        public List<Mission> GetMissionDetails();
        public int GetMissionCount();
        public CardsViewModel getCards();
        //public List<MissionSkill> GetMissionSkills();
        public List<Mission> Filter(List<int>? cityId, List<int>? countryId, List<int>? themeId, List<int>? skillId, string? search, int? sort, int pg, int UId);
        //public List<Story> StoryFilter(string? search);
        public List<Story> StoryFilter(string? search, int pg);
        public MissionListingViewModel GetCardDetail(int mid, int uid);
        public List<MissionMedium> media(int mid);
        public bool addToFav(int missionId, int userId);
        public List<MissionDocument> document(int mid);
        public void addComment(int mid, int uid, string comnt);
        public bool applyMission(int mid, int uid);
        public void RecommandToCoWorker(int FromUserId, List<int> ToUserId, int mid);
        public bool MICheck(int mid, int userId, List<int> toUserId);
        public bool SICheck(int sid, int userId, List<int> toUserId);
        public StoryListingViewModel GetStoryDetail();
        public List<StoryMedium> smedia(int sid);
        public StoryListingViewModel GetStory(int sid, int uid);
        public void RecommandStory(int FromUserId, List<int> ToUserId, int sid);
        public bool MissionRating(int userId, int mid, int rating);
        public StoryView addview(int sid, int UId);
        public List<MissionApplication> Mission(int UId);
        public StoryListingViewModel ShareStory(int UId);
        public bool saveStory(StoryListingViewModel obj, int status, int uid);
        public bool SaveImage(StoryListingViewModel obj, List<IFormFile> file);
        public StoryListingViewModel getData(int mid, int uid);
        public NotificationSetting getsettings(int uid);
        public void settings(string[] settings, int uid);
        public List<NotificationMessage> getnotification(int uid);
        public bool SendMail(NotificationMessage message);
        public void readNotification(int id);
        public void clearNotification(int uid);
        public int GetnotificationCount(int uid);
    }}