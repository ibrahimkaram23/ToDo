using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using ToDo.Application.Abstractions;
using ToDo.Application.DTOs;
using ToDo.Domain.Models;

namespace ToDo.Application.Validation.RefreshTokenValidation
{
    internal class RefreshTokenValidation : IValidation<RefreshTokenDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RefreshTokenValidation(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public StringBuilder Error { get; set; } = new StringBuilder();

        public async System.Threading.Tasks.Task Validate(RefreshTokenDto DTO)
        {
            var user = await _userManager.Users
                .SingleOrDefaultAsync(u => u.RefreshTokens!.Any(t => t.Token == DTO.Token));

            if (user == null)
            {
                Error.AppendLine("Invalid token");
                return;
            }
                

            var refreshToken = user.RefreshTokens!.Single(t => t.Token == DTO.Token);

            if (!refreshToken.IsActive)
                Error.AppendLine("Inactive token");
        }
    }
}
