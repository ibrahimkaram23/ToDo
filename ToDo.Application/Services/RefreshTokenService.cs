using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using ToDo.Application.Abstractions;
using ToDo.Application.DTOs;
using ToDo.Application.DTOs.User;
using ToDo.Domain.Models;

namespace ToDo.Application.Services
{
    internal class RefreshTokenService : IRefreshTokenService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IValidation<RevokeTokenDto> _revokeTokenValidation;
        private readonly IValidation<RefreshTokenDto> _refreshtokenValidation;
        private readonly ILogger<ApplicationUser> _logger;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;

        public RefreshTokenService(UserManager<ApplicationUser> userManager, 
            IValidation<RevokeTokenDto> revokeTokenValidation, IValidation<RefreshTokenDto> refreshtokenValidation,
            ILogger<ApplicationUser> logger, IJwtTokenService jwtTokenService, IMapper mapper)
        {
            _userManager = userManager;
            _revokeTokenValidation = revokeTokenValidation;
            _refreshtokenValidation = refreshtokenValidation;
            _logger = logger;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
        }

        public async Task<ResponseModel> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
        {
            try
            {
                await _refreshtokenValidation.Validate(refreshTokenDto);

                if (_refreshtokenValidation.Error.Length > 0)
                    return new ResponseModel(false)
                    { 
                        Error = _refreshtokenValidation.Error.ToString(),
                    };

                var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens!.Any(t => t.Token == refreshTokenDto.Token));

                var refreshToken = user!.RefreshTokens!.Single(t => t.Token == refreshTokenDto.Token);

                refreshToken.RevokedOn = DateTime.UtcNow;

                var newRefreshToken = GenerateRefreshToken();

                user.RefreshTokens!.Add(newRefreshToken);

                await _userManager.UpdateAsync(user);

                var jwtToken = _jwtTokenService.CreateJwtToken(user);

                var authModel = new AuthDto()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    ExpiresOn = jwtToken.ValidTo,
                    User = _mapper.Map<ApplicationUserDto>(user),
                    RefreshToken = newRefreshToken.Token,
                    RefreshTokenExpiration = newRefreshToken.ExpiresOn,
                };

                return new ResponseModel(true)
                {
                    Data = authModel,
                };

            }
            catch (Exception ex) { _logger.Log(LogLevel.Error, ex.ToString()); }

            return new ResponseModel(false);
        }

        public async Task<ResponseModel> RevokeTokenAsync(RevokeTokenDto revokeTokenDto)
        {
            try
            {
                await _revokeTokenValidation.Validate(revokeTokenDto);

                if (_revokeTokenValidation.Error.Length > 0)
                    return new ResponseModel(false)
                    {
                        Error = _revokeTokenValidation.Error.ToString(),
                    };

                var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens!.Any(t => t.Token == revokeTokenDto.Token));
                
                var refreshToken = user!.RefreshTokens!.Single(t => t.Token == revokeTokenDto.Token);

                refreshToken.RevokedOn = DateTime.Now;

                await _userManager.UpdateAsync(user); ;

                return new ResponseModel(true);
            }
            catch (Exception ex) { _logger.Log(LogLevel.Error, ex.ToString()); }

            return new ResponseModel(false);
        }

        public RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using var generator = RandomNumberGenerator.Create();

            generator.GetBytes(randomNumber);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresOn = DateTime.UtcNow.AddDays(10),
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
