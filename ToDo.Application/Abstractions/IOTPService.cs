using ToDo.Application.DTOs;

namespace ToDo.Application.Abstractions
{
    public interface IOTPService
    {
        public  Task<ResponseModel> SendOTPAsync(SendOTPDto sendOTPDto);

        public Task<ResponseModel> ResetPasswordWithOTP(ResetPasswordWithOTPDto verfiyOTPDto);
    }
}
