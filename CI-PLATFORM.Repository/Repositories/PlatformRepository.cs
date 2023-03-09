using CI_PLATFORM.Entities.Data;
using CI_PLATFORM.Entities.Models;
using CI_PLATFORM.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM.Repository.Repositories
{
    public class PlatformRepository : IPlatformRepository
    {

        public readonly CiPlatformContext _CiPlatformContext;
        public PlatformRepository(CiPlatformContext CiPlatformContext)
        {
            _CiPlatformContext = CiPlatformContext;
        }
        public List<Country> GetCountryData()
        {

            List<Country> country = _CiPlatformContext.Countries.ToList();
            return country;

        }
        public List<City> GetCityData(int countryId)
        {

            List<City> city = _CiPlatformContext.Cities.Where(i => i.CountryId == countryId).ToList();
            return city;

        }
        public List<MissionTheme> GetMissionThemes()
        {
            List<MissionTheme> themes = _CiPlatformContext.MissionThemes.ToList();
            return themes;

        }

        public List<Skill> GetSkills()
        {
            List<Skill> skills = _CiPlatformContext.Skills.ToList();
            return skills;

        }
        public List<Mission> GetMissions()
        {

            var missions = _CiPlatformContext.Missions.ToList();
            return missions;

        }
        public List<Mission> GetMissionDetails()
        {
            List<Mission> missionDetails = _CiPlatformContext.Missions.Include(m => m.City).Include(m => m.Theme).Include(m => m.MissionMedia).Include(m =>m.MissionRatings).ToList();
            return missionDetails;
        }
        public int GetMissionCount()
        {

            int missionNumber = _CiPlatformContext.Missions.Count();
            return missionNumber;

        }
        //public string GetCityName(long cityId)
        //{

        //    City city = _CiPlatformContext.Cities.FirstOrDefault(i => i.CityId == cityId);
        //    return city.Name;

        //}

        public List<Mission> Filter(List<int>? cityId, List<int>? countryId, List<int>? themeId, List<int>? skillId)
        {
            List<Mission> cards = new List<Mission>();
            var missioncards = GetMissionDetails();
            var Missionskills = _CiPlatformContext.MissionSkills.ToList();
            List<int> temp = new List<int>();

            if (cityId.Count != 0 || countryId.Count != 0 || themeId.Count != 0 || skillId.Count != 0)
            {
                foreach (var n in cityId)
                {
                    foreach (var item in missioncards)
                    {
                        bool citychek = cards.Any(x => x.MissionId == item.MissionId);
                        if (item.CityId == n && citychek == false)
                        {
                            cards.Add(item);
                        }

                    }
                }

                foreach (var n in countryId)
                {
                    foreach (var item in missioncards)
                    {
                        bool countrychek = missioncards.Any(x => x.MissionId == item.MissionId);
                        if (item.CountryId == n && countrychek == true)
                        {
                            cards.Add(item);
                        }
                    }
                }

                foreach (var n in themeId)
                {
                    foreach (var item in missioncards)
                    {
                        bool themechek = cards.Any(x => x.MissionId == item.MissionId);
                        if (item.ThemeId == n && themechek == false)
                        {
                            cards.Add(item);
                        }
                    }
                }

                foreach (var n in skillId)
                {

                    foreach (var item in Missionskills)
                    {
                        if (item.SkillId == n)
                        {
                            temp.Add((int)item.MissionId);
                        }
                        foreach (var item2 in temp)
                        {
                            bool skillchek = cards.Any(x => x.MissionId == item2);
                            if (skillchek == false)
                            {
                                cards.Add(missioncards.FirstOrDefault(x => x.MissionId == item2));
                            }
                        }
                    }
                }
                return cards;
            }

            else if (cityId.Count == 0 && countryId.Count == 0 && themeId.Count == 0 && skillId.Count == 0 )
            {
                foreach (var item in missioncards)
                {
                    cards.Add(item);
                }
            }

            //if (search != null)
            //{
            //    foreach (var n in missioncards)
            //    {
            //        var title = n.Title.ToLower();
            //        if (title.Contains(search.ToLower()))
            //        {
            //            cards.Add(n);
            //        }
            //    }

            //}

            //if (sort != null)
            //{
            //    if (sort == 1)
            //    {

            //        cards = cards.OrderByDescending(x => x.CreatedAt).ToList();
            //    }
            //    if (sort == 2)
            //    {
            //        cards = cards.OrderBy(x => x.CreatedAt).ToList();
            //    }

            //}
            return cards;

        }


    }
}