using Microsoft.AspNetCore.Identity;
using V7.Domain.Entites.Identity;

namespace V7.Infrastructure.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var User = new AppUser()
                {
                    DisplayName = "Abdelkarim Badr",
                    Email = "abdelkarim.badr@gmail.com",
                    UserName = "Abdelkarim.badr",
                    PhoneNumber = "1234567890"
                };
                await userManager.CreateAsync(User, "Pa$$w0rd");
            }
        }
    }
}
