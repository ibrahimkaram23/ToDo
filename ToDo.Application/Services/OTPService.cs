using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ToDo.Application.Abstractions;
using ToDo.Application.DTOs;
using ToDo.Domain.Models;
using ToDo.Infrastructure.Helper;

namespace ToDo.Application.Services
{
    internal class OTPService : IOTPService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IValidation<SendOTPDto> _sendOTPValidation;
        private readonly IValidation<ResetPasswordWithOTPDto> _verfiyOTPValidation;
        private readonly ILogger<ApplicationUser> _logger;

        public OTPService(UserManager<ApplicationUser> userManager, IValidation<SendOTPDto> sendOTPValidation, 
            IValidation<ResetPasswordWithOTPDto> verfiyOTPValidation, ILogger<ApplicationUser> logger)
        {
            _userManager = userManager;
            _sendOTPValidation = sendOTPValidation;
            _verfiyOTPValidation = verfiyOTPValidation;
            _logger = logger;
        }

        public async Task<ResponseModel> SendOTPAsync(SendOTPDto sendOTPDto)
        {
            try
            {
                await _sendOTPValidation.Validate(sendOTPDto);

                if (_sendOTPValidation.Error.Length > 0)
                    return new ResponseModel(false)
                    {
                        Error = _sendOTPValidation.Error.ToString(),
                    };

                string otp = new Random().Next(1000, 9999).ToString();

                var user = await _userManager.FindByEmailAsync(sendOTPDto.Email);

                user.OTP = otp;
                user.OTPExpirationDate = DateTime.Now.AddMinutes(5);

                await _userManager.UpdateAsync(user);

                MailHelper.Send(user.Email, "Romeya ToDo Verify Password", 
                    $"Hello  Dear {user.FullName}\nthis is your reset password OTP {user.OTP}\nvalid unitl {user.OTPExpirationDate}");

                return new ResponseModel(true);
            }
            catch (Exception ex) { _logger.Log(LogLevel.Error, ex.ToString()); }

            return new ResponseModel(false);
        }

        public async Task<ResponseModel> ResetPasswordWithOTP(ResetPasswordWithOTPDto verfiyOTPDto)
        {
            try
            {
                await _verfiyOTPValidation.Validate(verfiyOTPDto);

                if (_verfiyOTPValidation.Error.Length > 0)
                    return new ResponseModel(false)
                    {
                        Error = _verfiyOTPValidation.Error.ToString(),
                    };

                var user = await _userManager.FindByEmailAsync(verfiyOTPDto.Email);

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                await _userManager.ResetPasswordAsync(user, token, verfiyOTPDto.Password);

                return new ResponseModel(true);
            }
            catch (Exception ex) { _logger.Log(LogLevel.Error, ex.ToString()); }

            return new ResponseModel(false);
        }
    }
}
