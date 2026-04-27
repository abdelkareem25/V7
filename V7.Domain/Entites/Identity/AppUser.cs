using Microsoft.AspNetCore.Identity;

namespace V7.Domain.Entites.Identity
{
    public class AppUser: IdentityUser
    {
        public string DisplayName { get; set; }
        public Address Address { get; set; }
    }
}
