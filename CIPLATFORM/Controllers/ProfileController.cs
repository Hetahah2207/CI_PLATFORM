using Microsoft.AspNetCore.Mvc;

namespace CIPLATFORM.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Profile()
        {
            return View();
        }
    }
}
