using CI_PLATFORM.Entities.Models;
using CI_PLATFORM.Entities.ViewModels;
using CI_PLATFORM.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace CIPLATFORM.Controllers
{
    public class ProfileController : Controller
    {
        public readonly IProfileRepository _ProfileRepository;
        public readonly IPlatformRepository _PlatformRepository;
        public ProfileController(IProfileRepository ProfileRepository, IPlatformRepository PlatformRepository)
        {
            _ProfileRepository = ProfileRepository;
            _PlatformRepository = PlatformRepository;
        }
        public IActionResult Profile()
        {
            string? name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;

            string? avtar = HttpContext.Session.GetString("Avtar");
            ViewBag.Avtar = avtar;

            List<Country> countries = _PlatformRepository.GetCountryData();
            ViewBag.countries = countries;

            List<City> Cities = _PlatformRepository.GetCitys();
            ViewBag.Cities = Cities;

            if (name != null)
            {
                int? UserId = (int)HttpContext.Session.GetInt32("UId");
                ViewBag.UId = UserId;

                ProfileViewModel pm = _ProfileRepository.getUser(@ViewBag.UId);
                return View(pm);
            }

            return View();
        }
        [HttpPost]
        public IActionResult Profile(ProfileViewModel obj, int save)
        {
            List<Country> countries = _PlatformRepository.GetCountryData();
            ViewBag.countries = countries;

            List<City> Cities = _PlatformRepository.GetCitys();
            ViewBag.Cities = Cities;
            string? name = HttpContext.Session.GetString("Uname");
            ViewBag.Uname = name;

            string? avtar = HttpContext.Session.GetString("Avtar");
            ViewBag.Avtar = avtar;

            if (name != null)
            {
                int? UserId = (int)HttpContext.Session.GetInt32("UId");
                ViewBag.UId = UserId;
            }

            if (save == 1)
            {

                if (obj.resetPass.Password == null || obj.resetPass.ConfirmPassword == null || obj.resetPass.OldPassword == null)
                {
                    TempData["false"] = "All fields are required! Plz Enter Value For All 3 Fields";
                }
                if (obj.resetPass.ConfirmPassword != obj.resetPass.Password)
                {
                    TempData["morefalse"] = "Password & ConfirmPassword is Diffrent";
                }
                else
                {

                    bool resetpass = _ProfileRepository.changepassword(obj, @ViewBag.UId);
                    if (resetpass)
                    {
                        TempData["true"] = "password updated";
                    }
                    else
                    {
                        TempData["false"] = "entered password is wrong";
                    }
                }

                return View(obj);
            }

            //if (save == 2)
            //{
            //    bool saveskills = _ProfileRepository.saveSkills(obj, save, @ViewBag.UId);
            //    if (saveskills)
            //    {
            //        TempData["true"] = "Profile Updated Successfully";
            //    }
            //}


            if (save == 3)
            {
                bool saveprofile = _ProfileRepository.saveProfile(obj, @ViewBag.UId);

                if (saveprofile)
                {
                    TempData["true"] = "Profile Updated Successfully";
                }
                else
                {
                    TempData["false"] = "User does not exist";
                }
                return View(obj);
            }
            if (save == 4)
            {
                bool ContactUs = _ProfileRepository.ContactUs(obj);
                if (ContactUs)
                {
                    TempData["true"] = "Your Mail Has Been Sent";
                }
                else
                {
                    TempData["false"] = "Error occured during the process";
                }
                return View(obj);
            }
            return View(obj);
        }
    }
}
