using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using V7.Application.Services;
using V7.Domain.Entites.Identity;
using V7.Domain.Interfaces.Services;
using V7.Infrastructure.Identity;

namespace V7.Api.Extensions
{
    public static class IdentityServicesExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection Services, IConfiguration config)
        {
            Services.AddScoped<ITokenService, TokenService>();

            Services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<AppIdentityDbContext>();

            Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"] ?? "superSecretKey@345KeyThatIsLongEnoughToWork!123")),
                            ValidateIssuer = true,
                            ValidIssuer = config["JWT:validIssuer"],
                            ValidateAudience = true,
                            ValidAudience = config["JWT:validAudience"],
                            ValidateLifetime = true,
                            ClockSkew = System.TimeSpan.Zero
                        };
                    });
            return Services;
        }
    }
}
