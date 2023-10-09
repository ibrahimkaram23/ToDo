using System.ComponentModel.DataAnnotations;

namespace ToDo.Application.DTOs
{
    public class ResetPasswordWithOTPDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string OTP { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
