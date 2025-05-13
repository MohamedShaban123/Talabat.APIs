using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Service
{
   public interface IAuthService
    {

        // Method to create a JWT token for a given user
        Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager);



    }
}
