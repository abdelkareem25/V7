using V7.Domain.Interfaces.Services;
using V7.Domain.Entites.Identity;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace V7.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<AppUser> _userManager;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config, UserManager<AppUser> userManager)
        {
            _config = config;
            _userManager = userManager;
            
            ///var keyString = _config["JWT:Key"] ?? "superSecretKey@345KeyThatIsLongEnoughToWork!123";
            ///_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
        }

        public async Task<string> CreateTokenAsync(AppUser User , UserManager<AppUser> userManager)
        {
            // private claims
            var AuthClaims = new List<Claim>
            {
                new Claim(ClaimTypes.GivenName, User.DisplayName),
                new Claim(ClaimTypes.Email, User.Email)
            };

            // add role claims
            var UserRoles = await _userManager.GetRolesAsync(User);
            foreach (var role in UserRoles)
            {
                AuthClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"] ?? "superSecretKey@345KeyThatIsLongEnoughToWork!123"));
            var Token = new JwtSecurityToken(
                issuer : _config["JWT:validIssuer"],
                audience : _config["JWT:validAudience"],
                expires : DateTime.UtcNow.AddDays(double.Parse(_config["JWT:expiryInDays"] ?? "2")),
                claims : AuthClaims,
                signingCredentials : new SigningCredentials(AuthKey,SecurityAlgorithms.HmacSha256Signature)

                );
            return new JwtSecurityTokenHandler().WriteToken(Token);
            #region MyRegion
            //// 1 private class [user defined]
            //var AuthClaims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.GivenName, User.DisplayName),
            //    new Claim(ClaimTypes.Email, User.Email),
            //};

            //var userRoles = await _userManager.GetRolesAsync(User);
            //foreach (var role in userRoles)
            //{
            //    AuthClaims.Add(new Claim(ClaimTypes.Role, role));
            //}

            //var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);

            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(AuthClaims),
            //    Expires = DateTime.UtcNow.AddDays(double.Parse(_config["JWT:DurationInDays"] ?? "7")),
            //    SigningCredentials = creds,
            //    Issuer = _config["JWT:Issuer"],
            //    Audience = _config["JWT:Audience"]
            //};

            //var tokenHandler = new JwtSecurityTokenHandler();
            //var token = tokenHandler.CreateToken(tokenDescriptor);

            //return tokenHandler.WriteToken(token); 
            #endregion
        }
    }
}
