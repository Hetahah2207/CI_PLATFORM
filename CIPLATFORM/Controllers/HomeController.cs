using CI_PLATFORM.Entities.Data;
using CI_PLATFORM.Entities.Models;
using CIPLATFORM.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CIPLATFORM.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}