using ToDo.Application.DTOs;
using ToDo.Domain.Models;

namespace ToDo.Application.Abstractions
{
    public interface IRefreshTokenService
    {
        public Task<ResponseModel> RefreshTokenAsync(RefreshTokenDto refreshTokenDto);

        public Task<ResponseModel> RevokeTokenAsync(RevokeTokenDto revokeTokenDto);

        public RefreshToken GenerateRefreshToken();
    }
}
