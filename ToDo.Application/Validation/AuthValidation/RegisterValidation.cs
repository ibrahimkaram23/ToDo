using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using ToDo.Application.Abstractions;
using ToDo.Application.DTOs;

namespace ToDo.Application.Validations.AuthValidation
{
    internal class RegisterValidation : IValidation<RegisterDto>
    {
        private readonly UserManager<Domain.Models.ApplicationUser> _userManager;

        public RegisterValidation(UserManager<Domain.Models.ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public StringBuilder Error { get; set; } = new StringBuilder();

        public async Task Validate(RegisterDto DTO)
        {
            if (await _userManager.FindByEmailAsync(DTO.Email) is not null)
                Error.AppendLine("email is already exist!");

            if (await _userManager.FindByNameAsync(DTO.Username) is not null)
                Error.AppendLine("username is already in use!");

            if (await _userManager.Users.FirstOrDefaultAsync(p => p.PhoneNumber == DTO.PhoneNumber) is not null)
                Error.AppendLine("phone number is already exist!");

        }
    }
}
