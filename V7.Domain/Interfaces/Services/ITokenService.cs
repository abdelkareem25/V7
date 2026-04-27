using Microsoft.AspNetCore.Identity;
using V7.Domain.Entites.Identity;
namespace V7.Domain.Interfaces.Services
{
    public interface ITokenService
    {
       Task<string> CreateTokenAsync(AppUser User, UserManager<AppUser> userManager);
    }
}
