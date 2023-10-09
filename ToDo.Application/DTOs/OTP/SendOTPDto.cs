using System.ComponentModel.DataAnnotations;

namespace ToDo.Application.DTOs
{
    public class SendOTPDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
