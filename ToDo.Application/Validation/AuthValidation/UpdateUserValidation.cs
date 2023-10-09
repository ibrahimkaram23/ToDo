using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using ToDo.Application.Abstractions;
using ToDo.Application.DTOs.User;
using ToDo.Infrastructure.Helper;

namespace ToDo.Application.Validations.AuthValidation
{
    internal class UpdateUserValidation : IValidation<UpdateUserDto>
    {
        private readonly UserManager<Domain.Models.ApplicationUser> _userManager;

        public UpdateUserValidation(UserManager<Domain.Models.ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public StringBuilder Error { get; set; } = new StringBuilder();

        public async Task Validate(UpdateUserDto DTO)
        {
            var user = await _userManager.FindByIdAsync(DTO.UserId);

            if (user == null)
                Error.AppendLine("user not found");

            if (DTO.Username != null && await _userManager.FindByNameAsync(DTO.Username) != null)
                Error.AppendLine("username used before");

            if (DTO.Email != null && await _userManager.FindByEmailAsync(DTO.Email) != null)
                Error.AppendLine("email used before");

            if (DTO.PhoneNumber != null && await _userManager.Users.FirstOrDefaultAsync(p => p.PhoneNumber == DTO.PhoneNumber) != null)
                Error.AppendLine("phonenumber used before");

        }
    }
}
