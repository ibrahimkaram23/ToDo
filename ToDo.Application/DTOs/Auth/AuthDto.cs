using ToDo.Application.DTOs.User;

namespace ToDo.Application.DTOs
{
    public class AuthDto
    {
        public ApplicationUserDto User { get; set; }
        public string? Token { get; set; }
        public DateTime? ExpiresOn { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
