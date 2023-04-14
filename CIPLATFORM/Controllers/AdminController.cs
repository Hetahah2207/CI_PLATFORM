using CI_PLATFORM.Entities.Models;
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
            return View(am);
        }
        public IActionResult UserFilter(string? search)
        {
            List<User> fusers = _AdminRepository.UserFilter(search);
            AdminViewModel am = new AdminViewModel();
            {
                am.users = fusers;
            }
            return PartialView("_User",am);
        }
    }
}
