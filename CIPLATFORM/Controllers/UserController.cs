using CI_PLATFORM.Entities.Data;
using CI_PLATFORM.Entities.Models;
using CI_PLATFORM.Entities.ViewModels;
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
        //[HttpPost]
        //public IActionResult Login(User obj)
        //{
        //    var user = _UserRepository.Login(obj);
        //    if(user == null)
        //    {
        //        TempData["loginerr"] = "Email Or Password Is Inavalid!!!!!";
        //        return View();
        //    }
        //    HttpContext.Session.SetString("Uname", user.FirstName + " " + user.LastName);
        //    HttpContext.Session.SetInt32("UId", (Int32)user.UserId);
        //    if (user.Avatar != null)
        //    {
        //        HttpContext.Session.SetString("Avtar", user.Avatar);
        //    }
            
        //    else
        //    {
        //        HttpContext.Session.SetString("Avtar", "");
        //    }
        //    TempData["logins"] = "logged Successfull";
        //    return RedirectToAction("HomeGrid", "Platform");
        //}
        [HttpPost]
        public IActionResult Login(Login obj)
        {
            Login login = _UserRepository.login(obj);
            if (login.user == null && login.admin == null)
            {
                TempData["loginerr"] = "Email Or Password Is Inavalid!!!!!";
                return View();
            }
            else if (login.admin != null)
            {
                HttpContext.Session.SetString("Uname", login.admin.FirstName + " " + login.admin.LastName);
                HttpContext.Session.SetInt32("UId", (Int32)login.admin.AdminId);
                HttpContext.Session.SetString("Avtar", "");
                TempData["logins"] = "logged as a admin Successfull";
                return RedirectToAction("Admin", "Admin");

            }
            else if(login.user != null)
            {
                HttpContext.Session.SetString("Uname", login.user.FirstName + " " + login.user.LastName);
                HttpContext.Session.SetInt32("UId", (Int32)login.user.UserId);
                if (login.user.Avatar != null)
                {
                    HttpContext.Session.SetString("Avtar", login.user.Avatar);
                }
                else
                {
                    HttpContext.Session.SetString("Avtar", "");
                }
                TempData["logins"] = "logged Successfull";
                return RedirectToAction("HomeGrid", "Platform");
            }
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Register obj)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                {
                    user.FirstName = obj.FirstName;
                    user.LastName = obj.LastName;
                    user.Email = obj.Email;
                    user.Password = obj.Password;
                    user.PhoneNumber = obj.PhoneNumber;
                }
                var check = _UserRepository.Register(user);
                if (check != null)
                {
                    TempData["unsuccess"] = "User already exist";
                    return View();
                }
                else
                {
                    TempData["success"] = "Registration Successfull";
                    return RedirectToAction("Login", "User");
                }
            }
            return View();
        }
        public IActionResult Forgotpassword()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Forgotpassword(ForgotPwd obj)
        {
            User user1 = new User();
            {

                user1.Email = obj.Email;


            }
            if (ModelState.IsValid)
            {
                var user = _UserRepository.Forgotpassword(user1);
                if (user == null)
                {
                    TempData["msg"] = "Invalid Email";
                    return View();
                }
                else
                {
                    TempData["Message"] = "Check your email to reset password";
                }
                return RedirectToAction("Login");
            }
            return View();
        }
        public IActionResult Resetpassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Resetpassword(ResetPwd obj, string token)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                {

                    user.Password = obj.Password;


                }
                var validToken = _UserRepository.Resetpassword(user, token);

                if (validToken != null)
                {
                    TempData["Message"] = "Your Password is changed";
                    return RedirectToAction("Login");
                }
                TempData["Message"] = "Token not Found";
                return RedirectToAction("Login");
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login" , "User");
        }
    }
}
