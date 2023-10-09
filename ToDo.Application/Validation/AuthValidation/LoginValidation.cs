using Microsoft.AspNetCore.Identity;
using System.Text;
using ToDo.Application.Abstractions;
using ToDo.Application.DTOs;
namespace ToDo.Application.Validations.AuthValidation
{
    internal class LoginValidation : IValidation<LoginDto>
    {
        private readonly UserManager<Domain.Models.ApplicationUser> _userManager;

        public LoginValidation(UserManager<Domain.Models.ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public StringBuilder Error { get; set; } = new StringBuilder();

        public async Task Validate(LoginDto DTO)
        {
            var user = await _userManager.FindByEmailAsync(DTO.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, DTO.Password))
                Error.AppendLine("email or password is incorrect");
        }
    }
}
