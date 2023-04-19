using CI_PLATFORM.Entities.Models;
using CI_PLATFORM.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM.Repository.Interface
{
    public interface IAdminRepository 
    {
        public AdminViewModel getData();
        public AdminViewModel UserFilter(string search, int pg);
        public bool addcms(AdminViewModel obj,int command);
        public AdminViewModel EditForm(int id, string page);
        public bool deleteactivity(int id, int page);
        public bool Approval(int id, int page, int status);
    }
}
