using CI_PLATFORM.Entities.Models;
using CI_PLATFORM.Entities.ViewModels;

namespace CI_PLATFORM.Repository.Interface
{
    public interface IUserRepository
    {
        //public List<User> GetUserData();

        //User Login(User obj);
        public AdminViewModel getbanners();
        public Login login(Login obj);
        User Register(User obj);
        public bool checktoken(string token);
        User Forgotpassword(User obj);
        public bool checktime(string token);
        PasswordReset Resetpassword(User obj, string token);
    }
}
