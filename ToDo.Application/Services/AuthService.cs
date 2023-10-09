using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using ToDo.Application.DTOs.User;
using ToDo.Application.DTOs;
using ToDo.Domain.Models;
using ToDo.Application.Abstractions;
using Microsoft.Extensions.Logging;
using ToDo.Infrastructure.Helper;
using ToDo.Domain.Models.Enums;

namespace ToDo.Application.Services
{
    internal class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IValidation<LoginDto> _loginValidation;
        private readonly IValidation<RegisterDto> _registerValidation;
        private readonly IValidation<UpdateUserDto> _updateValidation;
        private readonly IMapper _mapper;
        private readonly ILogger<ApplicationUser> _logger;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IRefreshTokenService _refreshTokenService;

        public AuthService(UserManager<ApplicationUser> userManager,
            IValidation<LoginDto> loginValidation, IValidation<RegisterDto> registerValidation, 
            IValidation<UpdateUserDto> updateValidation, IMapper mapper, ILogger<ApplicationUser> logger,
            IJwtTokenService jwtTokenService, IRefreshTokenService refreshTokenService)
        {
            _userManager = userManager;
            _loginValidation = loginValidation;
            _registerValidation = registerValidation;
            _updateValidation = updateValidation;
            _mapper = mapper;
            _logger = logger;
            _jwtTokenService = jwtTokenService;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<ResponseModel> LoginAsync(LoginDto loginDto)
        {
            try
            {
                await _loginValidation.Validate(loginDto);

                if(_loginValidation.Error.Length > 0)
                    return new ResponseModel(false)
                    { 
                        Error = _loginValidation.Error.ToString(),
                    };

                var user = await _userManager.FindByEmailAsync(loginDto.Email);

                var jwtSecurityToken = _jwtTokenService.CreateJwtToken(user);
                
                var authDto = new AuthDto();

                authDto.User = _mapper.Map<ApplicationUserDto>(user);

                authDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                
                authDto.ExpiresOn = jwtSecurityToken.ValidTo;

                if (user.RefreshTokens!.Any(t => t.IsActive))
                {
                    var activeRefreshToken = user.RefreshTokens!.FirstOrDefault(t => t.IsActive);
                    
                    authDto.RefreshToken = activeRefreshToken!.Token;
                    
                    authDto.RefreshTokenExpiration = activeRefreshToken.ExpiresOn;
                }
                else
                {
                    var refreshToken = _refreshTokenService.GenerateRefreshToken();

                    authDto.RefreshToken = refreshToken.Token;

                    authDto.RefreshTokenExpiration = refreshToken.ExpiresOn;

                    user.RefreshTokens!.Add(refreshToken);

                    await _userManager.UpdateAsync(user);
                }

                return new ResponseModel(true)
                {
                    Data = authDto,
                };

            }
            catch (Exception ex) { _logger.Log(LogLevel.Error, ex.ToString()); }

            return new ResponseModel(false);
        }

        public async Task<ResponseModel> Logout(RevokeTokenDto revokeToken)
        {
            try
            {
                await _refreshTokenService.RevokeTokenAsync(revokeToken);

                return new ResponseModel(true);
            }
            catch (Exception ex) { _logger.Log(LogLevel.Error, ex.ToString()); }

            return new ResponseModel(false);
        }

        public async Task<ResponseModel> RegisterAsync(RegisterDto registerDto)
        {
            try
            {
                await _registerValidation.Validate(registerDto);

                if (_registerValidation.Error.Length > 0)
                    return new ResponseModel(false)
                    {
                        Error = _registerValidation.Error.ToString(),
                    };

                var user = new ApplicationUser
                {
                    FullName = registerDto.FullName,
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                    PhoneNumber = registerDto.PhoneNumber,
                };

                await _userManager.CreateAsync(user, registerDto.Password);

                var jwtSecurityToken = _jwtTokenService.CreateJwtToken(user);

                var refreshToken = _refreshTokenService.GenerateRefreshToken();

                user.RefreshTokens!.Add(refreshToken);

                await _userManager.UpdateAsync(user);

                var authDto = new AuthDto()
                {
                    User = _mapper.Map<ApplicationUserDto>(user),
                    ExpiresOn = jwtSecurityToken.ValidTo,
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    RefreshToken = refreshToken.Token,
                    RefreshTokenExpiration = refreshToken.ExpiresOn
                };

                return new ResponseModel(true)
                {
                    Data = authDto,
                };
            }
            catch (Exception ex) { _logger.Log(LogLevel.Error, ex.ToString()); }

            return new ResponseModel(false);
        }

        public async Task<ResponseModel> GetUserData(string? userName)
        {
            try
            {
                if (userName == null)
                    return new ResponseModel(false)
                    {
                        Error = "invalid username",
                    };

                var user = await _userManager.FindByNameAsync(userName);

                if (user == null)
                    return new ResponseModel(false)
                    {
                        Error = "user not found",
                    };

                var userDto = _mapper.Map<ApplicationUserDto>(user);

                return new ResponseModel(true)
                {
                    Data = userDto,
                };

            }
            catch (Exception ex) { _logger.Log(LogLevel.Error, ex.ToString()); }

            return new ResponseModel(false);
        }

        public async Task<ResponseModel> UpdateUserData(UpdateUserDto userDto)
        {
            try
            {
                await _updateValidation.Validate(userDto);

                if (_updateValidation.Error.Length > 0)
                    return new ResponseModel(false)
                    {
                        Error = _updateValidation.Error.ToString(),
                    };

                var user = await _userManager.FindByIdAsync(userDto.UserId);

                string? updatedUserImage = null;

                if(userDto.ImageFile != null)
                {
                    if(user.Image != null)
                        ImageSettings.DeleteFile(user.Image, ImageType.UserPicture);

                    updatedUserImage = ImageSettings.UploadFile(userDto.ImageFile, ImageType.UserPicture);
                }

                user.Update(userDto.Email, userDto.PhoneNumber, userDto.Username, userDto.FullName, 
                    userDto.BirthOfDate, userDto.Gender, updatedUserImage);

                await _userManager.UpdateAsync(user);

                return new ResponseModel(true)
                {
                    Data = _mapper.Map<ApplicationUserDto>(user),
                };
            }
            catch (Exception ex) { _logger.Log(LogLevel.Error, ex.ToString()); }

            return new ResponseModel(false);
        }
    }
}
