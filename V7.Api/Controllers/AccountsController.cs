using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using V7.Api.DTOs.Identity;
using V7.Api.Extensions;
using V7.Domain.Entites.Identity;
using V7.Domain.Interfaces.Services;

namespace V7.Api.Controllers
{

    public class AccountsController : ApiBaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService , IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        // Register
        // POST: api/accounts/register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if(CheckEmailExists(model.Email).Result.Value) return BadRequest("Email already in use");

            var User = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email.Split('@')[0]
            };
            var Result = await _userManager.CreateAsync(User, model.Password);
            if (!Result.Succeeded) return BadRequest(Result);
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
        [Authorize]
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
        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            var returnedUser = new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            };
            return Ok(returnedUser);
        }
        [Authorize]
        [HttpGet("CurrentUserAddress")]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            var user = await _userManager.FindUserWithAddressAsync(User);
            var returnedAddress= _mapper.Map<Address, AddressDto>(user.Address);
            return Ok(returnedAddress);
        }

        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto address)
        {
            var user = await _userManager.FindUserWithAddressAsync(User);
            var mappedAddress = _mapper.Map<AddressDto, Address>(address);
            mappedAddress.Id = user.Address.Id;
            user.Address = mappedAddress;
            var result = await _userManager.UpdateAsync(user);
            if(!result.Succeeded) return BadRequest("Problem updating the user address");
            return Ok(address);
        }

        [HttpGet("CheckEmailExists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null) return false;
            return true;
        }
    }
}
