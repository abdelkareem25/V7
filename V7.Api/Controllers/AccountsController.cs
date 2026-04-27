using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using V7.Api.DTOs.Identity;
using V7.Domain.Entites.Identity;
using V7.Domain.Interfaces.Services;

namespace V7.Api.Controllers
{
    
    public class AccountsController : ApiBaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountsController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager,ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        // Register
        // POST: api/accounts/register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            var User = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email.Split('@')[0]
            };
            var Result = await _userManager.CreateAsync(User,model.Password);
            if(!Result.Succeeded) return BadRequest(Result);
            var ReturnedUser = new UserDto()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                Token = await _tokenService.CreateTokenAsync(User, _userManager)
            };
            
            return Ok(ReturnedUser);
        }

        // Login
        // POST: api/accounts/login
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var User = await _userManager.FindByEmailAsync(model.Email);
            if (User is null) return Unauthorized();
            var Reslt = await _signInManager.CheckPasswordSignInAsync(User, model.PassWord, false);
            if (!Reslt.Succeeded) return Unauthorized();
            return Ok(new UserDto()
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = await _tokenService.CreateTokenAsync(User, _userManager)
            });
        }
    }
}
