using CI_PLATFORM.Entities.Data;
using CI_PLATFORM.Entities.Models;
using CI_PLATFORM.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CIPLATFORM.Controllers
{
    public class UserController : Controller
    {
        public readonly IUserRepository _UserRepository;

        public UserController(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
        }   
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User obj)
        {
            var user = _UserRepository.Login(obj);
            if(user == null)
            {
                return View();
            }
            return RedirectToAction("Register", "User");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User obj)
        {
            var user = _UserRepository.Register(obj);
            if(user!= null)
            {
                return View();
            }
            TempData["success"] = "Registration Successfull";
            return RedirectToAction("Login", "User");
        }
        public IActionResult Forgotpassword()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Forgotpassword(User obj)
        {
            var user = _UserRepository.Forgotpassword(obj);
            if (user == null)
            {
                TempData["Message"] = "Invalid Email";
                return View();
            }
            TempData["Message"] = "Check your email to reset password";
            return RedirectToAction("Login");
        }
        public IActionResult Resetpassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Resetpassword(User obj, string token)
        {

            var validToken = _UserRepository.Resetpassword(obj, token);

            if (validToken != null)
            {
                TempData["Message"] = "Your Password is changed";
                return RedirectToAction("Login");
            }
            TempData["Message"] = "Token not Found";
            return RedirectToAction("Login");
        }

    }
}
