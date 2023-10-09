using ToDo.Application.DTOs;

namespace ToDo.Application.Abstractions
{
    public interface IPasswordService
    {
        public Task<ResponseModel> ChangePasswordAsync(ChangePasswordDto model);
    }
}
