using CI_PLATFORM.Entities.Models;
using CI_PLATFORM.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CIPLATFORM.Controllers
{
    public class PlatformController : Controller
    {
        public readonly IPlatformRepository _PlatformRepository;
        public PlatformController(IPlatformRepository PlatformRepository)
        {
            _PlatformRepository = PlatformRepository;
        }
        public  IActionResult HomeGrid()
        {           
            string name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;


            //ViewBag.country = _PlatformRepository.GetCountryData();

            //ViewBag.skill = _PlatformRepository.GetSkills();

            //ViewBag.theme = _PlatformRepository.GetMissionThemes();


            //var data = _PlatformRepository.getCards();



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

            //List<MissionRating> missionRatings = _PlatformRepository.GetMissionDetails();
            //ViewBag.missionRatings = missionRatings;

            var totalMission = _PlatformRepository.GetMissionCount();
            ViewBag.totalMission = totalMission;

            return View();   
        }
        public IActionResult Filter(List<int>? cityId, List<int>? countryId, List<int>? themeId, List<int>? skillId, string? search, int? sort)
        {
            List<Mission> cards = _PlatformRepository.Filter(cityId, countryId, themeId, skillId, search, sort);
            ViewBag.MissionDeails = cards;

            return PartialView("_GridCard", cards);
        }



        public IActionResult HomeList()
        {
            return View();
        }
        public JsonResult GetCitys(int countryId)
        {
            List<City> city = _PlatformRepository.GetCityData(countryId);
            var json = JsonConvert.SerializeObject(city);


            return Json(json);
        }

    }
}
