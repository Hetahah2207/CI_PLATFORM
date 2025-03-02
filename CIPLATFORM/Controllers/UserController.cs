﻿using CI_PLATFORM.Entities.Data;
using CI_PLATFORM.Entities.Models;
using CI_PLATFORM.Entities.ViewModels;
using CI_PLATFORM.Repository.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Web.Helpers;

namespace CIPLATFORM.Controllers
{
    public class UserController : Controller
    {
        public readonly IUserRepository _UserRepository;

        public UserController(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
        }
        public IActionResult Login(String returnUrl = "")
        {
            List<Banner> banners = new List<Banner>();
            banners = _UserRepository.getbanners().banners;
            ViewBag.banners = banners;
            Login login = new Login();
            {
                login.returnUrl = returnUrl;
            }
            return View(login);
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
            List<Banner> banners = new List<Banner>();
            banners = _UserRepository.getbanners().banners;
            ViewBag.banners = banners;
            if (obj.Email == null || obj.Password == null)
            {
                TempData["loginerr"] = "Email and Password Is required!!!!!";
                return View();
            }
            Login login = _UserRepository.login(obj);
            if (login.user == null && login.admin == null)
            {
                TempData["loginerr"] = "Email Or Password Is Inavalid!!!!!";
                return View();
            }
            string role = "";
            if (login.admin != null)
            {
                role = "Admin";
            }
            else
            {
                role = "user";
            }

            var claims = new List<Claim>
                {
                    new Claim("role",role),
                    new Claim("Name", $"{login.user.FirstName} {login.user.LastName}"),
                    new Claim("Email", login.user.Email),
                    new Claim("Uid", login.user.UserId.ToString()),
                };
            var identity = new ClaimsIdentity(claims, "AuthCookie");
            var Principle = new ClaimsPrincipal(identity);
            HttpContext.User = Principle;
            var abc = HttpContext.SignInAsync(Principle);

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


            if (login.admin != null)
            {
                TempData["logins"] = "logged as a admin Successfull";
                if (!string.IsNullOrEmpty(obj.returnUrl))
                {
                    return LocalRedirect(obj.returnUrl);
                }
                return RedirectToAction("HomeGrid", "Platform");

            }
            else if (login.admin == null)
            {
                TempData["logins"] = "logged Successfull";
                if (!string.IsNullOrEmpty(obj.returnUrl))
                {
                    return LocalRedirect(obj.returnUrl);
                }
                return RedirectToAction("HomeGrid", "Platform");
            }

            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Register(Register obj)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                {
                    user.FirstName = obj.FirstName;
                    user.LastName = obj.LastName;
                    user.Email = obj.Email;
                    user.Password = Crypto.HashPassword(obj.Password);
                    //user.Password = obj.Password;
                    //var passwordHasher = new Microsoft.AspNetCore.Identity.PasswordHasher<User>();
                    //user.Password = passwordHasher.HashPassword(user, obj.Password);
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
        public IActionResult Resetpassword(string token)
        {
            bool ct = _UserRepository.checktoken(token);
            if (!ct)
            {
                TempData["Message"] = "Your token is Invalid Cannot open the page";
                return RedirectToAction("Login");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Resetpassword(ResetPwd obj, string token)
        {
            if (ModelState.IsValid)
            {
                bool time = _UserRepository.checktime(token);
                if (!time)
                {
                    TempData["Message"] = " Your token is Expired ";
                    return RedirectToAction("Login");
                }
                else
                {
                    User user = new User();
                    {
                        user.Password = Crypto.HashPassword(obj.Password);
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
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync().Wait();
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "User");
        }
    }
}
