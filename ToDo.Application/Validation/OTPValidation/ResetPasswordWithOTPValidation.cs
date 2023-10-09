using Microsoft.AspNetCore.Identity;
using System.Text;
using ToDo.Application.Abstractions;
using ToDo.Application.DTOs;
using ToDo.Domain.Models;

namespace ToDo.Application.Validation.OTPValidation
{
    internal class ResetPasswordWithOTPValidation : IValidation<ResetPasswordWithOTPDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ResetPasswordWithOTPValidation(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public StringBuilder Error { get; set; } = new StringBuilder();

        public async System.Threading.Tasks.Task Validate(ResetPasswordWithOTPDto DTO)
        {
            var user = await _userManager.FindByEmailAsync(DTO.Email);

            if (user == null)
            {
                Error.AppendLine("user not found");
                return;
            }


            if (DateTime.Now.CompareTo(user.OTPExpirationDate) > 0)
                Error.AppendLine("OTP is expired");

            if (user.OTP != DTO.OTP)
                Error.AppendLine("incorect OTP");
        }
    }
}