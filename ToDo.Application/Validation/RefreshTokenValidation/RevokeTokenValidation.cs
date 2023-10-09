using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using ToDo.Application.Abstractions;
using ToDo.Application.DTOs;
using ToDo.Domain.Models;

namespace ToDo.Application.Validation.RefreshTokenValidation
{
    internal class RevokeTokenValidation : IValidation<RevokeTokenDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RevokeTokenValidation(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public StringBuilder Error { get; set; } = new StringBuilder();

        public async System.Threading.Tasks.Task Validate(RevokeTokenDto DTO)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens!.Any(t => t.Token == DTO.Token));

            if (user == null)
            {
                Error.AppendLine("user not found");
                return;
            }
                

            var refreshToken = user!.RefreshTokens!.Single(t => t.Token == DTO.Token);

            if (!refreshToken.IsActive)
                Error.AppendLine("inactive token");
        }
    }
}
