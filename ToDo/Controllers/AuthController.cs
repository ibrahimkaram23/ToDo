using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDo.Application.Abstractions;
using ToDo.Application.DTOs;
using ToDo.Application.DTOs.User;

namespace ToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IOTPService _OTPService;
        private readonly IPasswordService _passwordService;

        public AuthController(IAuthService authService, IRefreshTokenService refreshTokenService, 
            IOTPService OTPService, IPasswordService passwordService)
        {
            _authService = authService;
            _refreshTokenService = refreshTokenService;
            _OTPService = OTPService;
            _passwordService = passwordService;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<ActionResult<ResponseModel>> Login([FromBody] LoginDto loginDto)
            => Ok(await _authService.LoginAsync(loginDto));

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<ActionResult<ResponseModel>> Register([FromBody] RegisterDto registerDto)
            => Ok(await _authService.RegisterAsync(registerDto));

        [HttpPut("[action]")]
        public async Task<ActionResult<ResponseModel>> Logout([FromBody] RevokeTokenDto revokeToken)
            => Ok(await _authService.Logout(revokeToken));

        [HttpPut("[action]")]
        public async Task<ActionResult<ResponseModel>> UpdateUser([FromForm] UpdateUserDto userDto)
        {
            userDto.UserId = User.FindFirstValue(ClaimTypes.Sid);

            return Ok(await _authService.UpdateUserData(userDto));
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<ResponseModel>> UserData()
            => Ok(await _authService.GetUserData(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        [AllowAnonymous]
        [HttpPut("[action]")]
        public async Task<ActionResult<ResponseModel>> RefreshToken(RefreshTokenDto refreshTokenDto)
             => Ok(await _refreshTokenService.RefreshTokenAsync(refreshTokenDto));

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<ActionResult<ResponseModel>> ForgetPassword(SendOTPDto sendOTPDto)
             => Ok(await _OTPService.SendOTPAsync(sendOTPDto));

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<ActionResult<ResponseModel>> ResetPassword(ResetPasswordWithOTPDto resetPasswordWithOTPDto)
             => Ok(await _OTPService.ResetPasswordWithOTP(resetPasswordWithOTPDto));

        [HttpPost("[action]")]
        public async Task<ActionResult<ResponseModel>> ChangePassword(ChangePasswordDto changePasswordDto)
            => Ok(await _passwordService.ChangePasswordAsync(changePasswordDto));
    }
}
