using ToDo.Application.DTOs;
using ToDo.Application.DTOs.User;

namespace ToDo.Application.Abstractions
{
    public interface IAuthService
    {
        public Task<ResponseModel> LoginAsync(LoginDto loginDto);

        public Task<ResponseModel> RegisterAsync(RegisterDto registerDto);

        public Task<ResponseModel> Logout(RevokeTokenDto revokeToken);

        public Task<ResponseModel> GetUserData(string? userName);

        public Task<ResponseModel> UpdateUserData(UpdateUserDto userDto);
    }
}
