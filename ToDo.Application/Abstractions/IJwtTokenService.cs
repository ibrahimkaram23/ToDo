using System.IdentityModel.Tokens.Jwt;
using ToDo.Domain.Models;

namespace ToDo.Application.Abstractions
{
    internal interface IJwtTokenService
    {
        public JwtSecurityToken CreateJwtToken(ApplicationUser user);
    }
}
