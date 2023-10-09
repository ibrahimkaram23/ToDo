using Microsoft.AspNetCore.Identity;
using System.Text;
using ToDo.Application.Abstractions;
using ToDo.Application.DTOs;
using ToDo.Domain.Models;

namespace ToDo.Application.Validation.PasswordValidation
{
    internal class ChangePasswordValidation : IValidation<ChangePasswordDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ChangePasswordValidation(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public StringBuilder Error { get; set; } = new StringBuilder();

        public async System.Threading.Tasks.Task Validate(ChangePasswordDto DTO)
        {
            if (await _userManager.FindByEmailAsync(DTO.Email) == null)
                Error.AppendLine("user not found");
        }
    }
}
