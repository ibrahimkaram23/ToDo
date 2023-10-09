using System.ComponentModel.DataAnnotations;

namespace ToDo.Application.DTOs
{
    public class RefreshTokenDto
    {
        [Required]
        public string Token { get; set; }
    }
}
