using CI_PLATFORM.Entities.Data;
using CI_PLATFORM.Entities.Models;
using CI_PLATFORM.Repository.Interface;
using MailKit.Security;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using CI_PLATFORM.Entities.ViewModels;
using System.Web.Helpers;

namespace CI_PLATFORM.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        public readonly CiPlatformContext _CiPlatformContext;

        public UserRepository(CiPlatformContext CiPlatformContext)
        {
            _CiPlatformContext = CiPlatformContext;
        }

        //public User Login(User obj)
        //{
        //    var user = _CiPlatformContext.Users.FirstOrDefault(U => U.Email == obj.Email && U.Password == obj.Password);
        //    return user;
        //}

        public Login login(Login obj)
        {
            Login lgn = new Login();
            {
                lgn.admin = _CiPlatformContext.Admins.FirstOrDefault(a => a.Email == obj.Email && a.Password == a.Password);
                lgn.user = _CiPlatformContext.Users.FirstOrDefault(u => u.Email == obj.Email);
                if (Crypto.VerifyHashedPassword(lgn.user.Password, obj.Password))
                {
                    return lgn;
                }
                else
                {
                    return null;
                }
                    //lgn.user = _CiPlatformContext.Users.FirstOrDefault(u => u.Email == obj.Email && u.Password == obj.Password);

                    //var passwordHasher = new PasswordHasher<Login>();
                    //var passwordVerificationResult = passwordHasher.VerifyHashedPassword(objUser, objUser.Password, obj.Password);
                   
                //lgn.admin = _CiPlatformContext.Admins.FirstOrDefault(a => a.Email == obj.Email && a.Password == a.Password);
            }
            return lgn;
        }
        public User Register(User obj)
        {
            var user = _CiPlatformContext.Users.FirstOrDefault(x => x.Email == obj.Email);
            if (user == null)
            {
                _CiPlatformContext.Users.Add(obj);
                _CiPlatformContext.SaveChanges();
            }
            return user;
        }
        public User Forgotpassword(User obj)
        {

            var user = _CiPlatformContext.Users.FirstOrDefault(u => u.Email == (obj.Email.ToLower()));

            if (user == null)
            {
                return null;
            }

            #region Genrate Token
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[16];
            var random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var finalString = new String(stringChars);
            #endregion Genrate Token

            #region Update Password Reset Table
            PasswordReset entry = new PasswordReset();
            entry.Email = obj.Email;
            entry.Token = finalString;
            _CiPlatformContext.PasswordResets.Add(entry);
            _CiPlatformContext.SaveChanges();
            #endregion Update Password Reset Table

            #region Send Mail
            var mailBody = "<h1>Click link to reset password</h1><br><h2><a href='" + "https://localhost:7251/User/Resetpassword?token=" + finalString + "'>Reset Password</a></h2>";

            // create email message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("hetshah2207@gmail.com"));
            email.To.Add(MailboxAddress.Parse(user.Email));
            email.Subject = "Reset Your Password";
            email.Body = new TextPart(TextFormat.Html) { Text = mailBody };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("hetshah2207@gmail.com", "lpoqtojvkcgkwdms");
            smtp.Send(email);
            smtp.Disconnect(true);
            #endregion Send Mail
            return user;
        }
        public bool checktime(string token)
        {
            PasswordReset pr = _CiPlatformContext.PasswordResets.FirstOrDefault(x => x.Token == token);
            if(pr == null)
            {
                return true;
            }
            DateTime dateTimeVariable = pr.CreatedAt;
            if (DateTime.Now.Subtract(dateTimeVariable).TotalHours > 2)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool checktoken(string token)
        {
            PasswordReset pr = _CiPlatformContext.PasswordResets.FirstOrDefault(x => x.Token == token);
            if(pr == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public PasswordReset Resetpassword(User obj, string token)
        {
            var validToken = _CiPlatformContext.PasswordResets.FirstOrDefault(x => x.Token == token);
            if (validToken == null)
            {
                return null;
            }

            if (validToken != null)
            {
                var user = _CiPlatformContext.Users.FirstOrDefault(x => x.Email == validToken.Email);
                user.Password = obj.Password;
                _CiPlatformContext.Users.Update(user);
                _CiPlatformContext.SaveChanges();

            }
            return validToken;
        }
    }
}
