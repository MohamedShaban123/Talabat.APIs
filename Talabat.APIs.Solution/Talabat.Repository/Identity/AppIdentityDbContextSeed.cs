using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync( UserManager<AppUser> _userManage)
        {
           if(_userManage.Users.Count() == 0)
            {
                var user  = new AppUser()
                { 
                    DisplayName="Ahmed Naser",
                    Email="ahmed.nasr@linkdev.com",
                    UserName="ahned.nasr", // requried
                    PhoneNumber="01020886338"
                
                };
                await _userManage.CreateAsync(user,"Pa$$w0rd");
            }
        }
    }
}
