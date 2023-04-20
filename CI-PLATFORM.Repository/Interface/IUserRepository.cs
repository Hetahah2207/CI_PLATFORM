﻿using CI_PLATFORM.Entities.Models;
using CI_PLATFORM.Entities.ViewModels;

namespace CI_PLATFORM.Repository.Interface
{
    public interface IUserRepository
    {
        //public List<User> GetUserData();

        //User Login(User obj);
        public Login login(Login obj);
        User Register(User obj);
        User Forgotpassword(User obj);
        PasswordReset Resetpassword(User obj, string token);
    }
}
