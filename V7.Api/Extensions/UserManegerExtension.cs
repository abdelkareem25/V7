using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using V7.Domain.Entites.Identity;

namespace V7.Api.Extensions
{
    public static class UserManegerExtension
    {
        public static async Task<AppUser>FindUserWithAddressAsync(this UserManager<AppUser> userManager,ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.Users.Include(x=> x.Address).FirstOrDefaultAsync(x=>x.Email== email);
            return user;    
        }
    }
}
