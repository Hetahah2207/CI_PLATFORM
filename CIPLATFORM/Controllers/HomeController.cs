using CI_PLATFORM.Entities.Data;
using CI_PLATFORM.Entities.Models;
using CIPLATFORM.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CIPLATFORM.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        //private readonly CiPlatformContext _CiPlatformDb;

        //public HomeController(CiPlatformContext CiPlatformDb)
        //{
        //    _CiPlatformDb = CiPlatformDb;
        //}
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        //public IActionResult Login()
        //{
        //    return View();
        //}



        //[HttpPost]
        //public IActionResult Login(User objlogin)
        //{
        //    if(_CiPlatformDb.Users.Any(U => U.Email == objlogin.Email && U.Password == objlogin.Password))
        //    {
        //        return RedirectToAction("Register","Home");
        //    }
        //    return View();
        //}

        //public IActionResult Register()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult Register(User objuser)
        //{
        //    if (objuser.Email != null && objuser.Password != null)
        //    {
        //        _CiPlatformDb.Users.Add(objuser);
        //        _CiPlatformDb.SaveChanges();
        //        return RedirectToAction("Login", "Home");
        //    }
        //    return View();
        //}






        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}