using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ToDo.Application.Abstractions;
using ToDo.Application.DTOs;
using ToDo.Domain.Models;

namespace ToDo.Application.Services
{
    internal class PasswordService : IPasswordService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IValidation<ChangePasswordDto> _changePasswprdValidation;
        private readonly ILogger<ApplicationUser> _logger;

        public PasswordService(UserManager<ApplicationUser> userManager, IValidation<ChangePasswordDto> changePasswprdValidation,
            ILogger<ApplicationUser> logger)
        {
            _userManager = userManager;
            _changePasswprdValidation = changePasswprdValidation;
            _logger = logger;
        }

        public async Task<ResponseModel> ChangePasswordAsync(ChangePasswordDto model)
        {
            try
            {
                await _changePasswprdValidation.Validate(model);

                if (_changePasswprdValidation.Error.Length > 0)
                    return new ResponseModel(false)
                    {
                        Error = _changePasswprdValidation.Error.ToString(),
                    };

                var user = await _userManager.FindByEmailAsync(model.Email);

                await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            }
            catch (Exception ex) { _logger.Log(LogLevel.Error, ex.ToString()); }

            return new ResponseModel(false);
        }
    }
}
