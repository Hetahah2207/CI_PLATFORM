using CI_PLATFORM.Entities.Data;
using CI_PLATFORM.Entities.Models;
using CI_PLATFORM.Entities.ViewModels;
using CI_PLATFORM.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM.Repository.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        public readonly CiPlatformContext _CiPlatformContext;
        public ProfileRepository(CiPlatformContext CiPlatformContext)
        {
            _CiPlatformContext = CiPlatformContext;
        }
        public ProfileViewModel getUser(int UId)
        {

            User user = _CiPlatformContext.Users.FirstOrDefault(x => x.UserId == UId);
            ProfileViewModel pm = new ProfileViewModel();
            if (user != null)
            {
                {
                    pm.UserId = user.UserId;
                    pm.FirstName = user.FirstName;
                    pm.LastName = user.LastName;
                    pm.EmployeeId = user.EmployeeId;
                    pm.Title = user.Title;
                    pm.Department = user.Department;
                    pm.ProfileText = user.ProfileText;
                    pm.Department = user.Department;
                    pm.WhyIVolunteer = user.WhyIVolunteer;
                    pm.CountryId = user.CountryId;
                    pm.CityId = user.CityId;
                    pm.LinkedInUrl = user.LinkedInUrl;
                }
            }
            return pm;
        }
        public bool saveProfile(ProfileViewModel user, int save, int UId)
        {

            User pm = _CiPlatformContext.Users.FirstOrDefault(x => x.UserId == UId);
            //PasswordReset pr = _CiPlatformContext.PasswordResets;
            if (pm != null)
            {
                if (save == 1)
                {
                    if (user.resetPass.OldPassword == pm.Password)
                    {
                        {
                            pm.Password = user.resetPass.Password;

                        }
                        _CiPlatformContext.Users.Update(pm);
                        _CiPlatformContext.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                if(save == 3)
                {
                    {
                        
                        pm.FirstName = user.FirstName;
                        pm.LastName = user.LastName;
                        pm.EmployeeId = user.EmployeeId;
                        pm.Title = user.Title;
                        pm.Department = user.Department;
                        pm.ProfileText = user.ProfileText;
                        pm.Department = user.Department;
                        pm.WhyIVolunteer = user.WhyIVolunteer;
                        pm.CountryId = user.CountryId;
                        pm.CityId = user.CityId;
                        pm.LinkedInUrl = user.LinkedInUrl;
                    }
                    _CiPlatformContext.Users.Update(pm);
                    _CiPlatformContext.SaveChanges();
                }


            }
            return true;
        }
    }
}
