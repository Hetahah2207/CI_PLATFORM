using CI_PLATFORM.Entities.Data;
using CI_PLATFORM.Entities.Models;
using CI_PLATFORM.Entities.ViewModels;
using CI_PLATFORM.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CIPLATFORM.Controllers
{
    [Authorize]
    public class PlatformController : Controller
    {
        public readonly IPlatformRepository _PlatformRepository;
        public readonly CiPlatformContext _CiPlatformContext;

        public PlatformController(CiPlatformContext CiPlatformContext, IPlatformRepository PlatformRepository)
        {
            _PlatformRepository = PlatformRepository;
            _CiPlatformContext = CiPlatformContext;
        }
        
        public IActionResult HomeGrid()
            {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;

            string avtar = HttpContext.Session.GetString("Avtar");
            ViewBag.Avtar = avtar;

            if (name != null)
            {
                int UserId = (int)HttpContext.Session.GetInt32("UId");
                ViewBag.UId = UserId;

            }
            
            List<Country> countries = _PlatformRepository.GetCountryData();
            ViewBag.countries = countries;

            List<City> Cities = _PlatformRepository.GetCitys();
            ViewBag.Cities = Cities;

            List<MissionTheme> themes = _PlatformRepository.GetMissionThemes();
            ViewBag.themes = themes;

            List<MissionSkill> skills = _PlatformRepository.GetSkills();
            ViewBag.skills = skills;

            List<Mission> missionDeails = _PlatformRepository.GetMissionDetails();
            ViewBag.MissionDeails = missionDeails;

            var totalMission = _PlatformRepository.GetMissionCount();
            ViewBag.totalMission = totalMission;


            CardsViewModel ms = _PlatformRepository.getCards();


            ViewBag.Totalpages = Math.Ceiling(ms.missions.Count() / 6.0);
            ms.missions = ms.missions.Skip((1 - 1) * 6).Take(6).ToList();
            ViewBag.pg_no = 1;

            return View(ms);
        }
        public IActionResult Filter(List<int>? cityId, List<int>? countryId, List<int>? themeId, List<int>? skillId, string? search, int? sort, int pg , int view)
        {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;

            string avtar = HttpContext.Session.GetString("Avtar");
            ViewBag.Avtar = avtar;  

            if (name != null)
            {
                int UserId = (int)HttpContext.Session.GetInt32("UId");
                ViewBag.UId = UserId;
            }

            List<Mission> cards = _PlatformRepository.Filter(cityId, countryId, themeId, skillId, search, sort, pg, @ViewBag.UId);
            CardsViewModel platformModel = new CardsViewModel();
            platformModel = _PlatformRepository.getCards();
            platformModel.missions = cards;

            if (cards.Count == 0)
            {
                return PartialView("_nomissionfound");
            }
            else if (cards.Count >= 1)
            {
                ViewBag.totalMission = _PlatformRepository.Filter(cityId, countryId, themeId, skillId, search, sort, 0, @ViewBag.UId).Count;
            }

            ViewBag.pg_no = pg;
            ViewBag.Totalpages = Math.Ceiling(_PlatformRepository.Filter(cityId, countryId, themeId, skillId, search, sort, 0, @ViewBag.UId).Count / 6.0);
           
            platformModel.missions = cards.Skip((1 - 1) * 6).Take(6).ToList();

            if (view == 0 || view == 1)            {                return PartialView("_GridCard", platformModel);            }            else            {                return PartialView("_ListCard", platformModel);            }
            //return PartialView("_FilterMission", platformModel);
        }
        public IActionResult StoryFilter(string? search, int pg)
        {
            List<Story> cards = _PlatformRepository.StoryFilter(search, pg);

            StoryListingViewModel sModel = new StoryListingViewModel();
            {
                sModel.stories = cards;
            }

            ViewBag.pg_no = pg;
            ViewBag.Totalpages = Math.Ceiling(_PlatformRepository.StoryFilter(search, 0).Count() / 6.0);
            sModel.stories = cards.Skip((1 - 1) * 6).Take(6).ToList();

            return PartialView("_StoryCard", sModel);
        }
        public IActionResult MissionListing(int mid)
        {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;

            string avtar = HttpContext.Session.GetString("Avtar");
            ViewBag.Avtar = avtar;

            if (name != null)
            {
                int UserId = (int)HttpContext.Session.GetInt32("UId");
                ViewBag.UId = UserId;
                var rating = _CiPlatformContext.MissionRatings.FirstOrDefault(x => x.UserId == UserId && x.MissionId == mid);
                if(rating != null)
                {
                    ViewBag.rating = rating.Rating;
                }
               
            }

            ViewBag.MId = mid;
            
            MissionListingViewModel ml = _PlatformRepository.GetCardDetail(mid, @ViewBag.UId);
            
            return View(ml);
        }
        [HttpPost]
        public JsonResult MissionRating(int mid, int rating)
        {
            int UserId = (int)HttpContext.Session.GetInt32("UId");
            bool success = _PlatformRepository.MissionRating(UserId, mid, rating);
            return Json(success);
        }
        public void RecommandToCoWorker(List<int> toUserId, int mid)
        {
            int FromUserId = (int)HttpContext.Session.GetInt32("UId");
            bool check = _PlatformRepository.MICheck(mid,FromUserId,toUserId);
            if (check)
            {
                _PlatformRepository.RecommandToCoWorker(FromUserId, toUserId, mid);
            }
            MissionListingViewModel volunteerModel = _PlatformRepository.GetCardDetail(mid, FromUserId);

        }
        public void RecommandStory(List<int> toUserId, int sid)
        {
            int FromUserId = (int)HttpContext.Session.GetInt32("UId");
            bool check = _PlatformRepository.SICheck(sid, FromUserId, toUserId);
            if (check)
            {
                _PlatformRepository.RecommandStory(FromUserId, toUserId, sid);
            }
            StoryListingViewModel volunteerModel = _PlatformRepository.GetStory(sid, FromUserId);

        }
        public void AddComment(int obj, string comnt)
        {
            int UserId = (int)HttpContext.Session.GetInt32("UId");
            _PlatformRepository.addComment(obj, UserId, comnt);
        }
        [HttpPost]
        public bool applyMission(int missionId)
        {
            int UserId = (int)HttpContext.Session.GetInt32("UId");
            var apply = _PlatformRepository.applyMission(missionId, UserId);
            if (apply == true)
            {
                //TempData["success"] = "Applied Successfully...";
                return apply;
            }
            //TempData["error"] = "You've already Applied... ";
            return false;
        }
        [HttpPost]
        public bool AddMissionToFavourite(int missionId)
        {
            var userId = (int)HttpContext.Session.GetInt32("UId");

            var fav = _PlatformRepository.addToFav(missionId, userId);
            if (fav != true)
            {
                _CiPlatformContext.SaveChanges();
            }
            else
            {
                _CiPlatformContext.SaveChanges();

            }
            return fav;
        }
        //public JsonResult GetCitys(int countryId)
        //{
        //    List<City> city = _PlatformRepository.GetCityData(countryId);
        //    var json = JsonConvert.SerializeObject(city);

        //    return Json(json);
        //}

        public JsonResult GetCitys(List<int>? countryId)        {            List<City> city = _PlatformRepository.GetCityData(countryId);            var json = JsonConvert.SerializeObject(city);            return Json(json);        }
        public IActionResult StoryListing ()
        {
            string? name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;

            string? avtar = HttpContext.Session.GetString("Avtar");
            ViewBag.Avtar = avtar;

            if (name != null)
            {
                int? UserId = (int?)HttpContext.Session.GetInt32("UId");
                ViewBag.UId = UserId;
            }
            List<Country> countries = _PlatformRepository.GetCountryData();
            ViewBag.countries = countries;

            List<City> Cities = _PlatformRepository.GetCitys();
            ViewBag.Cities = Cities;

            List<MissionTheme> themes = _PlatformRepository.GetMissionThemes();
            ViewBag.themes = themes;

            List<MissionSkill> skills = _PlatformRepository.GetSkills();
            ViewBag.skills = skills;


            StoryListingViewModel sl = _PlatformRepository.GetStoryDetail();


            ViewBag.Totalpages = Math.Ceiling(sl.stories.Count() / 6.0);
            sl.stories = sl.stories.Skip((1 - 1) * 6).Take(6).ToList();
            ViewBag.pg_no = 1;

            return View(sl);
        }
        public IActionResult ShareStory()
        {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;

            string avtar = HttpContext.Session.GetString("Avtar");
            ViewBag.Avtar = avtar;

            if (name != null)
            {
                int UserId = (int)HttpContext.Session.GetInt32("UId");
                ViewBag.UId = UserId;
            }
            StoryListingViewModel ss = _PlatformRepository.ShareStory(@ViewBag.UId);
            return View(ss);
        }
        [HttpPost]
        public IActionResult ShareStory(StoryListingViewModel obj, int status, List<IFormFile> file)
        {

            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;

            string avtar = HttpContext.Session.GetString("Avtar");
            ViewBag.Avtar = avtar;

            if (name != null)
            {
                int UserId = (int)HttpContext.Session.GetInt32("UId");
                ViewBag.UId = UserId;
            }
           
            bool abc = _PlatformRepository.saveStory(obj, status, @ViewBag.UId);
            bool image = _PlatformRepository.SaveImage(obj, file);
            
            if (status == 1)
            {
                StoryListingViewModel ss = _PlatformRepository.ShareStory(@ViewBag.UId);
               
                return View(ss);
            }
            if (status == 2)
            {
                return RedirectToAction("StoryListing", "Platform");
            }
            return View();
        }
        public IActionResult StoryDetail(int sid)
        {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;

            string avtar = HttpContext.Session.GetString("Avtar");
            ViewBag.Avtar = avtar;

            if (name != null)
            {
                int UserId = (int)HttpContext.Session.GetInt32("UId");
                ViewBag.UId = UserId;
            }

            ViewBag.sid = sid;
            StoryView sv = _PlatformRepository.addview(sid, ViewBag.UId);
            StoryListingViewModel sd = _PlatformRepository.GetStory(sid, @ViewBag.UId);

            return View(sd);
        }

        [HttpPost]
        public JsonResult CheckData(int mid)
        {
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;

            if (name != null)
            {
                int UserId = (int)HttpContext.Session.GetInt32("UId");
                ViewBag.UId = UserId;
            }
            // Check if the saved data exists in your data store based on the selected option
            var SM = _PlatformRepository.getData(mid, @ViewBag.UId);

            var dataExists = JsonConvert.SerializeObject(SM);



            // Return a boolean value indicating whether the data exists
            return Json(dataExists);

            //return View("~/Story/StoryApply", StoryModel);
        }
    }
}
