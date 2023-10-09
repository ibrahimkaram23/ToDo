using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDo.Application.Abstractions;
using ToDo.Domain.Models;

namespace ToDo.Application.Services
{
    internal class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public JwtSecurityToken CreateJwtToken(ApplicationUser user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Sid, user.Id!),
                new Claim(ClaimTypes.Name, user.FullName!),
                new Claim(ClaimTypes.NameIdentifier, user.UserName!),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber!),
                new Claim(ClaimTypes.Email, user.Email!),
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudiance"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JWT:DurationInMinutes"]!)),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
