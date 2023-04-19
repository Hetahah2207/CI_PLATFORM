﻿using CI_PLATFORM.Entities.Models;
using CI_PLATFORM.Entities.ViewModels;
using CI_PLATFORM.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CIPLATFORM.Controllers
{
    public class AdminController : Controller
    {
        public readonly IAdminRepository _AdminRepository;
        public AdminController(IAdminRepository AdminRepository)
        {
            _AdminRepository = AdminRepository;
        }
        public IActionResult Admin()
        {
            AdminViewModel am = _AdminRepository.getData();
            ViewBag.Totalpages = Math.Ceiling(am.users.Count() / 5.0);
            am.users = am.users.Skip((1 - 1) * 5).Take(5).ToList();

            ViewBag.Totalpages2 = Math.Ceiling(am.CmsPages.Count() / 5.0);
            am.CmsPages = am.CmsPages.Skip((1 - 1) * 5).Take(5).ToList();

            ViewBag.Totalpages3 = Math.Ceiling(am.missions.Count() / 5.0);
            am.missions = am.missions.Skip((1 - 1) * 5).Take(5).ToList();

            ViewBag.Totalpages4 = Math.Ceiling(am.missionThemes.Count() / 5.0);
            am.missionThemes = am.missionThemes.Skip((1 - 1) * 5).Take(5).ToList();

            ViewBag.Totalpages5 = Math.Ceiling(am.missionSkills.Count() / 5.0);
            am.missionSkills = am.missionSkills.Skip((1 - 1) * 5).Take(5).ToList();

            ViewBag.Totalpages6 = Math.Ceiling(am.missionapplications.Count() / 5.0);
            am.missionapplications = am.missionapplications.Skip((1 - 1) * 5).Take(5).ToList();

            ViewBag.Totalpages7 = Math.Ceiling(am.stories.Count() / 5.0);
            am.stories = am.stories.Skip((1 - 1) * 5).Take(5).ToList();
            ViewBag.pg_no = 1;
            return View(am);
        }
        [HttpPost]
        public IActionResult Admin(AdminViewModel obj, int command)
        {
            if (command == 2)
            {
                bool addcmspage = _AdminRepository.addcms(obj, command);
                if (addcmspage)
                    TempData["true"] = "cmspage added Successfully";
                else
                    TempData["false"] = "cmspage updated Successfully";
            }
            if (command == 4)
            {
                bool addcmspage = _AdminRepository.addcms(obj, command);
                if (addcmspage)
                    TempData["true"] = "Theme added Successfully";
                else
                    TempData["false"] = "Theme updated Successfully";
            }

            AdminViewModel am = _AdminRepository.getData();
            ViewBag.Totalpages = Math.Ceiling(am.users.Count() / 5.0);
            am.users = am.users.Skip((1 - 1) * 5).Take(5).ToList();

            ViewBag.Totalpages2 = Math.Ceiling(am.CmsPages.Count() / 5.0);
            am.CmsPages = am.CmsPages.Skip((1 - 1) * 5).Take(5).ToList();

            ViewBag.Totalpages3 = Math.Ceiling(am.missions.Count() / 5.0);
            am.missions = am.missions.Skip((1 - 1) * 5).Take(5).ToList();

            ViewBag.Totalpages4 = Math.Ceiling(am.missionThemes.Count() / 5.0);
            am.missionThemes = am.missionThemes.Skip((1 - 1) * 5).Take(5).ToList();

            ViewBag.Totalpages5 = Math.Ceiling(am.missionSkills.Count() / 5.0);
            am.missionSkills = am.missionSkills.Skip((1 - 1) * 5).Take(5).ToList();

            ViewBag.Totalpages6 = Math.Ceiling(am.missionapplications.Count() / 5.0);
            am.missionapplications = am.missionapplications.Skip((1 - 1) * 5).Take(5).ToList();

            ViewBag.Totalpages7 = Math.Ceiling(am.stories.Count() / 5.0);
            am.stories = am.stories.Skip((1 - 1) * 5).Take(5).ToList();
            ViewBag.pg_no = 1;
            return View(am);
        }
        public IActionResult UserFilter(string? search, int pg, string key)
        {
            AdminViewModel x = _AdminRepository.UserFilter(search, 0);
            AdminViewModel fusers = _AdminRepository.UserFilter(search, pg);




            ViewBag.pg_no = pg;
            ViewBag.Totalpages = Math.Ceiling(x.users.Count() / 5.0);
            ViewBag.Totalpages2 = Math.Ceiling(x.CmsPages.Count() / 5.0);
            ViewBag.Totalpages3 = Math.Ceiling(x.missions.Count() / 5.0);
            ViewBag.Totalpages4 = Math.Ceiling(x.missionThemes.Count() / 5.0);
            ViewBag.Totalpages5 = Math.Ceiling(x.missionSkills.Count() / 5.0);
            ViewBag.Totalpages6 = Math.Ceiling(x.missionapplications.Count() / 5.0);
            ViewBag.Totalpages7 = Math.Ceiling(x.stories.Count() / 5.0);



            if (key == "user")
            {
                return PartialView("_User", fusers);
            }
            if (key == "cms")
            {
                return PartialView("_CMSPages", fusers);
            }
            if (key == "mission")
            {
                return PartialView("_Mission", fusers);
            }
            if (key == "theme")
            {
                return PartialView("_MissionTheme", fusers);
            }
            if (key == "skill")
            {
                return PartialView("_MissionSkill", fusers);
            }
            if (key == "application")
            {
                return PartialView("_Application", fusers);
            }
            if (key == "story")
            {
                return PartialView("_Story", fusers);
            }
            return View(fusers);
        }

        public IActionResult EditForm(int id, string page)
        {
            AdminViewModel am = _AdminRepository.getData();

            ViewBag.Totalpages = Math.Ceiling(am.users.Count() / 5.0);
            am.users = am.users.Skip((1 - 1) * 5).Take(5).ToList();

            ViewBag.Totalpages2 = Math.Ceiling(am.CmsPages.Count() / 5.0);
            am.CmsPages = am.CmsPages.Skip((1 - 1) * 5).Take(5).ToList();

            ViewBag.Totalpages3 = Math.Ceiling(am.missions.Count() / 5.0);
            am.missions = am.missions.Skip((1 - 1) * 5).Take(5).ToList();

            ViewBag.Totalpages4 = Math.Ceiling(am.missionThemes.Count() / 5.0);
            am.missionThemes = am.missionThemes.Skip((1 - 1) * 5).Take(5).ToList();

            ViewBag.Totalpages5 = Math.Ceiling(am.missionSkills.Count() / 5.0);
            am.missionSkills = am.missionSkills.Skip((1 - 1) * 5).Take(5).ToList();

            ViewBag.Totalpages6 = Math.Ceiling(am.missionapplications.Count() / 5.0);
            am.missionapplications = am.missionapplications.Skip((1 - 1) * 5).Take(5).ToList();

            ViewBag.Totalpages7 = Math.Ceiling(am.stories.Count() / 5.0);
            am.stories = am.stories.Skip((1 - 1) * 5).Take(5).ToList();
            ViewBag.pg_no = 1;

            if (page == "nav-cms")
            {
                am.CmsPage = _AdminRepository.EditForm(id, page).CmsPage;
                return PartialView("_CMSPages", am);
            }
            else if(page == "nav-theme")
            {
                am.missionTheme = _AdminRepository.EditForm(id, page).missionTheme;
                return PartialView("_MissionTheme", am);
            }
            return PartialView("_CMSPages", am);
        }
        public IActionResult DeleteActivity(int id, int page)
        {
            bool tm = _AdminRepository.deleteactivity(id, page);
            if (tm)
                TempData["delete"] = "Deleted successfully";
            else
                TempData["delete"] = "Activity cannot deleted";
            return RedirectToAction("Admin");
        }
        public IActionResult Approval(int id, int page, int status)
        {
            bool tm = _AdminRepository.Approval(id, page, status);
            if (tm)
                TempData["accept"] = "Request Accepted";
            else
                TempData["decline"] = "Request declined";
            return RedirectToAction("Admin");
        }
    }
}
