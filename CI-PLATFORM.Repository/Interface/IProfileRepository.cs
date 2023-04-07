using CI_PLATFORM.Entities.Models;
using CI_PLATFORM.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM.Repository.Interface
{
    public interface IProfileRepository
    {
        public ProfileViewModel getUser(int UId);
        public bool changepassword(ProfileViewModel user, int UId);
        public bool saveProfile(ProfileViewModel obj, int UId);
        public bool ContactUs(ProfileViewModel obj);
    }
}
